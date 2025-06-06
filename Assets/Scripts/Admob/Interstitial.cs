using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TestInterstitial : MonoBehaviour
{
    public Button showInterstitialButton;

    public AdmobUnitInterstitial admobUnitInterstitial;

    private void Start()
    {
        showInterstitialButton.interactable = false;

        showInterstitialButton.onClick.AddListener(() =>
        {
            admobUnitInterstitial.ShowInterstitial();
            showInterstitialButton.interactable = false;
            SceneManager.LoadScene("Scenes/GameScene");
        });
    }

    private void Update()
    {
        showInterstitialButton.interactable = admobUnitInterstitial.IsReady;
    }
}