using System.IO;
using UnityEngine;

// このクラスはデータを取得するだけ、実際のデータの運用は別クラスで行う(今回はRanking.cs)
public class DataManager : MonoBehaviour
{
    [HideInInspector] public Score score;
    string filePath;
    string fileName = "Score.json";
    void Awake()
    {
        // データの保存先
        this.filePath = Application.persistentDataPath+"/"+ fileName;
        Debug.Log(this.filePath);
        // ファイルが存在しないとき、ファイル作成
        if(!File.Exists(this.filePath)){
            Save(this.score);
        }
        this.score = Load(this.filePath);
    }
    // Jsonデータの保存
    public void Save(Score score){
        Debug.Log("score.score10.Length:"+score.score.Length);
        Debug.Log("score.score20.Length:"+score.score.Length);
        Debug.Log("score.score30.Length:"+score.score.Length);
        string json = JsonUtility.ToJson(this.score);
        StreamWriter wr = new StreamWriter(filePath,false);
        wr.WriteLine(json);
        wr.Close();
    }

    // jsonデータの読み込み
    public Score Load(string path){
        StreamReader sd = new StreamReader(path);
        string json = sd.ReadToEnd();
        sd.Close();
        return JsonUtility.FromJson<Score>(json);
    }
}
