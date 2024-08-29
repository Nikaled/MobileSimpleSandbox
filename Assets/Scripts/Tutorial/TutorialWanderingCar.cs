using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class TutorialWanderingCar : MonoBehaviour, IMoveableAgent
{
    protected NavMeshChecker checker;
    [SerializeField] NavMeshAgent agent;
    void Start()
    {
        checker = Instantiate(CitizenNavMeshManager.instance.Checker, gameObject.transform.position, Quaternion.identity);
        checker.citizen = this;
        checker.GetComponent<SphereCollider>().enabled = true;
        FindNewDestination();
    }
    public void MoveToPosition(Vector3 DestinationPosition)
    {
        agent.SetDestination(DestinationPosition);
    }
    public virtual void FindNewDestination()
    {
        TutorialAnimalMovementManager.instance.MoveCheckerToNewPointForCar(checker.gameObject);
    }
    void Update()
    {

            MoveToPosition(checker.transform.position);
            if (Vector3.Distance(gameObject.transform.position, checker.transform.position) < 18)
            {
                FindNewDestination();
            }
    }
}
