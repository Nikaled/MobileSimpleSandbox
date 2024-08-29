using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TutorialShootingCitizen : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject projectile;
    [SerializeField] CitizenWeapon weapon;
    [SerializeField] Transform BulletSpawnPoint;
    [SerializeField] GameObject SpawnedObjectPrefab;
    [SerializeField] Transform SpawnedObjectTransform;
    GameObject CurrentTarget;
    private Queue<GameObject> projectiles;
    [SerializeField] HpSystem hpSystem;
    private bool IsDying;
    public enum CitizenWeapon
    {
        Pistol,
        Gun,
        Grenade,
    }
    private void Start()
    {
        hpSystem.OnDied += CitizenDie;
        SpawnedObjectTransform.parent = null;
        SpawnTarget();
        projectiles = new();
        SpawnPool();
        switch (weapon)
        {
            case CitizenWeapon.Pistol:
                StartCoroutine(ShootingPistolCycle());
                break;
            case CitizenWeapon.Gun:
                StartCoroutine(ShootingGunCycle());
                break;
            case CitizenWeapon.Grenade:
                StartCoroutine(LaunchGrenadeCycle());
                break;
        }
    }
    private void SpawnPool()
    {
        for (int i = 0; i < 41; i++)
        {
            var bullet = Instantiate(projectile, BulletSpawnPoint.transform.position, BulletSpawnPoint.transform.rotation);
            bullet.gameObject.SetActive(false);
            bullet.transform.parent = gameObject.transform;
            projectiles.Enqueue(bullet);
        }
    }
    private void SpawnTarget()
    {
        var newTarget = Instantiate(SpawnedObjectPrefab, SpawnedObjectTransform.position, SpawnedObjectTransform.rotation);
        CurrentTarget = newTarget;
    }
    private void Shoot()
    {
        if (CurrentTarget != null)
        {
            Vector3 RotatingToTargetVector = new Vector3(CurrentTarget.transform.position.x, this.transform.position.y, CurrentTarget.transform.position.z);
            transform.LookAt(RotatingToTargetVector);
            var ShootingProj = projectiles.Dequeue();
            ShootingProj.transform.SetPositionAndRotation(BulletSpawnPoint.transform.position, BulletSpawnPoint.transform.rotation);
            ShootingProj.gameObject.SetActive(true);
        }
    }
    private IEnumerator ShootingPistolCycle()
    {

        while (true)
        {
            yield return new WaitForSeconds(1.2f);
            if (!IsDying)
            {
                if (projectiles.Count <= 0)
                {
                    SpawnPool();
                }
                if (CurrentTarget == null)
                {
                    SpawnTarget();
                }
                animator.SetTrigger("PistolFire");
                yield return new WaitForSeconds(0.05f);
                Shoot();
            }
        }
    }
    private IEnumerator ShootingGunCycle()
    {

        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            if (!IsDying)
            {
                if (projectiles.Count <= 0)
                {
                    SpawnPool();
                }
                if (CurrentTarget == null)
                {
                    SpawnTarget();
                }

                animator.SetTrigger("PistolFire");
                yield return new WaitForSeconds(0.05f);
                Shoot();
            }
        }
    }
    protected void CitizenDie()
    {
        if (IsDying)
        {
            return;
        }
        //if (gameObject.GetComponent<CapsuleCollider>() != null)
        //{
        //    gameObject.GetComponent<CapsuleCollider>().enabled = false;
        //}
        IsDying = true;
        animator.SetBool("IsWalk", false);
        animator.SetTrigger("Die");
        StartCoroutine(WaitForDyingAnimation());
    }
    private IEnumerator WaitForDyingAnimation()
    {
        hpSystem.enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(hpSystem.RootObject);
    }

    private IEnumerator LaunchGrenadeCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(4);
           
            if (!IsDying)
            {
                if (projectiles.Count <= 0)
                {
                    SpawnPool();
                }
                if (CurrentTarget == null)
                {
                    SpawnTarget();
                }
                if (CurrentTarget != null)
                {
                    Vector3 RotatingToTargetVector = new Vector3(CurrentTarget.transform.position.x, this.transform.position.y, CurrentTarget.transform.position.z);
                    transform.LookAt(RotatingToTargetVector);
                }
                animator.SetBool("AimingGrenade", true);
                yield return new WaitForSeconds(0.5f);
                LaunchGrenade();
                animator.SetBool("AimingGrenade", false);
            }       
        }
    }
    private void LaunchGrenade()
    {
        int LaunchSpeed = 10;
        Vector3 AimVector = (SpawnedObjectTransform.position - transform.position).normalized;
        GameObject _projectile = projectiles.Dequeue();
        _projectile.transform.SetPositionAndRotation(BulletSpawnPoint.transform.position, BulletSpawnPoint.transform.rotation);
        _projectile.SetActive(true);
        _projectile.GetComponent<Rigidbody>().velocity = LaunchSpeed * AimVector + (Vector3.up * 10);
        _projectile.GetComponent<CapsuleCollider>().enabled = true;
        _projectile.GetComponent<Rigidbody>().useGravity = true;
        _projectile.GetComponent<Grenade>().OnLaunch();
    }

}