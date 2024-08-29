using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TutorialCitizenMovementBuilding : CitizenMovement
{
    [SerializeField] Transform BuildSpawnPoint;
    private int BuildInterval = 5;
    private float TimeBuildingPlaced;
    public bool LeftSide;
    protected override void Start()
    {
        checker = Instantiate(TutorialCitizenBuildManager.instance.Checker, gameObject.transform.position, Quaternion.identity);
        checker.citizen = this;
        checker.GetComponent<SphereCollider>().enabled = true;
        FindNewDestination();
        hpSystem.OnDied += CitizenDie;
    }
    public override void FindNewDestination()
    {
        if (LeftSide)
        {
            TutorialCitizenBuildManager.instance.MoveCheckerToNewPointLeftSide(checker.gameObject);
        }
        else
        {
            TutorialCitizenBuildManager.instance.MoveCheckerToNewPoint(checker.gameObject);
        }
        BuildRequest();
    }
    private void BuildRequest()
    {
        if (Time.time - TimeBuildingPlaced > BuildInterval)
        {
            TimeBuildingPlaced = Time.time;
            TutorialCitizenBuildManager.instance.BuildObject(BuildSpawnPoint.position);
        }
    }
}