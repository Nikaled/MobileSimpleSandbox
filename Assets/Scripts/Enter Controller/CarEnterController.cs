using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;

public class CarEnterController : EnterController
{
    [SerializeField] private Transform FirstViewCameraTransform;
    [SerializeField] VehicleControl vehicleControl;
    [Header("Tank Only")]
    [SerializeField] TankShooting tankShooting;
    public bool IsTank;
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Q) && IsPlayerIn && Player.instance.AdWarningActive == false)
        {
            TransportCamera.transform.position = FirstViewCameraTransform.position;
        }
    }
    protected override void ActivateTransport()
    {
        CanvasManager.instance.ShowControlCarInstruction(true, IsTank);
        vehicleControl.gameObject.transform.position = new Vector3(vehicleControl.gameObject.transform.position.x, vehicleControl.gameObject.transform.position.y + 1f, vehicleControl.gameObject.transform.position.z);
        vehicleControl.enabled = true;
        vehicleControl.GetComponent<VehicleControl>().activeControl = true;
        vehicleControl.OnCarEnter();
        if (Geekplay.Instance.mobile)
        {
            CanvasManager.instance.ShowCarMobileInstruction(true);
            vehicleControl.MyInitializeButtons();
            CarButtons.instance.GetOutButton.onClick.AddListener(delegate { GetOutTransport(); });
            CarButtons.instance.ShootButton.gameObject.SetActive(false);
        }
        if (tankShooting != null)
        {
            tankShooting.enabled = true;
            tankShooting.ActivateTankShooting(true);
            if (Geekplay.Instance.mobile)
            {
                CarButtons.instance.ShootButton.onClick.AddListener(delegate { tankShooting.Fire(); });
                CarButtons.instance.ShootButton.gameObject.SetActive(true);
            }
        }
    }
    protected override void DeactivateTransport()
    {
        CanvasManager.instance.ShowControlCarInstruction(false, IsTank);
        vehicleControl.carSetting.brakePower = float.MaxValue;
        vehicleControl.GetComponent<VehicleControl>().activeControl = false;
        vehicleControl.GetComponent<VehicleControl>().OnCarQuit();
        if (Geekplay.Instance.mobile)
        {
            vehicleControl.MyClearButtons();
            CarButtons.instance.GetOutButton.onClick.RemoveAllListeners();
            CanvasManager.instance.ShowCarMobileInstruction(false);
            CarButtons.instance.ShootButton.gameObject.SetActive(false);
        }
        if (tankShooting!=null)
        {
            tankShooting.ActivateTankShooting(false);
            tankShooting.enabled = false;
            if (Geekplay.Instance.mobile)
            {
                CarButtons.instance.ShootButton.onClick.RemoveAllListeners();
            }
        }
        //vehicleControl.enabled = false;
    }
}
