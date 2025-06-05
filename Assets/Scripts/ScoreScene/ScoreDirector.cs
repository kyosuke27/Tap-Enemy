using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDirector : MonoBehaviour
{
    public Ranking ranking;
    // prefab
    public GameObject TextPrefab;
    // UI
    public GameObject Content;
    private Score score;
    void Start()
    {
        this.ranking = GameObject.Find("DataManager").GetComponent<Ranking>();
        this.ranking.SortScore();
        this.score = this.ranking.score;
        DispRank();
    }

    public void DispRank()
    {
        for (int i = 0; i < this.score.score.Length; i++) {
            if(this.score.score[i]>0){
                GameObject rankText = Instantiate(this.TextPrefab,this.Content.transform );  // テキスト生成
                rankText.GetComponent<TextMeshProUGUI>().text = (i + 1) + "位：" + score.score[i].ToString();                      // テキストにスコアを表示
            }
        }
    }
}
