using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitAnimation : ObjectInteraction
{
    [SerializeField] protected Transform SitTransform;
    protected override void ActivateObject()
    {
        base.ActivateObject();
        Player.instance.motor.SetPositionAndRotation(SitTransform.position, SitTransform.rotation);
        Player.instance.motor.enabled = false;
        Player.instance.SwitchPlayerState(Player.PlayerState.Sitting);
        EnableSitAnimation(true);
    }
    protected virtual void EnableSitAnimation(bool Is)
    {
        Player.instance.animator.SetBool("Sitting", Is);
    }
    protected override void DeactivateObject()
    {
        base.DeactivateObject();
        EnableSitAnimation(false);
        Player.instance.motor.enabled = true;
        Player.instance.SwitchPlayerState(Player.PlayerState.Idle);

    }
}
