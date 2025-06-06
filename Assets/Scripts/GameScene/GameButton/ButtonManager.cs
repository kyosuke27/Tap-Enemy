using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] public Button retryButton;
    [SerializeField] public Button homeButton;
    [SerializeField] public Button scoreButton;
    [SerializeField] public AdmobUnitInterstitial admobUnitInterstitial;

    void Start()
    {
        // リトライボタンの初期化
        retryButton.interactable = false;
        retryButton.onClick.AddListener(() =>
        {
            retryButton.interactable = false;
            int randomValue = Random.Range(0, 10);
            if (randomValue < 5)
            {
                Common.SetMoveSceneName("GameScene");
                // 30%の確率で広告を表示
                // 広告を表示した場合には、AdmobUnitInterstitial関数の広告をし終了した際にシーンの遷移を行なう
                admobUnitInterstitial.ShowInterstitial();
            }
            else
            {
                // 広告を表示せずにリトライ
                SceneManager.LoadScene("GameScene");
            }
        });
        // スコアボタンの初期化
        scoreButton.interactable = false;
        scoreButton.onClick.AddListener(() =>
        {
            scoreButton.interactable = false;
            int randomValue = Random.Range(0, 10);
            if (randomValue < 5)
            {
                Common.SetMoveSceneName("ScoreScene");
                // 30%の確率で広告を表示
                // 広告を表示した場合には、AdmobUnitInterstitial関数の広告をし終了した際にシーンの遷移を行なう
                admobUnitInterstitial.ShowInterstitial();
            }
            else
            {
                // 広告を表示せずにリトライ
                SceneManager.LoadScene("ScoreScene");
            }
        });

        // ホームボタンの初期化
        homeButton.interactable = false;
        homeButton.onClick.AddListener(() =>
        {
            homeButton.interactable = false;
            int randomValue = Random.Range(0, 10);
            if (randomValue < 5)
            {
                Common.SetMoveSceneName("TopScene");
                // 30%の確率で広告を表示
                // 広告を表示した場合には、AdmobUnitInterstitial関数の広告をし終了した際にシーンの遷移を行なう
                admobUnitInterstitial.ShowInterstitial();
            }
            else
            {
                // 広告を表示せずにリトライ
                SceneManager.LoadScene("TopScene");
            }

        });
    }


    void Update()
    {
        bool isReady = admobUnitInterstitial.IsReady;
        // リトライボタンの有効化
        retryButton.interactable = isReady;
        // スコアボタンの有効化
        scoreButton.interactable = isReady;
        // ホームボタンの有効化
        homeButton.interactable = isReady;
    }

    public void OnClickTopButton()
    {
        // ゲーム画面へ遷移
        SceneManager.LoadScene("TopScene");
    }
    public void OnClickScoreButton()
    {
        // スコア画面へ遷移
        SceneManager.LoadScene("ScoreScene");
    }
    public void OnClickRetryButton()
    {
        // ゲーム画面へ遷移
        SceneManager.LoadScene("GameScene");
    }
    
}
