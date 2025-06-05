using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

[DefaultExecutionOrder(-1)]
public class Ranking : MonoBehaviour
{
    const int   rankCnt = Score.rankCnt;                 // ランキング数

    /* コンポーネント取得用 */
    /* 
        Score
        rankCnt : ランクセーブ上限
        score : 参照するセーブデータリスト型で入っている
    */
    public Score score;                                       // 参照するセーブデータ
    
    public DataManager DataManager;

    //-------------------------------------------------------------------
    void Start()
    {
        this.DataManager = GetComponent<DataManager>();            //DataManagerからセーブデータのフィールドの参照を取得
        //DataManagerからセーブデータのフィールドの参照を取得
        this.score = DataManager.score;            // セーブデータをDataManagerから参照
    }

    //-------------------------------------------------------------------
    void FixedUpdate()
    {
        // DispRank();
    }

    // ランキング保存
    public void SetRank(int gameScore)
    {
        if(IsChengeRank(gameScore)){
            // scoreの末尾の値を入れ替える
            score.score[rankCnt-1] = gameScore;
            this.DataManager.Save(score);
        }
    }
    //降順に並び替えられたランクデータをもとにしてデータの入れ替えが行われるか判定する
    public Boolean IsChengeRank(int gameScore){
        if(gameScore > score.score[rankCnt-1]){
            return true;
        }else{
            return false;
        }
    }
    //ランキングデータを降順に並び替える
    public void SortScore(){
        for (int i = 0; i < rankCnt; i++) {
            for (int j = i + 1; j < rankCnt; j++) {
                if (score.score[i] < score.score[j]) {
                    var rep = score.score[i];
                    score.score[i] = score.score[j];
                    score.score[j] = rep;
                }
            }
        }
    }
/*
    // ランクデータの削除
    public void DelRank()
    {
        for (int i = 0; i < rankCnt; i++) {
            data.rank[i] = 0;
        }
    } */
}

