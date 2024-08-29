using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    private Image Crosshair;
    [SerializeField] private LayerMask aimColliderLayerMask;
    [HideInInspector] public Vector3 CrosshairWorldPosition;
    public Vector3 AimDirection;
    [SerializeField] private Transform TankProjectileSpawnPoint;
    [SerializeField] ShootingProjectile TankProjectile;
    [SerializeField] GameObject TankHpCollider;

    [SerializeField] AudioSource ShootSource;
    private void Start()
    {
         Crosshair = CanvasManager.instance.Crosshair;
    }
    void Update()
    {
        if (Geekplay.Instance.mobile == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
            }
        }
    }
    public void ActivateTankShooting(bool Is)
    {
        TankHpCollider.SetActive(!Is);
    }
    public void Fire()
    {
        ShootSource.Play();
        Ray ray = Camera.main.ScreenPointToRay(Crosshair.transform.position);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 5000, aimColliderLayerMask))
        {
            CrosshairWorldPosition = raycastHit.point;
        }
        else
        {
            CrosshairWorldPosition = ray.GetPoint(1998);
        }

        AimDirection = (CrosshairWorldPosition - TankProjectileSpawnPoint.position).normalized;
        Instantiate(TankProjectile, TankProjectileSpawnPoint.position, Quaternion.LookRotation(AimDirection, Vector3.up));
    }
}
