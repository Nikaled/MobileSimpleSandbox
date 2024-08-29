using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HeneGames.Airplane;

public class PlaneEnterController : EnterController
{
    [SerializeField] private Transform _rotorsTransform;
    [SerializeField] SimpleAirPlaneController _planeController;

    public bool IsStartFlying;
    public float MobileHoldButtonTime;
     float startTime;
    float timeToUp = 1.5f;
    protected override void ActivateTransport()
    {
        _planeController.enterController = this;
        CanvasManager.instance.ShowPlaneInstruction(true);
        _planeController.enabled = true;
        _planeController.airplaneState = SimpleAirPlaneController.AirplaneState.Landing;
        //_planeController.GetComponent<Rigidbody>().isKinematic = true;
        _planeController.GetComponent<Rigidbody>().isKinematic = true;
        _planeController.GetComponent<Rigidbody>().useGravity = false;
        TransportCamera.farClipPlane = 500;
        if (Geekplay.Instance.mobile)
        {
            CanvasManager.instance.ShowPlaneMobileInstruction(true);
            _planeController.MyInitializeButtons();
            PlaneButtons.instance.GetOutButton.onClick.RemoveAllListeners();
            PlaneButtons.instance.GetOutButton.onClick.AddListener(delegate { GetOutTransport(); });
        }
    }

    protected override void DeactivateTransport()
    {
        if (Geekplay.Instance.mobile)
        {
            _planeController.MyClearButtons();
            PlaneButtons.instance.GetOutButton.onClick.RemoveAllListeners();
            CanvasManager.instance.ShowPlaneMobileInstruction(false);
        }
        CanvasManager.instance.ShowPlaneInstruction(false);
        _planeController.airplaneState = SimpleAirPlaneController.AirplaneState.Landing;
        _planeController.GetComponent<Rigidbody>().isKinematic = false;
        _planeController.GetComponent<Rigidbody>().useGravity = true;
        _planeController.enabled = false;
    }
    protected override void Update()
    {
        base.Update();
        if(Geekplay.Instance.mobile == false && Player.instance.AdWarningActive == false)
        {
            PCPlaneInput();
        }
        else
        {
            MobilePlaneInput();
        }
      
    }
    private void  MobilePlaneInput()
    {
        if(PlaneUpButton.instance == null)
        {
            return;
        }
        if (IsPlayerIn)
        {
            if (_planeController.airplaneState == SimpleAirPlaneController.AirplaneState.Landing)
            {
                if (PlaneUpButton.instance.isPressed)
                {
                    _planeController.airplaneState = SimpleAirPlaneController.AirplaneState.Takeoff;
                }
            }
            if (_planeController.airplaneState != SimpleAirPlaneController.AirplaneState.Flying)
            {
                if (PlaneUpButton.instance.isPressedThisFrame)
                {
                    startTime = Time.time;
                }
                if (PlaneUpButton.instance.isPressed && Time.time - startTime > timeToUp)
                {
                    if (_planeController.airplaneState == SimpleAirPlaneController.AirplaneState.Takeoff)
                    {
                    _planeController.airplaneState = SimpleAirPlaneController.AirplaneState.Flying;
                    }
                    _planeController.UpThePlaneFromGround();
                    Debug.Log((Time.time - startTime).ToString("00:00.00"));
                }

            }
            if (PlaneUpButton.instance.isPressed && _planeController.airplaneState == SimpleAirPlaneController.AirplaneState.Flying)
            {
                _planeController.UpThePlane();
            }
            if (_planeController.airplaneState != SimpleAirPlaneController.AirplaneState.Landing)
            {
                if (_rotorsTransform != null)
                    _rotorsTransform.Rotate(Vector3.forward * _planeController.currentSpeed);
            }
        }
    }
    private void PCPlaneInput()
    {
        if (IsPlayerIn)
        {
            if (_planeController.airplaneState == SimpleAirPlaneController.AirplaneState.Landing)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    IsStartFlying = true;
                    _planeController.airplaneState = SimpleAirPlaneController.AirplaneState.Takeoff;
                }
            }
            if (_planeController.airplaneState != SimpleAirPlaneController.AirplaneState.Flying)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    startTime = Time.time;
                }
                if (Input.GetKey(KeyCode.Space) && Time.time - startTime > timeToUp)
                {
                    if (_planeController.airplaneState == SimpleAirPlaneController.AirplaneState.Takeoff)
                    {
                        _planeController.airplaneState = SimpleAirPlaneController.AirplaneState.Flying;
                    }
                    _planeController.UpThePlaneFromGround();
                    Debug.Log((Time.time - startTime).ToString("00:00.00"));
                }

            }
            if (Input.GetKey(KeyCode.Space) && _planeController.airplaneState == SimpleAirPlaneController.AirplaneState.Flying)
            {
                _planeController.UpThePlane();
            }
            if (_planeController.airplaneState != SimpleAirPlaneController.AirplaneState.Landing)
            {
                if (_rotorsTransform != null)
                    _rotorsTransform.Rotate(Vector3.forward * _planeController.currentSpeed);
            }
        }
    }

}
