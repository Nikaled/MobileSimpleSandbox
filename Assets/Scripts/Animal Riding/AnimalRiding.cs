using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalRiding : MonoBehaviour
{
    [SerializeField] Animator AnimalAnimator;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] CitizenMovement citizenMovement;
    private bool IsMoving;
    [SerializeField] public Transform PlayerSittingTransform;
    [SerializeField] Vector3 OriginScale;
    [SerializeField] float CapsuleOnAnimalHeight;
    [Header("Little Animal")]
    [SerializeField] bool LittleAnimal;
    [SerializeField] public Transform PlayerSittingTransformOnMove;
    private GameObject characterMesh;
    private Vector3 charDefaultPosition;
    public void ActivateRiding()
    {
        Player player = Player.instance;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        AnimalAnimator.SetFloat("Speed_f", 0);
        float YScaleDifference = gameObject.transform.localScale.y / OriginScale.y;
        if (YScaleDifference > 1)
        {
            YScaleDifference *= 0.8f;
        }
        else if (YScaleDifference < 1)
        {
            YScaleDifference *= 1.3f;
        }
        player.motor.CapsuleHeight = CapsuleOnAnimalHeight * YScaleDifference;
        player.motor.ValidateData();
        player.animationPlayer.RidingAnimal = true;

        Vector3 SitPositionIncludeCharacterHeight = new Vector3(PlayerSittingTransform.position.x, PlayerSittingTransform.position.y - 0.3f, PlayerSittingTransform.position.z);
        player.motor.SetPositionAndRotation(SitPositionIncludeCharacterHeight, PlayerSittingTransform.rotation, true);
        gameObject.transform.parent = Player.instance.gameObject.transform;
        player.animator.SetBool("IsRun", false);
        player.animator.SetBool("Sitting", true);

        //gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        citizenMovement.enabled = false;
        agent.enabled = false;
    }
    public void DeactivateRiding()
    {
        Player player = Player.instance;
        player.motor.CapsuleHeight =1.83f;
        player.motor.ValidateData();
        gameObject.transform.parent = null;
        player.animationPlayer.RidingAnimal = false;
        player.animator.SetBool("Sitting", false);

        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        agent.enabled = true;
        agent.enabled = false;
        agent.enabled = true;
        citizenMovement.enabled = true;
        citizenMovement.FindNewDestination();
    }

    void Update()
    {
        if (Player.instance.AdWarningActive)
        {
            return;
        }
        if (Geekplay.Instance.mobile == false)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            IsMoving = h != 0 || v != 0;

            if (IsMoving)
            {
                AnimalAnimator.SetFloat("Speed_f", 1);
                //if (LittleAnimal)
                //{
                //    characterMesh.transform.position = PlayerSittingTransformOnMove.transform.position;
                //}
            }
            else
            {
                AnimalAnimator.SetFloat("Speed_f", 0);

                //if (LittleAnimal)
                //{
                //    characterMesh.transform.position = charDefaultPosition;
                //}
            }
        }
        else
        {
            float h = Player.instance.examplePlayer.FixedJoystick.Horizontal;
            float v = Player.instance.examplePlayer.FixedJoystick.Vertical;
            IsMoving = h != 0 || v != 0;

            if (IsMoving)
            {
                AnimalAnimator.SetFloat("Speed_f", 1);
            }
            else
            {
                AnimalAnimator.SetFloat("Speed_f", 0);
            }
        }
    }
}
