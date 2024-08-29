using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAnimalRider : MonoBehaviour
{
    [SerializeField] Animator animator;
    void Start()
    {
        animator.SetBool("Sitting", true);
    }
}
