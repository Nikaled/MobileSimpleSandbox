using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAnimalMovement : CitizenMovement
{
    public bool LeftSide;
    protected override void Start()
    {
        checker = Instantiate(TutorialAnimalMovementManager.instance.Checker, gameObject.transform.position, Quaternion.identity);
        checker.citizen = this;
        checker.GetComponent<SphereCollider>().enabled = true;
        FindNewDestination();
        hpSystem.OnDied += CitizenDie;
    }
    public override void FindNewDestination()
    {
        if(LeftSide == false)
        {
        TutorialAnimalMovementManager.instance.MoveCheckerToNewPoint(checker.gameObject);
        }
        else
        {
        TutorialAnimalMovementManager.instance.MoveCheckerToNewPointToLeft(checker.gameObject);
        }
    }
}
