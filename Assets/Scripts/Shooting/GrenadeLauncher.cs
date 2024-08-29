using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrenadeLauncher : MonoBehaviour
{
    public Transform LaunchPoint;
    public GameObject Projectile;
    public float LaunchSpeed = 111f;

    [Header("Trajectory Display")]
    public LineRenderer lineRenderer;
    public int linePoints = 500;
    public float timeIntervalinPoints = 0.01f;

    private Vector3 crossPosition;
    private Vector3 aimDirection;

    public float GrenadeMass = 1;
    public void GrenadeInput()
    {

        if (Player.instance.InterfaceActive)
        {
            return;
        }
        crossPosition = PlayerShooting.instance.CrosshairWorldPosition;
        aimDirection = (crossPosition - LaunchPoint.position).normalized;

        Debug.Log("crossPosition:" + crossPosition);
        if (lineRenderer != null)
        {
            DrawTrajectory();
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }
        Player.instance.RotatePlayerOnShoot(aimDirection);
    }
    public void LaunchGrenade()
    {
        var _projectile = Instantiate(Projectile, LaunchPoint.transform.position, LaunchPoint.transform.rotation);
        //_projectile.GetComponent<Rigidbody>().velocity = LaunchSpeed * LaunchPoint.up;
        _projectile.GetComponent<Rigidbody>().velocity = LaunchSpeed * Camera.main.transform.forward + (Vector3.up * 10);
        _projectile.GetComponent<CapsuleCollider>().enabled = true;
        _projectile.GetComponent<Rigidbody>().useGravity = true;
        _projectile.GetComponent<Grenade>().OnLaunch();
        lineRenderer.enabled = false;
        Player.instance.RotatePlayerOnShoot(aimDirection);
        PlayerShooting.instance.FireAudioSource.clip = PlayerShooting.instance.GrenadeThrowSound;
        PlayerShooting.instance.FireAudioSource.Play();
    }
    public void ClearTrajectory()
    {
        lineRenderer.enabled = false;
    }
    public void DrawTrajectory()
    {
        Vector3 origin = LaunchPoint.position;
        //Vector3 startVelocity = LaunchSpeed * Camera.main.transform.forward;
        Vector3 startVelocity = LaunchSpeed * Camera.main.transform.forward + (Vector3.up * 10);
        lineRenderer.positionCount = linePoints;
        float time = 0;
        for (int i = 0; i < linePoints; i++)
        {
            var y = (startVelocity.y * time) + (Physics.gravity.y / 2 * time * time);
            var x = (startVelocity.x * time) + (Physics.gravity.x / 2 * time * time);
            var z = (startVelocity.z * time) + (Physics.gravity.z / 2 * time * time);
            Vector3 point = new Vector3(x, y, z);
            lineRenderer.SetPosition(i, origin + point);
            time += timeIntervalinPoints;
        }
    }
}
