using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CoinsText;
    [SerializeField] TextMeshProUGUI CoinsInPromoText;
    string AnalyticMapCreate = "CreateMap";
    string AnalyticMapChooseField = "Field_ChooseMap";
    string AnalyticMapChooseMilitary = "Military_ChooseMap";
    string AnalyticMapChooseFarm = "Farm_ChooseMap";
    string AnalyticMapChooseAirport = "Airport_ChooseMap";
    string AnalyticMapChooseLoaded = "Loaded_ChooseMap";
    void Start()
    {
        Geekplay.Instance.GameReady();
        Geekplay.Instance.ShowInterstitialAd();
        ChangeCoinsText(Geekplay.Instance.PlayerData.Coins);
        Geekplay.Instance.PlayerData.CoinsChanged += ChangeCoinsText;
        if(Geekplay.Instance.PlayerData.IsNotFirstPlay == false)
        {
            Geekplay.Instance.PlayerData.IsNotFirstPlay = true;
            SceneManager.LoadScene(5);
        }
    }
    void ChangeCoinsText(int NewCoinsCount)
    {
        CoinsText.text = NewCoinsCount.ToString();
        CoinsInPromoText.text = NewCoinsCount.ToString();
    }
    #region Analytics
    public void AnalyticsOnMapCreate()
    {
        Analytics.instance.SendEvent(AnalyticMapCreate);
    }
    public void AnalyticsOnMapChooseField()
    {
        Analytics.instance.SendEvent(AnalyticMapChooseField);
    }
    public void AnalyticsOnMapChooseMilitary()
    {
        Analytics.instance.SendEvent(AnalyticMapChooseMilitary);
    }
    public void AnalyticsOnMapChooseFarm()
    {
        Analytics.instance.SendEvent(AnalyticMapChooseFarm);
    }
    public void AnalyticsOnMapChooseAirport()
    {
        Analytics.instance.SendEvent(AnalyticMapChooseAirport);
    }
    #endregion
    public void LoadStandartMap()
    {
        Geekplay.Instance.PlayerData.IsPlayerMapLoad = false;
        Geekplay.Instance.Save();
    }
    public void ShowInterstialOnMapChosen()
    {
        Geekplay.Instance.ShowInterstitialAd();
    }
    public void LoadChosenMap(int index)
    {
        Analytics.instance.SendEvent(AnalyticMapChooseLoaded);
        switch (index)
        {
            case 1:
                if(Geekplay.Instance.PlayerData.NameMap1 !=string.Empty && Geekplay.Instance.PlayerData.NameMap1 != null)
                {
                    Geekplay.Instance.PlayerData.IsPlayerMapLoad = true;
                    Geekplay.Instance.PlayerData.CurrentSaveSlotLoading = index;
                    Geekplay.Instance.Save();
                    Geekplay.Instance.ShowInterstitialAd();
                    SceneManager.LoadScene(Geekplay.Instance.PlayerData.NameMap1);
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                break;
            case 2:
                if (Geekplay.Instance.PlayerData.NameMap2 != string.Empty && Geekplay.Instance.PlayerData.NameMap2 != null)
                {
                    Geekplay.Instance.PlayerData.IsPlayerMapLoad = true;
                    Geekplay.Instance.PlayerData.CurrentSaveSlotLoading = index;
                    Geekplay.Instance.Save();
                    Geekplay.Instance.ShowInterstitialAd();
                    SceneManager.LoadScene(Geekplay.Instance.PlayerData.NameMap2);
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                break;
            case 3:
                if (Geekplay.Instance.PlayerData.NameMap3 != string.Empty && Geekplay.Instance.PlayerData.NameMap3 != null)
                {
                    Geekplay.Instance.PlayerData.IsPlayerMapLoad = true;
                    Geekplay.Instance.PlayerData.CurrentSaveSlotLoading = index;
                    Geekplay.Instance.Save();
                    Geekplay.Instance.ShowInterstitialAd();
                    SceneManager.LoadScene(Geekplay.Instance.PlayerData.NameMap3);
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                break;
            case 4:
                if (Geekplay.Instance.PlayerData.NameMap4 != string.Empty && Geekplay.Instance.PlayerData.NameMap4 != null)
                {
                    Geekplay.Instance.PlayerData.IsPlayerMapLoad = true;
                    Geekplay.Instance.PlayerData.CurrentSaveSlotLoading = index;
                    Geekplay.Instance.Save();
                    Geekplay.Instance.ShowInterstitialAd();
                    SceneManager.LoadScene(Geekplay.Instance.PlayerData.NameMap4);
                }
                else
                {
                    Cursor.lockState = CursorLockMode.None;
                }
                break;
        }
    }
}
