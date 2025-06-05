using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobUnitBanner : AdmobUnitBase
{
    private BannerView bannerView;

    protected override void Initialize()
    {
        ShowBanner();
    }

    public void ShowBanner()
    {
        if (bannerView != null)
        {
            Debug.Log("Destroying banner view.");
            bannerView.Destroy();
            bannerView = null;
        }

        // Create AdSize
        AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        // Designate AdSize as Argument
        bannerView = new BannerView(
            UnitID,
            adaptiveSize,
            AdPosition.Bottom);

        bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("ロードされました - 表示します");
        };
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            Debug.LogError("UnitID: " + UnitID);
            Debug.LogError("ロード失敗しました");
        };

        // Generate Request
        var adRequest = new AdRequest();
        bannerView.LoadAd(adRequest);
    }

    private void UnitDestroy()
    {
        if (bannerView != null)
        {
            Debug.Log("Destroying banner view.");
            bannerView.Destroy();
            bannerView = null;
        }
    }

    private void OnDestroy()
    {
        UnitDestroy();
    }

}
