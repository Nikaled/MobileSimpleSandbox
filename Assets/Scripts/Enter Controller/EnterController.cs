using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterController : MonoBehaviour
{
    public bool IsInterfaceActive;
    [HideInInspector] public bool IsPlayerIn;
    [SerializeField] protected Transform PlayerSpawnTransform;
    [SerializeField] protected Camera TransportCamera;
    [SerializeField] protected GameObject HpView;
    [HideInInspector] public Player player;
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collider entered");
            player = other.GetComponent<Player>();
            if(player.currentState != Player.PlayerState.Idle || IsPlayerIn || player.animationPlayer.RidingAnimal == true)
            {
                return;
            }
            ShowEnterInstruction();
            IsInterfaceActive = true;
            player.currentNearTransport = this;
            CanvasManager.instance.InteracteButton.gameObject.SetActive(true);
            CanvasManager.instance.InteracteButton.onClick.RemoveAllListeners();
            CanvasManager.instance.InteracteButton.onClick.AddListener(delegate { SitIntoTransport(); });
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideEnterInstruction();
            if(Player.instance.animationPlayer.RidingAnimal == true)
            {
                return;
            }
            CanvasManager.instance.InteracteButton.onClick.RemoveListener(delegate { SitIntoTransport(); });
            CanvasManager.instance.InteracteButton.gameObject.SetActive(false);
            if (player != null)
            {
                if (player.currentNearTransport != null)
                {
                    if (player.currentNearTransport == this)
                    {
                        player.currentNearTransport = null;
                    }
                }
            }
        }
        if (IsPlayerIn == false)
        {
            player = null;
        }

    }
    private void Start()
    {
        if (HpView == null)
        {
            HpView = GetComponentInChildren<HpSystem>().gameObject;
        }
    }
    protected virtual void ShowEnterInstruction()
    {
        CanvasManager.instance.ShowTransportEnterInstruction(true);
        CanvasManager.instance.ShowCurrentInteracteButton(1);
    }

    protected virtual void Update()
    {
        if (IsInterfaceActive == true)
        {
            if (player != null)
            {
                if (player.currentNearTransport != null)
                {
                    if (player.currentNearTransport == this && player.currentState == Player.PlayerState.Idle && player.InterfaceActive== false && player.AdWarningActive==false)
                    {
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            SitIntoTransport();
                            return;
                        }
                    }

                }

            }
        }
        if (IsPlayerIn)
        {
            if (Input.GetKeyDown(KeyCode.F) && player.AdWarningActive == false)
            {
                Debug.Log("F pressed");
                GetOutTransport();
            }
        }
    }
    protected virtual void HideEnterInstruction()
    {
        CanvasManager.instance.ShowTransportEnterInstruction(false);
    }
    public void ForceEnter()
    {
        player = Player.instance;
        HpView.SetActive(false);
        RotatingToPlayer.target = this.transform;
        player.PlayerSetActive(false);
        TransportCamera.farClipPlane = 150f;
        TransportCamera.gameObject.SetActive(true);
        CanvasManager.instance.InteracteButton.onClick.RemoveAllListeners();
        CanvasManager.instance.InteracteButton.gameObject.SetActive(false);
        HideEnterInstruction();
        ActivateTransport();
        IsPlayerIn = true;
        IsInterfaceActive = false;
        player.SwitchPlayerState(Player.PlayerState.InTransport, 0);
    }
    public virtual void SitIntoTransport()
    {
        if (Player.instance.currentState != Player.PlayerState.Idle)
        {
            return;
        }
        if (IsInterfaceActive == true)
        {
            if (player != null)
            {
                if (player.currentNearTransport != null)
                {
                    if (player.currentNearTransport == this && player.animationPlayer.RidingAnimal == false)
                    {
                        HpView.SetActive(false);
                        RotatingToPlayer.target = this.transform;
                        player.PlayerSetActive(false);
                        TransportCamera.farClipPlane = 150f;
                        TransportCamera.gameObject.SetActive(true);
                        CanvasManager.instance.InteracteButton.onClick.RemoveAllListeners();
                        CanvasManager.instance.InteracteButton.gameObject.SetActive(false);
                        HideEnterInstruction();
                        ActivateTransport();
                        IsPlayerIn = true;
                        IsInterfaceActive = false;
                        player.SwitchPlayerState(Player.PlayerState.InTransport, 0);

                    }

                }

            }
        }
    }
    public virtual void GetOutTransport()
    {
        RotatingToPlayer.target = Player.instance.transform;
        HpView.SetActive(true);
        player.PlayerParent.transform.position = PlayerSpawnTransform.position;
        player.motor.SetPositionAndRotation(PlayerSpawnTransform.position, PlayerSpawnTransform.rotation, true);
        player.PlayerSetActive(true);
        DeactivateTransport();
        IsPlayerIn = false;
        TransportCamera.gameObject.SetActive(false);

        CanvasManager.instance.InteracteButton.gameObject.SetActive(false);
        CanvasManager.instance.InteracteButton.onClick.RemoveAllListeners();
        player.SwitchPlayerState(Player.PlayerState.Idle);

    }
    private void OnDestroy()
    {
        if (player != null)
        {
            if (player.currentNearTransport == this)
            {
                if (Geekplay.Instance.mobile)
                {
                    CanvasManager.instance.InteracteButton.gameObject.SetActive(false);
                    CanvasManager.instance.InteracteButton.onClick.RemoveAllListeners();
                }
                else
                {
                    HideEnterInstruction();
                }
            }
        }
    }
    protected virtual void ActivateTransport()
    {
    }
    protected virtual void DeactivateTransport()
    {
    }
}
