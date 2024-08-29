using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrel : SitAnimation
{
    [SerializeField] ShootingProjectile bulletPrefab;
    [SerializeField] Transform BulletSpawnPoint;
    [SerializeField] GameObject TurrelParentToRotate;
    [SerializeField] GameObject TurrelModel;
    [SerializeField] BoxCollider[] TurrelColliders;
    private Queue<GameObject> projectiles;
    float GunTimer;
    float GunShootInterval = 0.025f;
    int gunCount;
    bool Reloading;
    Vector3 aimDirection;
    bool IsPlayerIn;
    protected override void ActivateObject()
    {
        TurrelModel.transform.parent = TurrelParentToRotate.transform;
        TurrelModel.transform.rotation = TurrelParentToRotate.transform.rotation;
        TurrelModel.transform.Rotate(0, 180, 0);
        base.ActivateObject();
        //TurrelParentToRotate.transform.rotation = new Quaternion(0, 0, 0, 0);
       
        Player.instance.transform.parent = SitTransform;
        for (int i = 0; i < TurrelColliders.Length; i++)
        {
            TurrelColliders[i].enabled = false;
        }
        IsPlayerIn = true;
        projectiles = new();
        if (Geekplay.Instance.mobile)
        {
            MobileShootButton.instance.OnHolding += Fire;
        }
    }
    protected override void EnableSitAnimation(bool Is)
    {
        Player.instance.animator.SetBool("PistolAiming", Is);
    }
    protected override void DeactivateObject()
    {
        base.DeactivateObject();
        IsPlayerIn = false;
        for (int i = 0; i < TurrelColliders.Length; i++)
        {
            TurrelColliders[i].enabled = true;
        }
        Player.instance.transform.parent = Player.instance.examplePlayer.transform;
        if (Geekplay.Instance.mobile)
        {
            MobileShootButton.instance.OnHolding -= Fire;
        }
    }
    protected override void Update()
    {
       
        base.Update();
        if (!IsPlayerIn)
        {
            return;
        }
        aimDirection = (PlayerShooting.instance.CrosshairWorldPosition - BulletSpawnPoint.position).normalized;
        RotateTurrelToCamera();

        if (Input.GetMouseButton(0))
        {
            if (Geekplay.Instance.mobile == false)
            {
                Fire();
            }      
        }       
    }
    private void Fire()
    {
        if (projectiles.Count <= 0)
        {
            SpawnPool();
        }
        if (Time.time - GunTimer > GunShootInterval)
        {
            Reloading = false;
        }
        if (Reloading == false)
        {
            Reloading = true;

            //ShootingProjectile proj = Instantiate(bulletPrefab, BulletSpawnPoint.position, Quaternion.LookRotation(aimDirection, Vector3.up));
            var ShootingProj = projectiles.Dequeue();
            ShootingProj.transform.SetPositionAndRotation(BulletSpawnPoint.transform.position, Quaternion.LookRotation(aimDirection, Vector3.up));
            ShootingProj.gameObject.SetActive(true);
            GunTimer = Time.time;
            gunCount += 1;
            if (gunCount > 2)
            {
                PlayerShooting.instance.FireAudioSource.clip = PlayerShooting.instance.GunSound;
                PlayerShooting.instance.FireAudioSource.Play();
                gunCount = 0;
            }
        }
    }
    private void RotateTurrelToCamera()
    {
        Vector3 LookPos = new Vector3(PlayerShooting.instance.CrosshairWorldPosition.x, TurrelParentToRotate.transform.position.y, PlayerShooting.instance.CrosshairWorldPosition.z);
        TurrelParentToRotate.transform.LookAt(LookPos);    
    }
    private void SpawnPool()
    {
        for (int i = 0; i < 41; i++)
        {
            var bullet = Instantiate(bulletPrefab, BulletSpawnPoint.transform.position, BulletSpawnPoint.transform.rotation);
            bullet.gameObject.SetActive(false);
            bullet.transform.parent = gameObject.transform;
            projectiles.Enqueue(bullet.gameObject);
        }
    }
}
