using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDamage : MonoBehaviour
{
    public Rigidbody carRb;
    float CarRbVelocityToKill = 100;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Citizen"))
        {
            Debug.Log("Врезался в жителя или зверя");
            if (carRb.velocity.sqrMagnitude > CarRbVelocityToKill)
            {
                other.gameObject.GetComponent<HpSystemCollision>().TakeDamage(50);
            }
        }
    }
}
