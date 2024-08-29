using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class AppShop : MonoBehaviour
{
    public AppShopCell RewardCell;
    public AppShopCell[] ShopCells;
    public TextMeshProUGUI RewardText;
    public GameObject ConfirmUI;
    public static AppShop instance;
    [Header("Only for  main menu")]
    [SerializeField] TextMeshProUGUI CoinsText;
    private void Start()
    {
        if(CoinsText != null)
        {
            Geekplay.Instance.PlayerData.CoinsChanged += SetCoinsText;
            CoinsText.text = Geekplay.Instance.PlayerData.Coins.ToString();
        }
        instance = this;
        for (int i = 0; i < ShopCells.Length; i++)
        {
            ShopCells[i].SubscribeOnPurchase();
        }

        RewardCell.SubscribeOnReward();
    }
    public void ShowConfirmRewardWindow(int GoldCount)
    {
        RewardText.text = GoldCount.ToString();
        ConfirmUI.SetActive(true); 
    }
    private void SetCoinsText(int newCoinsCount)
    {
        CoinsText.text = newCoinsCount.ToString();
    }
}
