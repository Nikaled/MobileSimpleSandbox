using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using alelavoie;

public class HelicopterEnterController : EnterController
{
    [SerializeField] AHC _helicopterController;
    protected override void ActivateTransport()
    {
        CanvasManager.instance.ShowHelicopterInstruction(true);
        CanvasManager.instance.ShowHelicopterMobileInstruction(true);
        _helicopterController.enabled = true;
        TransportCamera.farClipPlane = 500;
        if (Geekplay.Instance.mobile)
        {
        _helicopterController.MyInitializeButtons();
        HelicopterButtons.instance.GetOutButton.onClick.AddListener(delegate { GetOutTransport(); });
        }
    }
    public override void GetOutTransport()
    {
        base.GetOutTransport();

    }
    protected override void DeactivateTransport()
    {
        CanvasManager.instance.ShowHelicopterInstruction(false);
        CanvasManager.instance.ShowHelicopterMobileInstruction(false);
        if (Geekplay.Instance.mobile)
        {
             _helicopterController.MyClearButtons();
            HelicopterButtons.instance.GetOutButton.onClick.RemoveAllListeners();
        }
        _helicopterController.OnHeliExit();
        _helicopterController.enabled = false;
    }
}
