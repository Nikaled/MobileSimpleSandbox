using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingProjectile : MonoBehaviour
{
    [SerializeField] Rigidbody bulletRigidbody;
    public float speed = 300;
  public  int bulletDamage = 1;
    public GameObject ProjectileSource;
   public bool TankProjectile;
    [SerializeField] GameObject DestroyAnimation;

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody.velocity = transform.forward * speed;
        StartCoroutine(DestroyObj());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == ProjectileSource)
        {
            return;
        }
        //Debug.Log(other.name);
        if (other.GetComponent<HpSystemCollision>() != null)
        {
            other.GetComponent<HpSystemCollision>().TakeDamage(bulletDamage);
            if (TankProjectile)
            {
                var fx = Instantiate(DestroyAnimation, transform.position, Quaternion.identity);
                fx.transform.parent = null;
                fx.transform.DOScale(DestroyAnimation.transform.localScale * 5, 0);
            }
        Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == ProjectileSource)
        {
            return;
        }
        Debug.Log(collision.gameObject);
        if (collision.gameObject.GetComponent<HpSystemCollision>() != null)
        {
            collision.gameObject.GetComponent<HpSystemCollision>().TakeDamage(bulletDamage);
        }
        Destroy(gameObject);
    }
    private IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
