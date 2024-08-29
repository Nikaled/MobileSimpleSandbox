using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using KinematicCharacterController.Examples;
using UnityEngine.UI;

namespace KinematicCharacterController.Examples
{
    public class ExamplePlayer : MonoBehaviour
    {
        [SerializeField] private GameObject adPanel;

        public ExampleCharacterController Character;
        public ExampleCharacterCamera CharacterCamera;

        private const string MouseXInput = "Mouse X";
        private const string MouseYInput = "Mouse Y";
        private const string MouseScrollInput = "Mouse ScrollWheel";
        private const string HorizontalInput = "Horizontal";
        private const string VerticalInput = "Vertical";

        public Animator playerAnimator;
        private bool IsJumpTrue;
        public Joystick FixedJoystick;
        public bool Mobile = true;
        public bool PC = false;
        public bool IsCursorLocked;
        public bool MyLockOnShoot; // my field
        private void Start()
        {
            if (IsCursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            if(Geekplay.Instance.mobile == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            // Tell camera to follow transform
            CharacterCamera.SetFollowTransform(Character.CameraFollowPoint);

            // Ignore the character's collider(s) for camera obstruction checks
            CharacterCamera.IgnoredColliders.Clear();
            CharacterCamera.IgnoredColliders.AddRange(Character.GetComponentsInChildren<Collider>());
        }

        public void LockCursor(bool Is)
        {
            Debug.Log(Is);
            if(Geekplay.Instance != null)
            {
                if (Geekplay.Instance.mobile)
                {
                    Cursor.lockState = CursorLockMode.None;
                    return;
                }
            }
            if(Is)
            Cursor.lockState = CursorLockMode.Locked;
            else
            Cursor.lockState = CursorLockMode.None;
        }
        private void Update()
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    //Cursor.lockState = CursorLockMode.Locked;
            //}

            HandleCharacterInput();
        }

        private void LateUpdate()
        {
            // Handle rotating the camera along with physics movers
            if (CharacterCamera.RotateWithPhysicsMover && Character.Motor.AttachedRigidbody != null)
            {
                CharacterCamera.PlanarDirection = Character.Motor.AttachedRigidbody.GetComponent<PhysicsMover>().RotationDeltaFromInterpolation * CharacterCamera.PlanarDirection;
                CharacterCamera.PlanarDirection = Vector3.ProjectOnPlane(CharacterCamera.PlanarDirection, Character.Motor.CharacterUp).normalized;
            }

            HandleCameraInput();
        }

        private void HandleCameraInput()
        {
            Vector3 lookInputVector = Vector3.zero;



            // Create the look input vector for the camera
            if (Geekplay.Instance.mobile == false)
            {
                float mouseLookAxisUp = Input.GetAxisRaw(MouseYInput);
                float mouseLookAxisRight = Input.GetAxisRaw(MouseXInput);
                 lookInputVector = new Vector3(mouseLookAxisRight, mouseLookAxisUp, 0f);
                if (Cursor.lockState != CursorLockMode.Locked)
                {
                    lookInputVector = Vector3.zero;
                }
            }
            else
            {
                lookInputVector = new Vector2(SwipeDetector.instance.swipeDelta.x * 2.5f, SwipeDetector.instance.swipeDelta.y * 2.5f);
                //SwipeDetector.instance.swipeDelta = Vector2.zero;
            }

            // Prevent moving the camera while the cursor isn't locked

            // Input for zooming the camera (disabled in WebGL because it can cause problems)
            float scrollInput = -Input.GetAxis(MouseScrollInput);
#if UNITY_WEBGL
        scrollInput = 0f;
#endif

            // Apply inputs to the camera
            CharacterCamera.UpdateWithInput(Time.deltaTime, scrollInput, lookInputVector);

            // Handle toggling zoom level
            if (Input.GetKeyDown(KeyCode.Q) && Player.instance.AdWarningActive == false)
            {
                SwitchCamera();
                //CharacterCamera.TargetDistance = (CharacterCamera.TargetDistance == 0f) ? CharacterCamera.DefaultDistance : 0f;
            }
        }
        public void SwitchCamera()
        {
            if(Player.instance.InterfaceActive == false)
            CharacterCamera.TargetDistance = (CharacterCamera.TargetDistance == 0f) ? CharacterCamera.DefaultDistance : 0f;
        }
        public void JumpIsTrue()
        {
            IsJumpTrue = true;
        }
        private void HandleCharacterInput()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

            if (Player.instance.InterfaceActive)
            {
                return;
            }
                // Build the CharacterInputs struct
                if (Mobile)
            {
                if (!MyLockOnShoot)
                {
                    characterInputs.MoveAxisForward = FixedJoystick.Vertical;
                    characterInputs.MoveAxisRight = FixedJoystick.Horizontal;
                }
                characterInputs.CameraRotation = CharacterCamera.Transform.rotation;
                characterInputs.JumpDown = IsJumpTrue;
                IsJumpTrue = false;
                //characterInputs.CrouchDown = Input.GetKeyDown(KeyCode.C);
                //characterInputs.CrouchUp = Input.GetKeyUp(KeyCode.C);
            }
            if (PC)
            {
                if (!MyLockOnShoot)
                {
                    characterInputs.MoveAxisForward = Input.GetAxisRaw(VerticalInput);
                    characterInputs.MoveAxisRight = Input.GetAxisRaw(HorizontalInput);
                }

                characterInputs.CameraRotation = CharacterCamera.Transform.rotation;
                characterInputs.JumpDown = Input.GetKeyDown(KeyCode.Space);
                //characterInputs.CrouchDown = Input.GetKeyDown(KeyCode.C);
                //characterInputs.CrouchUp = Input.GetKeyUp(KeyCode.C);
            }
            // Apply inputs to character
            Character.SetInputs(ref characterInputs);
           
        }
    }
}