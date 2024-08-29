using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HpSystem : MonoBehaviour
{
    [SerializeField]public GameObject RootObject;
    [SerializeField] GameObject MeshObject;
    [SerializeField] Image HpBarCurrent;
    [SerializeField] Image HpBar;
    [SerializeField] private int MaxHp;
    [SerializeField] TextMeshProUGUI HpText;
    [SerializeField] GameObject DestroyAnimation;
    [SerializeField] AudioExplosion AudioExplosionPrefab;
    public Vector3 OriginScale;
    public float OriginHpBarScale;
    public bool Citizen;
    private readonly string AnalyticsDestroyObject = "ObjectDestroyed";
    private IEnumerator HideHpCor;
    [SerializeField]  public int CurrentHP
    {
        get { return _currentHP; }
        set
        {
            _currentHP = value;
            HpBarCurrent.transform.DOScaleX((float)_currentHP / MaxHp, 0);
            HpText.text = $"{_currentHP} / {MaxHp}";
            if(_currentHP < MaxHp)
            {
                HpBar.gameObject.SetActive(true);
                HideHpAfterDelay();
            }
        }
    }
    private int _currentHP;
    public event Action OnDied;
    private void Awake()
    {
        CurrentHP = MaxHp;
        HpBar.gameObject.SetActive(false);
        OriginScale = gameObject.transform.localScale;
        OriginHpBarScale = HpBar.gameObject.transform.localScale.y;
    }
    private void HideHpAfterDelay()
    {
        if(HideHpCor != null)
        {
            StopCoroutine(HideHpCor);
        }
        HideHpCor = HideHp();
        StartCoroutine(HideHpCor);
        IEnumerator HideHp()
        {
            yield return new WaitForSeconds(1.5f);
            HpBar.gameObject.SetActive(false);
        }
    }
    public void ResizeYHpBar(float IncreaseNumber)
    {
        HpBar.gameObject.transform.DOScaleY(OriginHpBarScale / IncreaseNumber, 0);
    }
    public void TakeDamage(int DamageCount)
    {
        if (DamageCount < 0)
        {
            Debug.Log("Ќанесено отрицательное количество урона");
            return;
        }
        CurrentHP -= DamageCount;
        HpBar.gameObject.SetActive(true);
        if (CurrentHP < 0)
        {
            CurrentHP = 0;
        }
        if(CurrentHP == 0)
        {
            ObjectDies();
        }

    }
    private void ObjectDies()
    {
        OnDied?.Invoke();

        RewardPlayer();
        if (HideHpCor != null)
        {
            StopCoroutine(HideHpCor);
        }

        if (RootObject.CompareTag("Citizen"))
        {
            return;
        }
        if(MeshObject != null)
        {
            if (!MeshObject.CompareTag("Player"))
            {
                Explosion(MeshObject);
            }
        Destroy(RootObject);
        }
        else
        {
            Debug.Log("Ќе назначен родитель удалени€");
            MeshObject = RootObject;
            Explosion(RootObject);
            if(RootObject !=null)
            Destroy(RootObject);
        }
    }
    protected virtual void RewardPlayer()
    {
        Geekplay.Instance.PlayerData.DestroyCount++;
        Geekplay.Instance.Leaderboard("Destroy", Geekplay.Instance.PlayerData.DestroyCount);

        Analytics.instance.SendEvent(AnalyticsDestroyObject);
        Geekplay.Instance.Save();
    }
    private void Explosion(GameObject rootObject)
    {
        if (RootObject == null)
        {
            GameObject parent = this.transform.parent.gameObject;
            if (parent.GetComponent<HpSystemCollision>() != null)
            {
                RootObject = parent;
                rootObject = parent;
            }
        }
            if (rootObject.GetComponent<MeshRenderer>() == null)
            {
                return;
            }
        Vector3 bounds = rootObject.GetComponent<MeshRenderer>().bounds.size;
        float SumOfSides = FindSumOfSides(bounds) / 9 ;
        Vector3 ExplosionPos = new Vector3(HpBar.transform.position.x, HpBar.transform.position.y-bounds.y, HpBar.transform.position.z);
        AudioExplosion audioExplosion = Instantiate(AudioExplosionPrefab, ExplosionPos, Quaternion.identity);
        audioExplosion.PlayExplosionSound();
        var fx = Instantiate(DestroyAnimation, ExplosionPos, Quaternion.identity);
        fx.transform.parent = null;
        fx.transform.DOScale(DestroyAnimation.transform.localScale *SumOfSides, 0);
    }
    private float FindSumOfSides(Vector3 bounds)
    {
        float[] Vectors = new float[] { bounds.x, bounds.y, bounds.z };
        float largerSide = 0;
        for (int i = 0; i < Vectors.Length; i++)
        {
            largerSide+= Vectors[i];
        }
        return largerSide;
    }
}