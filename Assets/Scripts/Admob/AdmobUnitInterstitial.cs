using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.SceneManagement;

public class AdmobUnitInterstitial : AdmobUnitBase
{
    private InterstitialAd interstitialAd;
    private string nextSceneName;

    public bool IsReady
    {
        get
        {
            if (AdmobManager.Instance.IsReady == false)
            {
                return false;
            }
            return interstitialAd != null && interstitialAd.CanShowAd();
        }
    }

    protected override void Initialize()
    {
        LoadInterstitialAd();
    }

    public void ShowInterstitial(string sceneToLoad)
    {
        nextSceneName = sceneToLoad;
        if (IsReady)
        {
            interstitialAd.Show();
        }
        else
        {
            Debug.Log("Interstitial ad is not ready yet.");
            SceneManager.LoadScene(nextSceneName);  // Load scene immediately if ad is not ready
        }
    }

    private void LoadInterstitialAd()
    {
        // Clean up old ads before loading new ones
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        var adRequest = new AdRequest();

        InterstitialAd.Load(UnitID, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if (error != null || ad == null)
                {
                    Debug.LogError("Interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
                RegisterEventHandlers(interstitialAd);
            });
    }

    private void RegisterEventHandlers(InterstitialAd interstitialAd)
    {
        // Called when the ad closes the full screen content
        interstitialAd.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
            LoadInterstitialAd();
            SceneManager.LoadScene(nextSceneName);  // Load scene after ads are closed.
        };
    }
}
