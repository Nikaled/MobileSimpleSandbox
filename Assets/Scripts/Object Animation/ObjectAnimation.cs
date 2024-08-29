using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAnimation : ObjectInteraction
{
    [SerializeField] Animator animator;
    [SerializeField] bool Sound;
    [SerializeField] bool Animation = true;
    [SerializeField] AudioClip clip;
    [SerializeField] AudioSource source;

    protected override void ActivateObject()
    {
        base.ActivateObject();
        if (Animation)
        {
            animator.SetBool("Activated", true);
            Debug.Log("Activated - " + animator.GetBool("Activated"));
        }
        if (Sound)
        {
            source.PlayOneShot(clip);
        }
    }
    protected override void DeactivateObject()
    {
        base.DeactivateObject();
        if (Animation)
        {
            animator.SetBool("Activated", false);
            Debug.Log("Activated - " + animator.GetBool("Activated"));
        }
    }
}
