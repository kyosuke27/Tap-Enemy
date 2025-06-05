using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// データ保存用Jsonクラス
[System.Serializable]
public class Score
{
    // constで宣言するとJsonUtilityでのシリアライズができない
    public const  int rankCnt = 20;
    public int[] score = new int[rankCnt];
}
