using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshChecker : MonoBehaviour
{
    public IMoveableAgent citizen;
    [SerializeField] LayerMask NotAvailableToGoMask;

    private void OnTriggerEnter(Collider other)
    {
        if ((NotAvailableToGoMask & (1 << other.gameObject.layer)) != 0)
        {
            if(other.CompareTag("Road")== false)
            {
                Debug.Log("—фера столкнулась со зданием:" + other.gameObject.name);
                if (citizen != null)
                    citizen.FindNewDestination();
            }
        }
    }
}
