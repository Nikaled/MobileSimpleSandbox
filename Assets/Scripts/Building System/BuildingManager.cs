using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private AudioSource deleteSound;
    private GameObject CurrentPrefab;
    private Vector3 pos;
    private RaycastHit hit;
    [SerializeField] Image Cross;
    [SerializeField] LayerMask layerMask;
    [SerializeField] LayerMask NotDeletingLayerMask;
    [SerializeField] LayerMask TurnRedIncludeMask;
    private GameObject pendingObj;
    public float rotateAmount = 45;
    bool IsBuildingOpened;
    [SerializeField] GameObject BuildingMenu;
    [SerializeField] Material OnBuildingMaterial;
    [SerializeField] Material RedMaterial;
    [SerializeField] Material YellowMaterial;
    [SerializeField] Button DoButton;
    [SerializeField] AudioSource PlaceObjectSoundSource;

    Player player;
    public static BuildingManager instance;

    private bool IsDeletingBuilding;
    List<Material[]> CashedMaterialsOnDeleting;
    List<Material[]> CashedMaterialsOnRotating;
    private GameObject deletingObject;
    private GameObject rotatingObject;

    Vector3 RotatingCashedRotating;
    Vector3 RotatingCashedScale;
    private Vector3 CashedHpScale;
    private GameObject ScalingObject;
    private float CashedRotatingX;
    private float CashedRotatingY;
    private float CashedRotatingZ;
    HpSystem hpSystemOnObject;
    private GameObject rotatingObjectCenter;
    readonly string BuildingAnalytics = "BuildingMenuOpened";
    readonly string BuildingObjectAnalytics = "ObjectBuilt";
    readonly string RotatingAnalytics = "RotatingMenuOpened";
    public bool RotateChosenObjectMode { get; private set; }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        player = Player.instance;
    }
    public void SetBuildingObject(GameObject ObjectPrefab)
    {
        CurrentPrefab = ObjectPrefab;
        SelectObject();
    }
    public void BuildingInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Cross.transform.position);
        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            pos = hit.point;
        }
        else
        {
            RaycastHit DownHit = new();
            Vector3 MaxDistancePos = ray.GetPoint(20);
            pos = MaxDistancePos;
        }
        if (pendingObj != null)
        {
            pendingObj.transform.position = pos;

            if (Geekplay.Instance.mobile == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    PlaceObject();
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    RotateObject();
                }
            }
            else
            {
                player.examplePlayer.LockCursor(false);
            }

        }
    }
    public void DeletingBuildingInput()
    {

        Debug.Log("Deleting Input");
        Ray ray = Camera.main.ScreenPointToRay(Cross.transform.position);
        if (Physics.Raycast(ray, out hit, 10000, TurnRedIncludeMask))

        {
            if (deletingObject != hit.collider.gameObject)
            {
                if (deletingObject != null)
                {
                    if (deletingObject.CompareTag("Citizen"))
                    {
                        TurnDeletingCitizenNormalAndClearFields();
                    }
                    else
                    {
                        TurnDeletingObjectNormalAndClearFields();
                    }
                }

                deletingObject = hit.collider.gameObject;
                Debug.Log("deleting object:" + deletingObject);
                if (IsItDestructable(deletingObject))
                {
                    if (deletingObject.CompareTag("Citizen"))
                    {
                        Debug.Log("наведен на жителя");
                        TurnIntoColorChosenCitizen(deletingObject.GetComponentsInChildren<SkinnedMeshRenderer>(), RedMaterial, ref CashedMaterialsOnDeleting);
                    }
                    else
                    {
                        TurnIntoColorChosenObject(deletingObject.GetComponentsInChildren<MeshRenderer>(), RedMaterial, ref CashedMaterialsOnDeleting);
                    }
                }
            }
            if (Geekplay.Instance.mobile == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    DeleteObject(deletingObject);
                }
            }
            else
            {
                player.examplePlayer.LockCursor(false);
            }
        }
        else
        {
            if (deletingObject != null)
            {
                if (deletingObject.CompareTag("Citizen"))
                {
                    TurnDeletingCitizenNormalAndClearFields();
                }
                else
                {
                    TurnDeletingObjectNormalAndClearFields();
                }
            }
        }
    }
    public void TurnDeletingObjectNormalAndClearFields()
    {

        if (deletingObject != null && CashedMaterialsOnDeleting != null)
        {
            if (deletingObject.CompareTag("Citizen"))
            {
                TurnDeletingCitizenNormalAndClearFields();
            }
            else
            {
                TurnNormalChosenObject(deletingObject.GetComponentsInChildren<MeshRenderer>(), CashedMaterialsOnDeleting);
            }
        }
        CashedMaterialsOnDeleting = null;
        deletingObject = null;
    }
    public void TurnDeletingCitizenNormalAndClearFields()
    {
        if (deletingObject != null && CashedMaterialsOnDeleting != null)
        {
            TurnNormalChosenCitizen(deletingObject.GetComponentsInChildren<SkinnedMeshRenderer>(), CashedMaterialsOnDeleting);
        }
        CashedMaterialsOnDeleting = null;
        deletingObject = null;
    }

    public void ActivateDeletingMode(bool Is)
    {
        //if (IsDeletingBuilding == true && Is == true)
        //{
        //    Is = false;
        //}
        DoButton.onClick.RemoveAllListeners();
        if (Is)
        {
            DoButton.onClick.AddListener(delegate { DeleteObject(deletingObject); });
        }
        IsDeletingBuilding = Is;
        if (Geekplay.Instance.mobile == false)
        {
            player.examplePlayer.LockCursor(true);
        }
        else
        {
            player.examplePlayer.LockCursor(false);
        }
        if (deletingObject != null)
        {
            if (deletingObject.CompareTag("Citizen"))
            {
                Debug.Log("Делаю жителя из красного нормальным");
                TurnDeletingCitizenNormalAndClearFields();
            }
            else
            {
                TurnDeletingObjectNormalAndClearFields();
            }
        }
        CanvasManager.instance.ShowDeletingModeInstruction(Is);
    }
    public void SwitchPlayerStateToRotating()
    {
        if (Player.instance.currentState != Player.PlayerState.RotatingBuilding)
        {
            player.SwitchPlayerState(Player.PlayerState.RotatingBuilding);
        }
        else
        {
            player.SwitchPlayerState(Player.PlayerState.Idle);
        }
    }
    public void SwitchPlayerStateToDeleting()
    {
        if (Player.instance.currentState != Player.PlayerState.DeletingBuilding)
        {
            player.SwitchPlayerState(Player.PlayerState.DeletingBuilding);
        }
        else
        {
            player.SwitchPlayerState(Player.PlayerState.Idle);
        }
    }
    public void SwitchPlayerStateToBuilding()
    {
        if (Player.instance.currentState != Player.PlayerState.Building)
        {
            player.SwitchPlayerState(Player.PlayerState.Building);
        }
        else
        {
            if (pendingObj == null)
            {
                player.SwitchPlayerState(Player.PlayerState.Idle);
            }
        }
    }
    private void DeleteObject(GameObject deletingObject)
    {

        if (IsItDestructable(deletingObject))
        {
            if (deletingObject.GetComponentInParent<DeletingRoot>() != null)
            {
                Destroy(deletingObject.GetComponentInParent<DeletingRoot>().gameObject);
            }
            if (deletingObject.GetComponentInParent<SerializedBuilding>() != null)
            {
                Destroy(deletingObject.GetComponentInParent<SerializedBuilding>().gameObject);
            }
            else
            {
                Destroy(deletingObject);
            }
            deleteSound.Play();
        }
    }
    private bool IsItDestructable(GameObject deletingObject)
    {
        if (deletingObject == null)
        {
            return false;
        }
        if (((NotDeletingLayerMask & (1 << deletingObject.layer)) != 0))
        {
            return false;
        }
        return true;
    }
    public void ActivateBuildingButton(bool Is)
    {
        IsBuildingOpened = Is;
        BuildingMenu.SetActive(IsBuildingOpened);
        if (Geekplay.Instance.mobile == false)
        {
            player.examplePlayer.LockCursor(!IsBuildingOpened);
        }
        else
        {
            player.examplePlayer.LockCursor(false);
        }
        if (pendingObj != null) { Destroy(pendingObj); }
        CanvasManager.instance.ShowBuildingModeInstruction(false);

        if (Is)
        {
            Analytics.instance.SendEvent(BuildingAnalytics);
        }
    }
    private void RotateObject()
    {
        pendingObj.transform.Rotate(Vector3.up, rotateAmount);
    }
    public void SelectObject()
    {
        CanvasManager.instance.ShowBuildingModeInstruction(true);
        pendingObj = Instantiate(CurrentPrefab, pos, transform.rotation);
        DeactivateColliders(pendingObj.GetComponentsInChildren<Collider>());
        SetOnBuildMaterial(pendingObj.GetComponentsInChildren<MeshRenderer>());
        if (pendingObj.GetComponentInChildren<Rigidbody>() != null) pendingObj.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Debug.Log("Object Selected");
        player.SwitchPlayerState(Player.PlayerState.Building);
        if (Geekplay.Instance.mobile == false)
        {
            player.examplePlayer.LockCursor(true);
        }
        else
        {
            DoButton.onClick.RemoveAllListeners();
            DoButton.onClick.AddListener(delegate { PlaceObject(); });
        }
        pendingObj.transform.LookAt(player.transform);
        pendingObj.transform.DORotate(new Vector3(0, pendingObj.transform.position.y, 0), 0);


    }
    private void PlaceObject()
    {
        DoButton.onClick.RemoveAllListeners();
        PlaceObjectSoundSource.Play();

        var newObj = Instantiate(CurrentPrefab, pendingObj.transform.position, pendingObj.transform.rotation);
        CanvasManager.instance.ShowBuildingModeInstruction(false);
        //if (CurrentPrefab.CompareTag("Road"))
        //{
        //    newObj.transform.position += new Vector3(0, 0.05f, 0);
        //}
        Destroy(pendingObj);
        Debug.Log("Object placed");
        player.SwitchPlayerState(Player.PlayerState.Idle);

        Geekplay.Instance.PlayerData.BuildCount++;
        Geekplay.Instance.Leaderboard("Buildings", Geekplay.Instance.PlayerData.BuildCount);

        Geekplay.Instance.Save();


        Analytics.instance.SendEvent(BuildingObjectAnalytics);
    }
    private void DeactivateColliders(Collider[] colliders)
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
    }
    private void SetOnBuildMaterial(MeshRenderer[] renderers)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            for (int j = 0; j < renderers[i].materials.Length; j++)
            {
                Destroy(renderers[i].materials[j]);
            }
            renderers[i].material = OnBuildingMaterial;

        }
    }
    private void TurnIntoColorChosenObject(MeshRenderer[] renderers, Material colorMaterial, ref List<Material[]> CashedMaterials)
    {
        List<Material[]> materials = new();
        for (int i = 0; i < renderers.Length; i++)
        {
            materials.Add(renderers[i].materials);
            Material[] RedMats = new Material[renderers[i].materials.Length];
            for (int j = 0; j < renderers[i].materials.Length; j++)
            {
                RedMats[j] = colorMaterial;
            }
            renderers[i].materials = RedMats;
        }
        CashedMaterials = materials;
    }
    private void TurnIntoColorChosenCitizen(SkinnedMeshRenderer[] renderers, Material colorMaterial, ref List<Material[]> CashedMaterials)
    {
        List<Material[]> materials = new();
        for (int i = 0; i < renderers.Length; i++)
        {
            materials.Add(renderers[i].materials);
            Material[] RedMats = new Material[renderers[i].materials.Length];
            for (int j = 0; j < renderers[i].materials.Length; j++)
            {
                RedMats[j] = colorMaterial;
            }
            renderers[i].materials = RedMats;
        }
        CashedMaterials = materials;
    }
    private void TurnNormalChosenObject(MeshRenderer[] renderers, List<Material[]> CashedMaterials)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            Material[] CashedMat = CashedMaterials[i];
            renderers[i].materials = CashedMat;
        }
    }
    private void TurnNormalChosenCitizen(SkinnedMeshRenderer[] renderers, List<Material[]> CashedMaterials)
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            Material[] CashedMat = CashedMaterials[i];
            renderers[i].materials = CashedMat;
        }
    }

    #region RotatingBuildings

    public void ActivateRotatingMode(bool IsActive)
    {
        CanvasManager.instance.ShowRotatingModeInstruction(IsActive);
        TurnRotatingObjectNormalAndClearFields();
        DoButton.onClick.RemoveAllListeners();
        if (IsActive)
        {
            DoButton.onClick.AddListener(delegate { ActivateRotateChosenObjectMode(true); });


            Analytics.instance.SendEvent(RotatingAnalytics);
        }
    }
    public void RotatingInput()
    {
        if (!RotateChosenObjectMode)
        {
            Ray ray = Camera.main.ScreenPointToRay(Cross.transform.position);
            if (Physics.Raycast(ray, out hit, 1000, TurnRedIncludeMask))
            {
                if (rotatingObject != hit.collider.gameObject)
                {
                    TurnRotatingObjectNormalAndClearFields();


                    rotatingObject = hit.collider.gameObject;
                    Debug.Log("rotatingObject object:" + rotatingObject);
                    if (IsItDestructable(rotatingObject))
                    {
                        if (rotatingObject.CompareTag("Citizen"))
                        {
                            TurnIntoColorChosenCitizen(rotatingObject.GetComponentsInChildren<SkinnedMeshRenderer>(), YellowMaterial, ref CashedMaterialsOnRotating);
                        }
                        else
                        {
                            TurnIntoColorChosenObject(rotatingObject.GetComponentsInChildren<MeshRenderer>(), YellowMaterial, ref CashedMaterialsOnRotating);
                        }
                    }
                }

            }
            else
            {
                if (rotatingObject != null)
                {
                    TurnRotatingObjectNormalAndClearFields();
                }
            }
            if (Geekplay.Instance.mobile == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (rotatingObject != null)
                    {
                        ActivateRotateChosenObjectMode(true);
                    }
                }
            }
        }
    }
    public void TurnRotatingObjectNormalAndClearFields()
    {
        if (rotatingObject != null && CashedMaterialsOnRotating != null)
        {
            Debug.Log("Turn object from yellow to normal" + rotatingObject.name);

            if (rotatingObject.CompareTag("Citizen"))
            {
                TurnNormalChosenCitizen(rotatingObject.GetComponentsInChildren<SkinnedMeshRenderer>(), CashedMaterialsOnRotating);
            }
            else
            {
                TurnNormalChosenObject(rotatingObject.GetComponentsInChildren<MeshRenderer>(), CashedMaterialsOnRotating);
            }

        }
        CashedMaterialsOnRotating = null;
        rotatingObject = null;
        rotatingObjectCenter = null;
    }

    private void ActivateRotateChosenObjectMode(bool Is)
    {
        if (rotatingObject == null)
        {
            return;
        }
        hpSystemOnObject = rotatingObject.GetComponentInChildren<HpSystem>();
        if (Is)
        {
            if (hpSystemOnObject != null)
                CashedHpScale = hpSystemOnObject.gameObject.transform.localScale;
        }

        Player.instance.examplePlayer.MyLockOnShoot = Is;
        if (rotatingObjectCenter != null)
        {
            var rotateCenter = rotatingObjectCenter.GetComponent<RotatingCenter>();
            if (rotateCenter != null)
            {
                rotateCenter.UnbindRotatingCenter();
            }
            rotatingObjectCenter = null;
        }
        RotateChosenObjectMode = Is;
        Debug.Log("Rotating object:" + rotatingObject.name);
        if (rotatingObject.CompareTag("Car"))
        {
            if (rotatingObject.GetComponentInChildren<Rigidbody>() != null)
            {
                rotatingObject.GetComponentInChildren<Rigidbody>().useGravity = !Is;
                rotatingObject.GetComponentInChildren<Rigidbody>().isKinematic = Is;
                Debug.Log("rotatingObject.GetComponent<Rigidbody>().useGravity" + rotatingObject.GetComponentInChildren<Rigidbody>().useGravity);

            }
        }
        if (rotatingObject.GetComponent<Rigidbody>() != null)
        {
            rotatingObject.GetComponent<Rigidbody>().useGravity = !Is;
            rotatingObject.GetComponent<Rigidbody>().isKinematic = Is;
            Debug.Log("rotatingObject.GetComponent<Rigidbody>().useGravity" + rotatingObject.GetComponent<Rigidbody>().useGravity);

        }
        if (rotatingObject.GetComponentInChildren<RotatingCenter>() != null)
        {
            rotatingObjectCenter = rotatingObject.GetComponentInChildren<RotatingCenter>().gameObject;
            rotatingObjectCenter.GetComponent<RotatingCenter>().SetRotatingCenter();
            Debug.Log("Центр вращения найден");
        }
        else
        {
            if (rotatingObject.GetComponentInParent<RotatingCenter>() != null)
            {
                rotatingObjectCenter = rotatingObject.GetComponentInParent<RotatingCenter>().gameObject;
                rotatingObjectCenter.GetComponent<RotatingCenter>().SetRotatingCenter();
                Debug.Log("Центр вращения  - родитель");
            }
            if (rotatingObject.transform.parent != null)
            {
                if (rotatingObject.transform.parent.GetComponentInChildren<RotatingCenter>() != null)
                {
                    rotatingObjectCenter = rotatingObject.transform.parent.GetComponentInChildren<RotatingCenter>().gameObject;
                    rotatingObjectCenter.GetComponent<RotatingCenter>().SetRotatingCenter();
                    Debug.Log("Центр вращения  - брат");
                }
            }
        }
        if (rotatingObjectCenter == null)
        {
            rotatingObjectCenter = rotatingObject;
            Debug.Log("Центр вращения не  найден");

        }
        RotatingCashedRotating = rotatingObjectCenter.transform.eulerAngles;
        ScalingObject = rotatingObject;
        RotatingCashedScale = rotatingObject.transform.localScale;


        if (Is)
        {
            ScalingParent ScaleParent = rotatingObject.GetComponentInChildren<ScalingParent>();
            if (ScaleParent != null)
            {
                ScaleParent.SetThisAsParent();
                RotatingCashedScale = ScaleParent.transform.localScale;
                ScalingObject = ScaleParent.gameObject;
                Debug.Log("есть скейл перент");
            }
        }
        else
        {
            if (rotatingObject.GetComponentInParent<ScalingParent>() != null)
            {
                ScalingObject.GetComponentInParent<ScalingParent>().SetThisAsChild();
                RotatingCashedScale = ScalingObject.transform.localScale;
                ScalingObject = null;
            }
        }

        CanvasManager.instance.ShowRotatingModeInstruction(false);
        CanvasManager.instance.ShowChosenObjectRotatingModeInstruction(Is, Scale: RotatingCashedScale, Rotation: RotatingCashedRotating);
        if (Is)
        {
            ChangeTextureManager.instance.ButtonsInitialize(rotatingObject);
            TurnNormalChosenObject(rotatingObject.GetComponentsInChildren<MeshRenderer>(), CashedMaterialsOnRotating);
            if (rotatingObject.CompareTag("Citizen"))
            {
                TurnNormalChosenCitizen(rotatingObject.GetComponentsInChildren<SkinnedMeshRenderer>(), CashedMaterialsOnRotating);

                var navmesh = rotatingObject.GetComponent<NavMeshAgent>();
                if(navmesh != null)
                {
                rotatingObject.GetComponent<NavMeshAgent>().enabled = false;
                }

            }
        }
        if (!Is)
        {
            ChangeTextureManager.instance.ClearButtonListeners();
            if (rotatingObject.CompareTag("Citizen"))
            {
                var navmesh = rotatingObject.GetComponent<NavMeshAgent>();
                if (navmesh != null)
                {
                    rotatingObject.GetComponent<NavMeshAgent>().enabled = true;
                }

            }
        }
        if (Geekplay.Instance.mobile == false)
        {
            player.examplePlayer.LockCursor(!Is);
        }
        else
        {
            player.examplePlayer.LockCursor(false);
        }
    }
    public void ApplyRotatingChanges()
    {

        ActivateRotateChosenObjectMode(false);
        player.SwitchPlayerState(Player.PlayerState.Idle);
    }
    public void CancelRotatingChanges()
    {
        ScalingObject.transform.localScale = RotatingCashedScale;
        rotatingObjectCenter.transform.eulerAngles = RotatingCashedRotating;
        CanvasManager.instance.ShowChosenObjectRotatingModeInstruction(true, Scale: RotatingCashedScale, Rotation: RotatingCashedRotating);
    }
    #region RotatingSliders
    public void RotatingSliderScaleX(float IncreaseNumber)
    {
        if (ScalingObject != null)
        {
            ScalingObject.transform.DOScaleX(IncreaseNumber, 0);
            if (hpSystemOnObject != null)
            {
                //if (ScalingObject.transform.localScale.x < IncreaseNumber)
                hpSystemOnObject.gameObject.transform.DOScaleX(hpSystemOnObject.OriginScale.x / IncreaseNumber, 0);

            }
        }
    }
    public void RotatingSliderScaleY(float IncreaseNumber)
    {
        if (ScalingObject != null)

            ScalingObject.transform.DOScaleY(IncreaseNumber, 0);
        if (hpSystemOnObject != null)
        {
            //hpSystemOnObject.gameObject.transform.DOScaleY(hpSystemOnObject.OriginScale.y / IncreaseNumber, 0);
            hpSystemOnObject.ResizeYHpBar(IncreaseNumber);
        }
    }
    public void RotatingSliderScaleZ(float IncreaseNumber)
    {
        if (ScalingObject != null)

            ScalingObject.transform.DOScaleZ(IncreaseNumber, 0);

        if (hpSystemOnObject != null)
        {
            if (ScalingObject.transform.localScale.z < IncreaseNumber)
                hpSystemOnObject.gameObject.transform.DOScaleZ(hpSystemOnObject.OriginScale.z / IncreaseNumber, 0);
        }
    }
    public void RotatingSliderRotateX(float IncreaseNumber)
    {
        if (rotatingObjectCenter != null)

            rotatingObjectCenter.transform.rotation = Quaternion.Euler(IncreaseNumber, rotatingObjectCenter.transform.eulerAngles.y, rotatingObjectCenter.transform.eulerAngles.z);

    }
    public void RotatingSliderRotateY(float IncreaseNumber)
    {
        if (rotatingObjectCenter != null)

            rotatingObjectCenter.transform.rotation = Quaternion.Euler(rotatingObjectCenter.transform.eulerAngles.x, IncreaseNumber, rotatingObjectCenter.transform.eulerAngles.z);
    }
    public void RotatingSliderRotateZ(float IncreaseNumber)
    {
        if (rotatingObjectCenter != null)

            rotatingObjectCenter.transform.rotation = Quaternion.Euler(rotatingObjectCenter.transform.eulerAngles.x, rotatingObjectCenter.transform.eulerAngles.y, IncreaseNumber);
    }

    #endregion
    #endregion

}
