using System;
using System.Collections;
using System.Collections.Generic;
using CAS;
using UnityEngine;
using Unity.RemoteConfig;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
using System.Threading.Tasks;
using static CASAds;

public class CASAds : MonoBehaviour
{

    public static CASAds instance = null;

    private static IMediationManager _manager = null;
    private static IAdView _lastAdView = null;
    private static IAdView _lastMrecAdView = null;
    private static Action _lastAction = null;

    public static bool RemoteAdValue = false;
    public struct UserAttributes { }
    public struct AppAttributes { }

    private void Awake()
    {        
        Init();
    }

    /*void ApplyRemoteSettings(Unity.RemoteConfig.ConfigResponse response)
    {
        if (response.requestOrigin == Unity.RemoteConfig.ConfigOrigin.Remote)
        {
            RemoteAdValue = RemoteConfigService.Instance.appConfig.GetBool("AdIsOn_1.6.3");
            Debug.Log("RemoteAdValue: " + RemoteAdValue);
        }
        else
        {
            Debug.Log("Using default config values, Remote Config fetch failed or pending.");
            RemoteAdValue = false;
        }



        CAS.MobileAds.settings.isExecuteEventsOnUnityThread = true;

        if (RemoteAdValue == true)
        {
            Init();
        }
    }*/



    private void Init()
    {
        _manager = MobileAds.BuildManager()
            .WithInitListener(CreateAdView)
            // Call Initialize method in any case to get IMediationManager instance
            .Initialize();

        _manager.OnRewardedAdCompleted += _lastAction;

        _manager.GetAdView(AdSize.Banner).OnImpression += OnAdImpression;
        _manager.OnInterstitialAdImpression += OnAdImpression;
        _manager.OnRewardedAdImpression += OnAdImpression;
    }

    private void CreateAdView(bool success, string error)
    {
        _lastAdView = _manager.GetAdView(AdSize.Banner);
        _lastMrecAdView = _manager.GetAdView(AdSize.MediumRectangle);
        _lastAdView.SetActive(false);
        _lastMrecAdView.SetActive(false);
    }
 
    public void ShowBanner(AdPosition position)
    {
        Debug.Log("Show Banner");
        if (_lastAdView == null)
        {
            CreateAdView(true, ""); 
        }

        if (_lastAdView != null)
        {
            _lastAdView.position = position;
            _lastAdView.SetActive(true);
        }
    }

    public void ShowMrecBanner(AdPosition position)
    {
        if (_lastMrecAdView == null)
        {
            CreateAdView(true, "");
        }

        if (_lastMrecAdView != null)
        {
            _lastMrecAdView.position = position;
            _lastMrecAdView.SetActive(true);
        }
    }

    public void HideBanner()
    {
        if ( _lastAdView != null )
        {
            _lastAdView.SetActive( false );
        }
    }

    public void HideMrecBanner()
    {
        if (_lastMrecAdView != null)
        {
            _lastMrecAdView.SetActive(false);
        }
    }

    public void ShowInterstitial()
    {
        _manager?.ShowAd( AdType.Interstitial );
    }

    public void ShowRewarded( Action complete )
    {
        if ( _manager == null )
            return;
        
        if ( _lastAction != null)
        {
            _manager.OnRewardedAdCompleted -= _lastAction;
        }

        _lastAction = complete;
        _manager.OnRewardedAdCompleted += _lastAction;
        _manager?.ShowAd(AdType.Rewarded);
    }

    private void OnAdImpression(IAdView view, AdMetaData adMetaData)
    {
        OnAdImpression(adMetaData);
    }

    private void OnAdImpression(AdMetaData adMetaData)
    {
        if (adMetaData.priceAccuracy == PriceAccuracy.Undisclosed) return;


        //РАСКОММЕНТИТЬ
        /*var e = new AdRevenueEvent(
                "CAS",
                adMetaData.type.ToString(),
                adMetaData.network.ToString(),
                "USD",
                "",
                adMetaData.cpm / 1000 * 0.9
            );
        AnalyticsSender.HandleAdRevenueEvent(e);*/
    }
}
