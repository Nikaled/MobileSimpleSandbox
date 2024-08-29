using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UndependAppShopCell : MonoBehaviour
{
    public string PurName;
    public int PurGold;
    public Button BuyGoldButton;
    [Header("Only Reward")]
    [SerializeField] protected GameObject RewardBlocker;
    [SerializeField] protected TextMeshProUGUI RewardTimerText;
    [SerializeField] GameObject ConfirmUI;
    [SerializeField] Image ButtonImage;
    [SerializeField] GameObject ObjectsToHide;
    private void Start()
    {
        SubscribeOnReward();
    }
    private void OnEnable()
    {
        if (RewardBlocker != null && RewardTimerText != null)
        {
            Geekplay.Instance.RewardLockTimeUpdate += SetNewTimerTextAndCheckEnd;
            if (Geekplay.Instance.RewardLockTimer > 0)
            {
                ShowButton(false);
                RewardBlocker.SetActive(true);
                RewardTimerText.text = string.Format("{0:00}:{1:00}", 01, 30);
            }
            else
            {
                ShowButton(true);
                RewardBlocker.SetActive(false);
            }
        }
    }
    private void ShowButton(bool Is)
    {
        ButtonImage.enabled = Is;
        ObjectsToHide.gameObject.SetActive(Is);
    }
    protected virtual void OnDisable()
    {
        if (RewardBlocker != null && RewardTimerText != null)
        {
            Geekplay.Instance.RewardLockTimeUpdate -= SetNewTimerTextAndCheckEnd;
        }
    }
    public void SubscribeOnPurchase()
    {
        Geekplay.Instance.SubscribeOnPurchase(PurName, GetGold);
        BuyGoldButton.onClick.AddListener(delegate { InAppOperation(); });
    }
    public void SubscribeOnReward()
    {
        Geekplay.Instance.SubscribeOnReward(PurName, GetGold);
        BuyGoldButton.onClick.AddListener(delegate { RewardOperation(); });
    }
    private void InAppOperation()
    {
        Geekplay.Instance.RealBuyItem(PurName);
    }
    private void RewardOperation()
    {
        Geekplay.Instance.ShowRewardedAd(PurName);
        Geekplay.Instance.RunCoroutine(BlockRewardOnTimeByGeekplay());

        RewardTimerText.text = string.Format("{0:00}:{1:00}", 01, 30);
        RewardBlocker.SetActive(true);
        BuyGoldButton.enabled = false;
        ShowButton(false);
    }
    private void SetNewTimerTextAndCheckEnd(int Timer)
    {
        if (RewardTimerText == null)
        {
            return;
        }
        if (Timer < 60)
        {
            RewardTimerText.text = string.Format("{0:00}:{1:00}", 00, Timer);
        }
        else
        {
            int TimerSeconds = Timer - 60;
            RewardTimerText.text = string.Format("{0:00}:{1:00}", 01, TimerSeconds);
        }
        if (Timer <= 0)
        {
            RewardBlocker.SetActive(false);
            BuyGoldButton.enabled = true;
            ShowButton(true);
        }
    }
    private void GetGold()
    {
        Geekplay.Instance.PlayerData.Coins += PurGold;
        AppShop.instance.ShowConfirmRewardWindow(PurGold);
    }
    private IEnumerator BlockRewardOnTimeByGeekplay()
    {
        Geekplay.Instance.RewardLockTimer = 90;
        while (Geekplay.Instance.RewardLockTimer > 0)
        {
            yield return new WaitForSeconds(1);
            Geekplay.Instance.RewardLockTimer--;
            Geekplay.Instance.RewardLockTimeUpdate?.Invoke(Geekplay.Instance.RewardLockTimer);
        }
    }
}
