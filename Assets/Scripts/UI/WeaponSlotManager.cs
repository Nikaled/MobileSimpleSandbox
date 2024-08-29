using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    [SerializeField] WeaponSlot[] WeaponSlots;
    private void Awake()
    {
        
    }
    private void Start()
    {
        OnWeaponSwitched(1);
        Player.instance.SwitchedWeapon += OnWeaponSwitched;
    }
    private void OnEnable()
    {
        if (Geekplay.Instance != null)
            Player.instance.SwitchedWeapon += OnWeaponSwitched;
    }

    private void OnDisable()
    {
        Player.instance.SwitchedWeapon -= OnWeaponSwitched;
    }
    private void OnWeaponSwitched(int PressedNumber)
    {
        for (int i = 0; i < WeaponSlots.Length; i++)
        {
            WeaponSlots[i].WeaponIsInactive();
        }
        int ActiveWeaponIndex = PressedNumber - 1;
        WeaponSlots[ActiveWeaponIndex].WeaponIsActive();
    }
}
