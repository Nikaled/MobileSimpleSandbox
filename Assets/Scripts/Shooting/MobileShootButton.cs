using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class MobileShootButton : MonoBehaviour, IUpdateSelectedHandler, IPointerDownHandler, IPointerUpHandler
{

    public bool isPressed;

    public static MobileShootButton instance;
    public Action OnHolding;
    private void Start()
    {
        instance = this;
    }

    // Start is called before the first frame update
    public void OnUpdateSelected(BaseEventData data)
    {
        if (isPressed && Player.instance.CurrentWeapon == Player.WeaponType.Gun)
        {
            Player.instance.MobileFireInput();
        }
        if (isPressed && Player.instance.CurrentWeapon == Player.WeaponType.Grenade)
        {
            //Player.instance.MobileFireInput();
            Player.instance.AimingGrenadeOnMobile();
        }
        if (isPressed)
        {
            OnHolding?.Invoke();
        }
        
    }
    public void OnPointerDown(PointerEventData data)
    {
        isPressed = true;
        if (Player.instance.CurrentWeapon == Player.WeaponType.Grenade)
        {
            Player.instance.AimingGrenadeOnMobile();
        }
    }
    public void OnPointerUp(PointerEventData data)
    {
        isPressed = false;
        if (Player.instance.CurrentWeapon == Player.WeaponType.Grenade)
        {
            Player.instance.LaunchGrenadeOnMobile();
        }
        
    }
}
