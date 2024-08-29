using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CitizenMovement : MonoBehaviour, IMoveableAgent
{
    protected bool isMoving;
    public NavMeshAgent agent;
    public Animator animator;
    public Transform CurrentDestination;
    protected Vector3? CurrentDest;
    protected NavMeshChecker checker;
    private bool IsDying;
    [SerializeField] protected HpSystem hpSystem;
    public float DieAnimationTime = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if(agent != null)
            {
                agent.enabled = false;
                agent.enabled = true;
            }      
        }
    }
    protected virtual void Start()
    {
        checker = Instantiate(CitizenNavMeshManager.instance.Checker, gameObject.transform.position, Quaternion.identity);
        checker.citizen = this;
        checker.GetComponent<SphereCollider>().enabled = true;
        FindNewDestination();
        hpSystem.OnDied += CitizenDie;
    }
    public void MoveToPosition(Vector3 DestinationPosition)
    {

        agent.SetDestination(DestinationPosition);
        animator.SetBool("IsWalk", true);
        animator.SetFloat("Speed_f", 1);
    }
    private void StopMoving()
    {
        animator.SetBool("IsWalk", false);
        animator.SetFloat("Speed_f", 0);
    }
    protected void CitizenDie()
    {
        if(gameObject.GetComponent<CapsuleCollider>() != null)
        {
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
        }
        agent.speed = 0;
        animator.SetBool("IsWalk", false);
        animator.SetTrigger("Die");
        IsDying = true;
        agent.SetDestination(transform.position);
        StartCoroutine(WaitForDyingAnimation());
    }
    private IEnumerator WaitForDyingAnimation()
    {
        hpSystem.enabled = false;
        yield return new WaitForSeconds(DieAnimationTime);
        Destroy(hpSystem.RootObject);
    }
    public virtual void FindNewDestination()
    {
        CitizenNavMeshManager.instance.MoveCheckerToNewPoint(checker.gameObject);
    }
    void Update()
    {
        if(IsDying == false)
        {
                MoveToPosition(checker.transform.position);
                if (Vector3.Distance(gameObject.transform.position, checker.transform.position) < 9)
                {
                    StopMoving();
                    FindNewDestination();
                }
              
        }

    }
}
