using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController.Examples;
using KinematicCharacterController;
using Cinemachine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] public GameObject PlayerParent;
    [SerializeField] public KinematicCharacterMotor motor;
    [SerializeField] public ExampleCharacterController characterController;
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] public AnimationPlayer animationPlayer;
    [SerializeField] GrenadeLauncher grenadeLauncher;
    public PlayerState currentState = PlayerState.Idle;
    public WeaponType CurrentWeapon;
    public event Action PistolFire;
    public event Action<int> SwitchedWeapon;
    [SerializeField] public Animator animator;

    [SerializeField] public ExamplePlayer examplePlayer;
    [SerializeField] ExampleCharacterCamera normalCamera;

    public EnterController currentNearTransport;
    [SerializeField] GameObject GunModel;
    [SerializeField] GameObject PistolModel;
    [SerializeField] GameObject KnifeModel;
    [SerializeField] GameObject GrenadeModel;
    [SerializeField] GameObject CharacterModel;
    [SerializeField] public AudioSource SwapCitizenAudioSource;
    
    public KeyCode DeletingModeButton = KeyCode.N;
    public KeyCode BuildingModeButton = KeyCode.B;
    public KeyCode RotatingModeButton = KeyCode.M;
    public static Player instance;
    public bool IsFirstView;
    public bool InterfaceActive;
    public bool AdWarningActive;

    public SkinnedMeshRenderer CurrentCitizenMesh;
    public SkinnedMeshRenderer[] PlayerMeshes;
    private IEnumerator lockOnShoot;
    public enum PlayerState
    {
        InTransport,
        Aiming,
        Shooting,
        Idle,
        Sitting,
        Building,
        DeletingBuilding,
        RotatingBuilding,
        AimingGrenade,
        InterfaceActive,
    }
    float startTime;
    public enum WeaponType
    {
        Pistol,
        Gun,
        Knife,
        Hand,
        Grenade
    }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (Geekplay.Instance.mobile)
        {
            examplePlayer.Mobile = true;
            examplePlayer.PC = false;
            CanvasManager.instance.DoButton.onClick.AddListener(delegate { MobileFireInput(); });
            examplePlayer.LockCursor(false);
        }
        else
        {
            examplePlayer.Mobile = false;
            examplePlayer.PC = true;
           
        }
    }
    public void PlayerSetActive(bool Is)
    {
        PlayerParent.SetActive(Is);
    }
    public void SwitchPlayerState(PlayerState newPlayerState, float Delay = 0.1f)
    {
        if (currentState == PlayerState.Sitting)
        {
            CanvasManager.instance.LeftButtonsZone.SetActive(true);
            if(newPlayerState != PlayerState.Idle)
            {
            return;
            }
        }
        if (Geekplay.Instance.mobile)
        {
            CanvasManager.instance.DoButton.onClick.RemoveAllListeners();
            CanvasManager.instance.TurnYellowDeletingButton(false);
            CanvasManager.instance.TurnYellowBuildingButton(false);
            CanvasManager.instance.TurnYellowRotatingButton(false);
        }
        if (currentState == PlayerState.DeletingBuilding)
        {
            BuildingManager.instance.ActivateDeletingMode(false);
        }
        switch (newPlayerState)
        {
            case PlayerState.Aiming:
                if (currentState != PlayerState.Aiming)
                {
                    animator.SetBool("PistolAiming", true);
                    GoToAimCamera();
                }
                break;
            case PlayerState.Idle:
                animator.SetBool("PistolAiming", false);
                CanvasManager.instance.ShowIdleInstruction(true);
                CanvasManager.instance.ChangeDoButtonImageToMode(false);
                break;
            case PlayerState.DeletingBuilding:
                BuildingManager.instance.ActivateDeletingMode(true);
                CanvasManager.instance.TurnYellowDeletingButton(true);
                CanvasManager.instance.ChangeDoButtonImageToMode(true);
                break;
            case PlayerState.RotatingBuilding:
                BuildingManager.instance.ActivateRotatingMode(true);
                CanvasManager.instance.ShowCitizenEnterInstruction(false);
                CanvasManager.instance.ShowTransportEnterInstruction(false);
                CanvasManager.instance.TurnYellowRotatingButton(true);
                CanvasManager.instance.ChangeDoButtonImageToMode(true);
                break;
            case PlayerState.Building:
                CanvasManager.instance.TurnYellowBuildingButton(true);
                CanvasManager.instance.ChangeDoButtonImageToMode(true);
                break;
            case PlayerState.Sitting:
                CanvasManager.instance.LeftButtonsZone.SetActive(false);
                break;

        }
        if (currentState == PlayerState.Aiming && newPlayerState != PlayerState.Aiming)
        {
            GoToNormalCamera();
        }
        if (Delay > 0)
        {
        StartCoroutine(DelaySwitchState(newPlayerState, Delay));
        }
        else
        {
            AfterSwitchState(newPlayerState);
        }
    }   
    private void AfterSwitchState(Player.PlayerState newPlayerState)
    {
        if (newPlayerState == PlayerState.Idle || newPlayerState == PlayerState.Aiming)
        {
            CanvasManager.instance.DoButton.onClick.AddListener(delegate { MobileFireInput(); });
        }
        else
        {
            if (currentState == PlayerState.Idle)
                CanvasManager.instance.ShowIdleInstruction(false);
        }
        if (currentState == PlayerState.DeletingBuilding)
        {
            BuildingManager.instance.TurnDeletingObjectNormalAndClearFields();
        }
        if (currentState == PlayerState.RotatingBuilding)
        {
            BuildingManager.instance.TurnRotatingObjectNormalAndClearFields();
        }
        currentState = newPlayerState;
        Debug.Log("Player State:" + currentState);
    }
    private IEnumerator DelaySwitchState(Player.PlayerState newPlayerState, float Delay)
    {

        yield return new WaitForSeconds(Delay);

        AfterSwitchState(newPlayerState);
    }
    public void SwitchWeapon(int PressedNumber)
    {
        if(currentState == PlayerState.Sitting)
        {
            return;
        }
        GunModel.SetActive(false);
        PistolModel.SetActive(false);
        KnifeModel.SetActive(false);
        GrenadeModel.SetActive(false);

        if (CurrentWeapon == WeaponType.Grenade)
        {
            animator.SetBool("AimingGrenade", false);
            grenadeLauncher.ClearTrajectory();
        }

        switch (PressedNumber)
        {
            case 4:
                CurrentWeapon = WeaponType.Gun;
                GunModel.SetActive(true);
                CanvasManager.instance.DoButton.onClick.RemoveAllListeners();
                CanvasManager.instance.DoButton.GetComponent<MobileShootButton>().enabled = true;

                break;
            case 3:
                CurrentWeapon = WeaponType.Pistol;
                PistolModel.SetActive(true);
                break;
            case 2:
                CurrentWeapon = WeaponType.Knife;
                KnifeModel.SetActive(true);
                break;
            case 1:
                CurrentWeapon = WeaponType.Hand;
                break;
            case 5:
                CurrentWeapon = WeaponType.Grenade;
                GrenadeModel.SetActive(true);
                CanvasManager.instance.DoButton.GetComponent<MobileShootButton>().enabled = true;
                break;

        }
        SwitchedWeapon?.Invoke(PressedNumber);
        SwitchPlayerState(PlayerState.Idle);
    }
    private void SwitchView()
    {
        IsFirstView = !IsFirstView;
        GoToNormalCamera();
    }
    public void SwitchCamera()
    {
        examplePlayer.SwitchCamera();
        SwitchView();
    }
    private void FixedUpdate()
    {
        if (/*animationPlayer.IsMoving == false &&*/ currentState == PlayerState.Idle && IsFirstView)
        {
            RotatePlayerOnShoot(playerShooting.AimDirection);
        }
        if(currentState == PlayerState.AimingGrenade)
        {
            RotatePlayerOnShoot(playerShooting.AimDirection);
        }
    }
    private void Update()
    {
        if (currentState == PlayerState.Sitting || InterfaceActive || AdWarningActive)
        {
            return;
        }

        if (currentState == PlayerState.Idle)
        {
            if (Input.GetKeyDown(DeletingModeButton))
            {
                SwitchPlayerState(Player.PlayerState.DeletingBuilding);
            }
            if (Input.GetKeyDown(BuildingModeButton))
            {
                BuildingManager.instance.ActivateBuildingButton(true);
                SwitchPlayerState(Player.PlayerState.Building);
            }
            if (Input.GetKeyDown(RotatingModeButton))
            {
                SwitchPlayerState(Player.PlayerState.RotatingBuilding);
            }

        }
        if (currentState != PlayerState.Building && currentState != PlayerState.DeletingBuilding && currentState != PlayerState.RotatingBuilding)
        {
            if (Geekplay.Instance.mobile == false)
            {
                FireInput();
                ChangeWeaponInput();
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    SwitchView();
                }
            }
        }
        if (currentState == PlayerState.Building)
        {
            BuildingManager.instance.BuildingInput();
            if (Input.GetKeyDown(BuildingModeButton))
            {
                BuildingManager.instance.ActivateBuildingButton(false);
                SwitchPlayerState(Player.PlayerState.Idle);
            }
            return;
        }
        if (currentState == PlayerState.DeletingBuilding)
        {
            BuildingManager.instance.DeletingBuildingInput();

            if (Input.GetKeyDown(DeletingModeButton))
            {
                SwitchPlayerState(Player.PlayerState.Idle);
                BuildingManager.instance.TurnDeletingCitizenNormalAndClearFields();
                BuildingManager.instance.TurnDeletingObjectNormalAndClearFields();
            }
        }
        if (currentState == PlayerState.RotatingBuilding)
        {
            BuildingManager.instance.RotatingInput();
            if (BuildingManager.instance.RotateChosenObjectMode == false)
            {
                if (Input.GetKeyDown(RotatingModeButton))
                {
                    SwitchPlayerState(Player.PlayerState.Idle);
                    BuildingManager.instance.ActivateRotatingMode(false);
                }
            }
        }
        if (currentState == PlayerState.Aiming)
        {
            RotatePlayerOnShoot(playerShooting.AimDirection);
            playerShooting.LockPlayerMovement(0.05f);
        }
    }

    private void ChangeWeaponInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchWeapon(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchWeapon(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SwitchWeapon(5);
        }
    }

    public IEnumerator LockPositionOnShoot(float HoldingTime = 1f)
    {
        examplePlayer.MyLockOnShoot = true;
        yield return new WaitForSeconds(HoldingTime);
        examplePlayer.MyLockOnShoot = false;
    }

    #region MobileFunctions
    public void AimingGrenadeOnMobile()
    {
        animator.SetBool("AimingGrenade", true);
        currentState = PlayerState.AimingGrenade;
        grenadeLauncher.GrenadeInput();
        examplePlayer.MyLockOnShoot = true;
    }
    public void LaunchGrenadeOnMobile()
    {
        animator.SetBool("AimingGrenade", false);
        grenadeLauncher.LaunchGrenade();
        currentState = PlayerState.Idle;
        examplePlayer.MyLockOnShoot = false;
    }
    public void MobileJump()
    {
        examplePlayer.JumpIsTrue();
    }
    public void MobileFireInput()
    {
        if(currentState == PlayerState.Sitting)
        {
            return;
        }
        if (CurrentWeapon == WeaponType.Pistol)
        {

            animator.SetTrigger("PistolFire");
            motor.SetPosition(transform.position);
            playerShooting.Fire(CurrentWeapon);
        }
        if (CurrentWeapon == WeaponType.Knife)
        {

            animator.SetTrigger("Stab");
            playerShooting.HandAttack(WeaponType.Knife);
        }
        if (CurrentWeapon == WeaponType.Hand)
        {
            animator.SetBool("IsRun", false);
            animator.SetTrigger("Punch");
            playerShooting.HandAttack(WeaponType.Hand);
        }
        if (CurrentWeapon == WeaponType.Gun)
        {
            animator.SetTrigger("GunFire");
            playerShooting.Fire(CurrentWeapon);
            examplePlayer.MyLockOnShoot = true;
            motor.SetPosition(transform.position);
        }
        //if (CurrentWeapon == WeaponType.Grenade)
        //{
        //    AimingGrenadeOnMobile();
        //}
    } // Used by Fire Button
    public void MobileAiming()// Used by Aim Button
    {
        if (currentState != PlayerState.Aiming)
        {
            if (CurrentWeapon == WeaponType.Pistol || CurrentWeapon == WeaponType.Gun)
            {
                SwitchPlayerState(PlayerState.Aiming, 0);
                return;
            }
        }
        if (currentState == PlayerState.Aiming)
        {
            Debug.Log("Stop Aiming");
            SwitchPlayerState(PlayerState.Idle, 0);
        }
    }
    #endregion
    private void FireInput()
    {
        if (CurrentWeapon == WeaponType.Pistol)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("PistolFire");
                motor.SetPosition(transform.position);
                playerShooting.Fire(CurrentWeapon);
            }
        }
        if (CurrentWeapon == WeaponType.Knife)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("Stab");

                playerShooting.HandAttack(WeaponType.Knife);
            }
        }
        if (CurrentWeapon == WeaponType.Hand)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("IsRun", false);
                animator.SetTrigger("Punch");
                playerShooting.HandAttack(WeaponType.Hand);
            }
        }
        if (CurrentWeapon == WeaponType.Gun)
        {
            if (Input.GetMouseButton(0))
            {
                animator.SetTrigger("GunFire");
                playerShooting.Fire(CurrentWeapon);
                if (motor.GroundingStatus.IsStableOnGround)
                {
                motor.SetPosition(transform.position);
                }
            }
        }
        if (CurrentWeapon == WeaponType.Grenade)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetBool("AimingGrenade", true);
                currentState = PlayerState.AimingGrenade;
            }
            if (Input.GetMouseButton(0))
            {
                grenadeLauncher.GrenadeInput();
                examplePlayer.MyLockOnShoot = true;

            }
            if (Input.GetMouseButtonUp(0))
            {
                animator.SetBool("AimingGrenade", false);
                grenadeLauncher.LaunchGrenade();
                currentState = PlayerState.Idle;
                examplePlayer.MyLockOnShoot = false;
            }
        }
        if (currentState != PlayerState.Aiming)
        {
            if (CurrentWeapon == WeaponType.Pistol || CurrentWeapon == WeaponType.Gun)
            {
                if (Input.GetMouseButton(1))
                {

                    SwitchPlayerState(PlayerState.Aiming, 0);
                }
            }
        }
        if (currentState == PlayerState.Aiming)
        {
            //Quaternion targetRotation = Quaternion.LookRotation(playerShooting.AimDirection);
            //Quaternion OnlyY = new Quaternion(0, targetRotation.y, 0, targetRotation.w);
            //motor.RotateCharacter(OnlyY);

            //RotatePlayerOnShoot(playerShooting.AimDirection);
            if (Input.GetMouseButtonUp(1))
            {
                Debug.Log("Stop Aiming");
                SwitchPlayerState(PlayerState.Idle, 0);
            }
        }

    }
    public void RotatePlayerOnShoot(Vector3 aimDirection)
    {
        Quaternion targetRotation = Quaternion.LookRotation(aimDirection);
        Quaternion OnlyY = new Quaternion(0, targetRotation.y, 0, targetRotation.w);
        motor.RotateCharacter(OnlyY);
    }
    private void GoToAimCamera()
    {
        if (IsFirstView)
        {
            normalCamera.Camera.fieldOfView = 20;
            normalCamera.FollowPointFraming = new Vector2(0f, 0f);
        }
        else
        {
            normalCamera.Camera.fieldOfView = 30;
        }
        //playerShooting.lineRenderer.enabled = true;

    }
    private void GoToNormalCamera()
    {
        if (IsFirstView == false)
        {
            normalCamera.FollowPointFraming = new Vector2(1.8f, 1.8f);
            normalCamera.Camera.fieldOfView = 55;
        }
        else
        {
            normalCamera.FollowPointFraming = new Vector2(-0.2f, 0.69f);
            normalCamera.Camera.fieldOfView = 40;
        }
        //playerShooting.lineRenderer.enabled = false;

    }
}
