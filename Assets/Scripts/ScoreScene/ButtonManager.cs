using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreButtonManager : MonoBehaviour
{
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
