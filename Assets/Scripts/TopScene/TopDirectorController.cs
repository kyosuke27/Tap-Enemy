using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopDirectorController: MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        // ゲーム画面へ遷移
        SceneManager.LoadScene("GameScene");
    }
    
    public void OnScoreButtonClicked()
    {
        // スコア画面へ遷移
        SceneManager.LoadScene("ScoreScene");
    }
}
