using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBoxingCitizen : MonoBehaviour
{
    [SerializeField] GameObject CurrentEnemy;
    [SerializeField] MeleeAttackHitbox handHitbox;
    [SerializeField] Animator animator;
    [SerializeField] HpSystem hpSystem;
    private bool IsDying;
    [SerializeField] ObjectVariants FootballerTextureVariants;
    [SerializeField] bool IsWhite;
    private void Start()
    {
        StartCoroutine(InfinityBoxing());
        hpSystem.OnDied += CitizenDie;
        ChangeTexturesToWhite(IsWhite);
    }
    public void ChangeTexturesToWhite(bool IsWhite)
    {
        if (IsWhite)
        {
            FootballerTextureVariants.ChangeTexturesOnLoad(2);
        }
    }
    protected void CitizenDie()
    {
        if(IsDying == true)
        {
            return;
        }
        IsDying = true;
        TutorialMeleeCitizenSpawner.instance.CreateNewUnit(transform.position, transform.rotation, IsWhite);
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
    private void Update()
    {
        if(CurrentEnemy == null)
        {
            FindEnemy();
        }
    }
    private IEnumerator InfinityBoxing()
    {
        while (true)
        {
           float RandomInterval =  Random.Range(0.7f, 1.2f);
            yield return new WaitForSeconds(RandomInterval);
            if(IsDying== false && CurrentEnemy !=null)
            {
            Boxing();
            }
        }
    }
    private void FindEnemy()
    {
        if (handHitbox.collisions != null)
        {
            if (handHitbox.collisions.Count > 0)
            {
                for (int i = 0; i < handHitbox.collisions.Count; i++)
                {
                    if(handHitbox.collisions[i]!=null)
                    if (handHitbox.collisions[i].CompareTag("Citizen"))
                    {
                        CurrentEnemy = handHitbox.collisions[i].gameObject;
                    }
                }
            }
        }
    }
    private void Boxing()
    {
        transform.LookAt(CurrentEnemy.transform.position);
        List<HpSystemCollision> targets = new();
        if (handHitbox.GetEnemies() != null)
        {
            targets.AddRange(handHitbox.GetEnemies());
        }
        animator.SetTrigger("Punch");
        int meleeDamage = 1;
        if (targets.Count > 0)
        {
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].TakeDamage(meleeDamage);
            }
        }
        handHitbox.EndAttack();
    }
}
