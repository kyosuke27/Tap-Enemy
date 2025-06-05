using UnityEngine;
using UnityEngine.UI;

public class Interstitial : MonoBehaviour
{
    public Button showInterstitialButton;
    public AdmobUnitInterstitial admobUnitInterstitial;

    private void Start()
    {
        showInterstitialButton.interactable = true;

        showInterstitialButton.onClick.AddListener(() =>
        {
            string buttonTag = showInterstitialButton.tag;
            if (buttonTag == "retry")
            {
                admobUnitInterstitial.ShowInterstitial("Scenes/GameScene");
            }
            else
            {
                Debug.LogWarning("Button tag is not recognized.");
            }
            showInterstitialButton.interactable = true;
        });
    }

    private void Update()
    {
        showInterstitialButton.interactable = admobUnitInterstitial.IsReady;
    }
}
