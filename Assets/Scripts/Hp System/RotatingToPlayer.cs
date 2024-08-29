using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingToPlayer : MonoBehaviour
{
    public static Transform target;

    private void Start()
    {
        if(target == null)
        {
        target = Player.instance.transform;
        }
    }
    void Update()
    {
        if (target != null)
        {
            Vector3 targetPostition = new Vector3(target.position.x,
                                       this.transform.position.y,
                                       target.position.z);
           transform.LookAt(targetPostition);
        }
    }
}
