using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class Advertising : MonoBehaviour
{
    public static Action OnPlayerSawAd;

    private const string AD_UNIT_ID = "ca-app-pub-3940256099942544/5224354917";
    private RewardedAd _ad;

    void Awake()
    {
        OnPlayerSawAd = null;
    }

    public void LoadRewardedAd()
    {
        if(GameData.IsWatchedAd) return;

        if(_ad != null)
        {
            _ad.Destroy();
            _ad = null;
        }

        var adRequest = new AdRequest.Builder().Build();
        RewardedAd.Load(AD_UNIT_ID, adRequest, 
            (RewardedAd ad, LoadAdError error) => 
            {
                if(error != null || ad == null)
                {
                    Debug.Log("Reward was failed " + error);
                    return;
                }
                else
                {
                    _ad = ad;
                    ShowRewardedAd();
                }
            });
        }

    public void ShowRewardedAd()
    {
        if(_ad != null && _ad.CanShowAd())
        {
            _ad.Show(PlayerSawAd);
            GameData.IsWatchedAd = true;
        }
    }

    public void PlayerSawAd(Reward reward)
    {
        OnPlayerSawAd?.Invoke();
    }
}
