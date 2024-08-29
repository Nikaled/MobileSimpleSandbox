using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shoot parametres")]
    [SerializeField] private float normalSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask;
    [SerializeField] private Transform RaycastOrigin;
    [SerializeField] private Transform PistolProjectileSpawnPoint;
    [SerializeField] private Transform GunProjectileSpawnPoint;
    [SerializeField] ShootingProjectile projectile;
    [SerializeField] public Image Crosshair;
    [SerializeField] Player player;
    [SerializeField] MeleeAttackHitbox handHitbox;
   public Vector3 AimDirection;
    [HideInInspector] public Vector3 CrosshairWorldPosition;
    [HideInInspector] public Vector3 MouseWorldPosition;
    float GunTimer;
    float GunShootInterval = 0.05f;
    int gunCount;
    public static PlayerShooting instance;
    public IEnumerator HoldingCoroutine;
    //[SerializeField] public LineRenderer lineRenderer;
    public AudioSource FireAudioSource;
    [SerializeField] AudioClip PistolSound;
    [SerializeField] public AudioClip GunSound;
    [SerializeField] AudioClip HandAttackSound;
    [SerializeField] AudioClip HandAttackHitObjectSound;
    [SerializeField] AudioClip KnifeAttackSound;
    [SerializeField] AudioClip KnifeAttackHitObjectSound;
    [SerializeField] public AudioClip GrenadeThrowSound;
    private void Start()
    {
        instance = this;
    }
    public bool Reloading { get; private set; }

    void Update()
    {
        CrosshairWorldPosition = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Crosshair.transform.position);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 5000, aimColliderLayerMask))
        {
            CrosshairWorldPosition = raycastHit.point;
        }
        else
        {
            CrosshairWorldPosition = ray.GetPoint(1998);
        }

         AimDirection = (CrosshairWorldPosition - PistolProjectileSpawnPoint.position).normalized;

    }
    public void Fire(Player.WeaponType currentWeapon)
    {
        if (currentWeapon == Player.WeaponType.Pistol)
        {
            Vector3 aimDirection = (CrosshairWorldPosition - PistolProjectileSpawnPoint.position).normalized;
            player.RotatePlayerOnShoot(aimDirection);
            ShootingProjectile proj = Instantiate(projectile, PistolProjectileSpawnPoint.position, Quaternion.LookRotation(aimDirection, Vector3.up));
            LockPlayerMovement(0.7f);
            FireAudioSource.clip = PistolSound;
            FireAudioSource.Play();
        }

        if (currentWeapon == Player.WeaponType.Gun)
        {
            FireGun();
            LockPlayerMovement(0.4f);
        }

    }
    public void LockPlayerMovement(float HoldingTime = 1f)
    {
        //var cor = player.LockPositionOnShoot(HoldingTime);
        if (HoldingCoroutine != null)
        {
            StopCoroutine(HoldingCoroutine);
        }
        HoldingCoroutine = null;
        HoldingCoroutine = player.LockPositionOnShoot(HoldingTime);
        StartCoroutine(HoldingCoroutine);
    }
    public void FireGun()
    {
        Vector3 aimDirection = (CrosshairWorldPosition - GunProjectileSpawnPoint.position).normalized;
        player.RotatePlayerOnShoot(aimDirection);
        if (Time.time - GunTimer > GunShootInterval)
        {
            Reloading = false;
        }
        if (Reloading == false)
        {
            Reloading = true;
           
            ShootingProjectile proj = Instantiate(projectile, GunProjectileSpawnPoint.position, Quaternion.LookRotation(aimDirection, Vector3.up));
            GunTimer = Time.time;
            gunCount += 1;
            if(gunCount > 2)
            {
                FireAudioSource.clip = GunSound;
                FireAudioSource.Play();
                gunCount = 0;
            }
        }

    }
    public void HandAttack(Player.WeaponType meleeWeapon)
    {
        Vector3 aimDirection = (CrosshairWorldPosition - PistolProjectileSpawnPoint.position).normalized;
        player.RotatePlayerOnShoot(aimDirection);
        //LockPlayerMovement();
        List<HpSystemCollision> targets = new();
        if (handHitbox.GetEnemies() != null)
        {
            targets.AddRange(handHitbox.GetEnemies());
        }
        int meleeDamage = 1;
        if (meleeWeapon == Player.WeaponType.Knife)
        {
            meleeDamage = 2;
        }
        if (targets.Count > 0)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].TakeDamage(meleeDamage);
            }
            if (meleeWeapon == Player.WeaponType.Knife)
            {
                FireAudioSource.clip = KnifeAttackHitObjectSound;
                FireAudioSource.Play();
            }
            else
            {
                FireAudioSource.clip = HandAttackHitObjectSound;
                FireAudioSource.Play();
            }
        }
        else
        {
            if (meleeWeapon == Player.WeaponType.Knife)
            {
                FireAudioSource.clip = KnifeAttackSound;
                FireAudioSource.Play();
            }
            else
            {
                FireAudioSource.clip = HandAttackSound;
                FireAudioSource.Play();
            }

        }

        handHitbox.EndAttack();
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Ray ray = Camera.main.ScreenPointToRay(Crosshair.transform.position);
    //    Gizmos.DrawRay(RaycastOrigin.position, ray.direction * 999);
    //}
}
