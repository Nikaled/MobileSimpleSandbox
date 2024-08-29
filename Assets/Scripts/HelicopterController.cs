using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    // Сделать интерфейс с подсказками по управлению при посадке в вертолет
    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private float _responsiveness = 500f;
    [SerializeField] private float _throttleAmt = 25f;
    [SerializeField] private float _movementMultiplier = 1.3f;
    private float _throttle;

    private float _roll;
    private float _pitch;
    private float _yaw;

    [SerializeField] private float _rotorSpeedModifier = 10f;
    [SerializeField] private Transform _rotorsTransform;
    private void Update()
    {
        HandleInputs();
        _rotorsTransform.Rotate(Vector3.up * _throttle * _rotorSpeedModifier);
    }
    private void FixedUpdate()
    {
        _rigidbody.AddForce(transform.up * _throttle* _movementMultiplier, ForceMode.Impulse);
        _rigidbody.AddTorque(transform.right * _pitch * _responsiveness* _movementMultiplier);
        _rigidbody.AddTorque(-transform.forward * _roll * _responsiveness* _movementMultiplier);
        _rigidbody.AddTorque(transform.up * _yaw * _responsiveness* _movementMultiplier);
    }
    private void HandleInputs()
    {
        _roll = Input.GetAxis("Roll");
        _pitch = Input.GetAxis("Pitch") * 4;
        _yaw = Input.GetAxis("Yaw");

        if (Input.GetKey(KeyCode.Space))                                         
        {
            _throttle += Time.deltaTime * _throttleAmt;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            _throttle -= Time.deltaTime * _throttleAmt;
        }
        _throttle = Mathf.Clamp(_yaw,_throttle, _throttleAmt);
    }
}
