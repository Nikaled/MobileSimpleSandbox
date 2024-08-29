using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    [SerializeField] public TextMeshProUGUI CoinsText;
    [SerializeField] public Image Crosshair;
    [SerializeField] private GameObject BuildingMenu;
    [SerializeField] private GameObject InAppShop;
    [SerializeField] public GameObject SaveMapUI;
    [SerializeField] public GameObject CoinsUI;
    [SerializeField] public GameObject MultiplatformUI;
    [SerializeField] public GameObject WeaponSlotsUI;
    [Header("PC Interfaces")]
    [SerializeField] GameObject _helicopterInstruction;
    [SerializeField] GameObject _planeInstruction;
    [SerializeField] GameObject _objectInteractionInstruction;
    [SerializeField] GameObject _ControlCarInstruction;

    [SerializeField] GameObject _EnterTransportInstruction;
    [SerializeField] GameObject _EnterCitizenInstruction;

    [SerializeField] GameObject _rotatingModeInstruction;
    [SerializeField] GameObject _idleInstruction;
    [SerializeField] GameObject _buildingModeInstruction;
    [SerializeField] GameObject _deletingModeInstruction;
    [SerializeField] GameObject CanvasPCInterface;
    [SerializeField] GameObject CarShootingText;
    [Header("Mobile Interfaces")]
    [SerializeField] GameObject CanvasMobileInterface;
    [SerializeField] public GameObject LeftButtonsZone;
    [SerializeField] GameObject RightButtonsZone;
    [SerializeField] GameObject HelicopterMobileInstruction;
    [SerializeField] GameObject PlaneMobileInstruction;
    [SerializeField] GameObject CarMobileInstruction;
    [SerializeField] GameObject AppShopButton;
    [SerializeField] GameObject UpLeftButtons;
    [SerializeField] public Button DoButton;
    [SerializeField] public Button InteracteButton;
    [SerializeField] Button BuildingButton;
    [SerializeField] Image BuildingButtonImage;
    [SerializeField] Button DeletingButton;
    [SerializeField] Image DeletingButtonImage;
    [SerializeField] Button RotatingButton;
    [SerializeField] Image RotatingButtonImage;
    [SerializeField] Image[] InteracteSymbolInButton;
    [SerializeField] Image DoButtonImageInIdle;
    [SerializeField] Image DoButtonImageInMode;
    [Header("Rotating Mode")]
    [SerializeField] GameObject _rotatingChosenObjectModeInstruction;
    [SerializeField] Slider[] RotatingModeSlidersScale;
    [SerializeField] Slider[] RotatingModeSlidersRotation;

    private bool InAppShopActive;
    private bool SaveMapUIActive;
    private IEnumerator cursorLocker;
    [Header("Unlock cursor Windows")]
    [SerializeField] private List<GameObject> UnlockCursorWindows;

    private readonly string LoadedInGameplay = "LoadedInGameplay";
    [HideInInspector] public bool IsTutorial;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (Player.instance.AdWarningActive == true || IsTutorial)
        {
            return;
        }
        if (Player.instance.currentState == Player.PlayerState.Idle)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                ShowInAppShop(!InAppShopActive);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                ShowSaveMapUI(!SaveMapUIActive);
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {         
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene(0);
        }
    }
    private void Start()
    {
        Geekplay.Instance.GameReady();
        ChangeCoinsText(Geekplay.Instance.PlayerData.Coins);
        if (Geekplay.Instance.mobile)
        {
            CanvasMobileInterface.SetActive(true);
            CanvasPCInterface.SetActive(false);
            InteracteButton.gameObject.SetActive(false);
            ShowMobileIdleButtons(true);
        }
        else
        {
            CanvasMobileInterface.SetActive(false);
            CanvasPCInterface.SetActive(true);
            AppShopButton.SetActive(false);
        }
        Geekplay.Instance.PlayerData.CoinsChanged += ChangeCoinsText;
        Geekplay.Instance.LockCursorAfterAd += CheckActiveUnlockCursorWindows;

        Analytics.instance.SendEvent(LoadedInGameplay);
    }
    #region Mobile
    public void ChangeDoButtonImageToMode(bool Mode)
    {
        if (Mode)
        {
            DoButtonImageInIdle.gameObject.SetActive(false);
            DoButtonImageInMode.gameObject.SetActive(true);
        }
        else
        {
            DoButtonImageInIdle.gameObject.SetActive(true);
            DoButtonImageInMode.gameObject.SetActive(false);
        }
    }
    public void TurnYellowBuildingButton(bool Is)
    {
        if (Is)
        {
            BuildingButton.image.color = Color.yellow;
            BuildingButtonImage.color = Color.yellow;
        }
        else
        {
            BuildingButton.image.color = Color.white;
            BuildingButtonImage.color = Color.white;
        }
    }
    public void TurnYellowDeletingButton(bool Is)
    {
        if (Is)
        {
            DeletingButton.image.color = Color.yellow;
            DeletingButtonImage.color = Color.yellow;
        }
        else
        {
            DeletingButton.image.color = Color.white;
            DeletingButtonImage.color = Color.white;
        }
    }
    public void TurnYellowRotatingButton(bool Is)
    {
        if (Is)
        {
            RotatingButton.image.color = Color.yellow;
            RotatingButtonImage.color = Color.yellow;
        }
        else
        {
            RotatingButton.image.color = Color.white;
            RotatingButtonImage.color = Color.white;
        }
    }
    private void ShowMobileIdleButtons(bool Is)
    {
        LeftButtonsZone.SetActive(Is);
        RightButtonsZone.SetActive(Is);
    }
    private void ShowWeaponSlotsUI(bool Is)
    {
        WeaponSlotsUI.SetActive(Is);
    }

    public void ShowCurrentInteracteButton(int ButtonIndex)
    {
        for (int i = 0; i < InteracteSymbolInButton.Length; i++)
        {
            InteracteSymbolInButton[i].gameObject.SetActive(false);
        }
        InteracteSymbolInButton[ButtonIndex].gameObject.SetActive(true);

    }
    public void ShowAllUI(bool Is)
    {
        if (Geekplay.Instance.mobile)
        {
            CanvasMobileInterface.SetActive(Is);
        }
        else
        {
            CanvasPCInterface.SetActive(Is);
        }
        MultiplatformUI.SetActive(Is);
    }
    public void ShowSaveMapUI(bool Is)
    {
        SaveMapUIActive = Is;
        SaveMapUI.SetActive(Is);
        if (Is)
        {
            Player.instance.examplePlayer.LockCursor(false);
            Player.instance.InterfaceActive = true;

        }
        else
        {
            CheckActiveUnlockCursorWindows();
        }
    }
    public void ShowHelicopterMobileInstruction(bool Is)
    {
        HelicopterMobileInstruction.SetActive(Is);
        ShowMobileIdleButtons(!Is);
    }
    public void ShowCarMobileInstruction(bool Is)
    {
        CarMobileInstruction.SetActive(Is);
        ShowMobileIdleButtons(!Is);
    }
    public void ShowPlaneMobileInstruction(bool Is)
    {
        PlaneMobileInstruction.SetActive(Is);
        ShowMobileIdleButtons(!Is);
    }
    #endregion
    private void OnDisable()
    {
        Geekplay.Instance.PlayerData.CoinsChanged -= ChangeCoinsText;
        Geekplay.Instance.LockCursorAfterAd -= CheckActiveUnlockCursorWindows;
    }
    public void CheckActiveUnlockCursorWindows()
    {
        if (cursorLocker != null)
        {
            StopCoroutine(cursorLocker);
        }
        cursorLocker = DelayDeactivateInterface(false);
        StartCoroutine(cursorLocker);

        if (Geekplay.Instance.mobile == true)
        {
            Cursor.lockState = CursorLockMode.None;

            return;
        }
        Cursor.lockState = CursorLockMode.Locked;

        for (int i = 0; i < UnlockCursorWindows.Count; i++)
        {
            if (UnlockCursorWindows[i].activeInHierarchy == true)
            {
                Cursor.lockState = CursorLockMode.None;
                if(UnlockCursorWindows[i].activeInHierarchy == BuildingMenu)
                {
                    continue;
                }
                if (cursorLocker != null)
                {
                    StopCoroutine(cursorLocker);
                }
                cursorLocker = DelayDeactivateInterface(true);
                Debug.Log("UnlockCursorWindows[i].activeInHierarchy:" + UnlockCursorWindows[i].name);
                StartCoroutine(cursorLocker);
            }
        }
    }
    //StartCoroutine(DelayDeactivateInterface(false));
    //if (Geekplay.Instance.mobile == true)
    //{
    //    Cursor.lockState = CursorLockMode.None;

    //    return;
    //}
    //Cursor.lockState = CursorLockMode.Locked;

    //for (int i = 0; i < UnlockCursorWindows.Count; i++)
    //{
    //    if(UnlockCursorWindows[i].activeInHierarchy == true)
    //    {
    //        Cursor.lockState = CursorLockMode.None;
    //        StartCoroutine(DelayDeactivateInterface(true));
    //    }
    //}
    private IEnumerator DelayDeactivateInterface(bool Is)
    {
        yield return new WaitForSeconds(0.05f);
        Player.instance.InterfaceActive = Is;
    }
    private void ChangeCoinsText(int NewValue)
    {
        CoinsText.text = NewValue.ToString();
    }
    public void ShowHelicopterInstruction(bool Is)
    {
        _helicopterInstruction.SetActive(Is);
    }
    public void ShowTransportEnterInstruction(bool Is)
    {
        _EnterTransportInstruction.SetActive(Is);
        if (Is)
        {
            ShowObjectInteructInstruction(false);
            ShowCitizenEnterInstruction(false);
        }
    }
    public void ShowControlCarInstruction(bool Is, bool IsTank)
    {
        _ControlCarInstruction.SetActive(Is);
        CarShootingText.SetActive(IsTank);
    }

    public void ShowInAppShop(bool Is)
    {
        InAppShopActive = Is;
        InAppShop.SetActive(Is);
        if (Is)
        {
            Player.instance.examplePlayer.LockCursor(false);
            Player.instance.InterfaceActive = true;
        }
        else
        {
            CheckActiveUnlockCursorWindows();
        }
    }
    public void ShowCitizenEnterInstruction(bool Is)
    {
        _EnterCitizenInstruction.SetActive(Is);
        if (Is)
        {
            ShowObjectInteructInstruction(false);
            ShowTransportEnterInstruction(false);
        }
    }
    public void ShowPlaneInstruction(bool Is)
    {
        _planeInstruction.SetActive(Is);
    }
    public void ShowBuildingMenu(bool Is)
    {
        BuildingMenu.SetActive(Is);
        CheckActiveUnlockCursorWindows();
    }
    public void ShowObjectInteructInstruction(bool Is)
    {
        _objectInteractionInstruction.SetActive(Is);
        if (Is)
        {
            ShowTransportEnterInstruction(false);
            ShowCitizenEnterInstruction(false);
        }

    }
    public void ShowRotatingModeInstruction(bool Is)
    {
        _rotatingModeInstruction.SetActive(Is);
    }
    public void ShowDeletingModeInstruction(bool Is)
    {
        _deletingModeInstruction.SetActive(Is);
        ShowCitizenEnterInstruction(false);
        ShowTransportEnterInstruction(false);
    }
    public void ShowBuildingModeInstruction(bool Is)
    {
        _buildingModeInstruction.SetActive(Is);
    }
    public void ShowIdleInstruction(bool Is)
    {
        _idleInstruction.SetActive(Is);
        ShowWeaponSlotsUI(Is);
    }
    public void ShowChosenObjectRotatingModeInstruction(bool Is, Vector3 Scale, Vector3 Rotation)
    {
        _rotatingChosenObjectModeInstruction.SetActive(Is);
        if (Is)
        {
            ShowTransportEnterInstruction(false);
            ShowCitizenEnterInstruction(false);
            ShowObjectInteructInstruction(false);
        }
        if (Geekplay.Instance.mobile)
        {
            ShowMobileIdleButtons(!Is);
            UpLeftButtons.SetActive(!Is);
        }

        if (Is)
        {
            float[] ScaleParametres = new float[] { Scale.x, Scale.y, Scale.z };
            float[] RotationParametres = new float[] { Rotation.x, Rotation.y, Rotation.z };
            for (int i = 0; i < ScaleParametres.Length; i++)
            {
                if (ScaleParametres[i] > RotatingModeSlidersScale[i].maxValue)
                {
                    ScaleParametres[i] = RotatingModeSlidersScale[i].maxValue;
                }
            }
            for (int i = 0; i < RotationParametres.Length; i++)
            {
                if (RotationParametres[i] < RotatingModeSlidersRotation[i].minValue)
                {
                    RotationParametres[i] += 360f;
                }
                if (RotationParametres[i] > RotatingModeSlidersRotation[i].maxValue)
                {
                    RotationParametres[i] -= 360f;
                }
            }
            for (int i = 0; i < RotatingModeSlidersScale.Length; i++)
            {
                RotatingModeSlidersScale[i].value = ScaleParametres[i];
            }
            for (int i = 0; i < RotatingModeSlidersRotation.Length; i++)
            {
                RotatingModeSlidersRotation[i].value = RotationParametres[i];
            }
        }

    }
}
