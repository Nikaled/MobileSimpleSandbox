using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackHitbox : MonoBehaviour
{
    public List<HpSystemCollision> collisions = new();
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<HpSystemCollision>()!=null)
        {
            collisions.Add(other.gameObject.GetComponent<HpSystemCollision>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<HpSystemCollision>() != null)
        {
            collisions.Remove(other.gameObject.GetComponent<HpSystemCollision>());
        }
    }
    public List<HpSystemCollision> GetEnemies()
    {
        return collisions;
    }
    public void EndAttack()
    {
        GetComponent<BoxCollider>().enabled = false;
        collisions = new();
        GetComponent<BoxCollider>().enabled = true;
    }
}
