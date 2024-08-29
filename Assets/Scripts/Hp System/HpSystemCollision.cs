using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpSystemCollision : MonoBehaviour
{
    [SerializeField] HpSystem hpSystem;

    private void Start()
    {
        if(hpSystem == null)
        {
            hpSystem = transform.parent.GetComponentInChildren<HpSystem>();
        }
    }
    public void TakeDamage(int DamageCount)
    {
        if(hpSystem !=null)
        hpSystem.TakeDamage(DamageCount);
        //else
        //{
        //    hpSystem = GetComponentInChildren<HpSystem>();
        //    if(hpSystem != null)
        //    {
        //        hpSystem.TakeDamage(DamageCount);
        //    }
        //}
    }
}
