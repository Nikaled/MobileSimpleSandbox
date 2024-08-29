using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private KinematicCharacterController.Examples.ExampleCharacterCamera playerCamera;
    public void SwitchCameraView()
    {
        playerCamera.TargetDistance = (playerCamera.TargetDistance == 0f) ? playerCamera.DefaultDistance : 0f;
    }
}
