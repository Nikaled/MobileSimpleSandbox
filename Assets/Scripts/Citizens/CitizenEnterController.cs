using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenEnterController : EnterController
{
    [SerializeField] public SkinnedMeshRenderer CitizenMesh;
    [SerializeField] SkinnedMeshRenderer[] AllCitizenMeshes;
    [SerializeField] GameObject CitizenRoot;
    [SerializeField] CitizenMovement citizenMovement;
    private Vector3 PlayerPosition;
    private Quaternion PlayerRotation;

    public int CurrentMeshIndex;
    private readonly string AnalyticsCitizenSwaped = "SwapedCitizenBody";
    private void Start()
    {
        for (int i = 0; i < AllCitizenMeshes.Length; i++)
        {
            if (AllCitizenMeshes[i].gameObject.activeInHierarchy)
            {
                CurrentMeshIndex = i;
                CitizenMesh = AllCitizenMeshes[i];
            }
        }
    }
    public void LoadMeshIndex(int MeshIndex)
    {
        CurrentMeshIndex = MeshIndex;
        for (int i = 0; i < AllCitizenMeshes.Length; i++)
        {
            AllCitizenMeshes[i].gameObject.SetActive(false);
        }
        AllCitizenMeshes[MeshIndex].gameObject.SetActive(true);
        CitizenMesh = AllCitizenMeshes[MeshIndex];
    }
    public override void SitIntoTransport()
    {
        Player.instance.SwapCitizenAudioSource.Play();
        HideEnterInstruction();
        //HpView.SetActive(false);
        ActivateTransport();
        IsInterfaceActive = false;
        PlayerPosition = player.transform.position;
        PlayerRotation = player.transform.rotation;
        player.motor.SetPositionAndRotation(CitizenRoot.transform.position, CitizenRoot.transform.rotation, true);
        SwapMeshes();
        CitizenRoot.transform.position = PlayerPosition;
        CitizenRoot.transform.rotation = PlayerRotation;

        Analytics.instance.SendEvent(AnalyticsCitizenSwaped);
    }
    protected override void ShowEnterInstruction()
    {
        CanvasManager.instance.ShowCitizenEnterInstruction(true);
        CanvasManager.instance.ShowCurrentInteracteButton(0);
    }
    protected override void HideEnterInstruction()
    {
        CanvasManager.instance.ShowCitizenEnterInstruction(false);
    }
    private void SwapMeshes()
    {
        Mesh cashedPlayerMesh = player.CurrentCitizenMesh.sharedMesh;

        Texture[] CitizenTextures = new Texture[CitizenMesh.materials.Length];
        Texture[] PlayerTextures = new Texture[player.CurrentCitizenMesh.materials.Length];

        for (int i = 0; i < player.CurrentCitizenMesh.materials.Length; i++)
        {
            PlayerTextures[i] = player.CurrentCitizenMesh.materials[i].mainTexture;
        }
        for (int i = 0; i < CitizenMesh.materials.Length; i++)
        {
            CitizenTextures[i] = CitizenMesh.materials[i].mainTexture;
        }

        for (int i = 0; i < player.PlayerMeshes.Length; i++)
        {
            player.PlayerMeshes[i].gameObject.SetActive(false);

            if (CitizenMesh.sharedMesh == player.PlayerMeshes[i].sharedMesh)
            {
                player.PlayerMeshes[i].gameObject.SetActive(true);
                player.CurrentCitizenMesh = player.PlayerMeshes[i];
            }
        }
        for (int i = 0; i < AllCitizenMeshes.Length; i++)
        {
            AllCitizenMeshes[i].gameObject.SetActive(false);

            if (cashedPlayerMesh == AllCitizenMeshes[i].sharedMesh)
            {
                AllCitizenMeshes[i].gameObject.SetActive(true);
                CitizenMesh = AllCitizenMeshes[i];
                CurrentMeshIndex = i;
            }
        }

        for (int i = 0; i < CitizenMesh.materials.Length; i++)
        {
            CitizenMesh.materials[i].mainTexture = PlayerTextures[i];
        }
        for (int i = 0; i < player.CurrentCitizenMesh.materials.Length; i++)
        {
            player.CurrentCitizenMesh.materials[i].mainTexture = CitizenTextures[i];
        }

    }
    private void OnDestroy()
    {
        if(CanvasManager.instance != null)
        {
            HideEnterInstruction();
        }
    }
}
