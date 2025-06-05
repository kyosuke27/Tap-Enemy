using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    // Prefab
    public GameObject namaGomiPrefab;
    public GameObject bananaPrefab;
    public GameObject gomiBakoPrefab;
    public GameObject coinPrefab;
    //UI
    public GameObject scoreText;
    public GameObject timerText;
    [SerializeField] public TextMeshProUGUI scoreDetailText;
    // Panel
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;
    // データの保存用DataManager
    public Ranking ranking;
    public Score score;
    private int createTime;

    // Max X position
    float maxXPos = 3.0f;
    float maxYPos = 3.20f;
    public float time;

    private bool endFlag;
    void Start()
    {
        this.time = 30.0f;
        this.createTime = 60;
        // Score Detailを初期化(空文字にする)
        this.scoreDetailText.GetComponent<TextMeshProUGUI>().text = "";
        // Panelを非表示
        this.gameOverPanel.SetActive(false);
        this.gameClearPanel.SetActive(false);
        // rankingオブジェクトを取得
        this.ranking = GameObject.Find("DataManager").GetComponent<Ranking>();
        ranking.SortScore();
        this.score = this.ranking.score;
        // オブジェクト内でのみ使用する変数の初期化
        this.endFlag=true;
    }

    // Update is called once per frame
    void Update()
    {
        // 敵キャラの作成
        if(this.time > 0){
             // タイマーの更新
            this.time -= Time.deltaTime;
            this.timerText.GetComponent<TextMeshProUGUI>().text = this.time.ToString("F1");
            // 敵生成感覚
            if(this.time>=15&&this.time<=20){
                this.createTime = 40;
            }else if(this.time>10&&this.time<=15){
                this.createTime = 30;
            }else if(this.time<=10){
                this.createTime = 20;
            }
            //1秒ごとにPrefabを生成する
            // Time.frameCountはゲームが始まってからのフレーム数
            if (Time.frameCount % this.createTime== 0)
            {
                GameObject enemyPrefab;
                //ランダムな数字を生成
               
                if(this.time>=25){
                   // Bananaのみ出現 
                   CreatePrefab("banana",this.bananaPrefab);
                }else if(this.time>=20){
                    // 0: なまごみ、1: バナナ、2: ゴミ箱
                    int enemyType = Random.Range(0, 2);

                    if(enemyType == 0){
                        enemyPrefab = CreatePrefab("gomiBako",this.gomiBakoPrefab);
                    }else if(enemyType == 1){
                        enemyPrefab = CreatePrefab("banana",this.bananaPrefab);
                        enemyPrefab.GetComponent<BananaController>().SettingSpeed(0.2f);
                    }
                }else if(this.time<=20){
                    // 0: なまごみ、1: バナナ、2: ゴミ箱
                    int enemyType = Random.Range(0, 3);
                    if(enemyType == 0){
                        enemyPrefab = CreatePrefab("gomiBako",this.gomiBakoPrefab);
                        enemyPrefab.GetComponent<GomiBakoController>().SettingSpeed(0.4f);
                    }else if(enemyType == 1){
                        enemyPrefab = CreatePrefab("banana",this.bananaPrefab);
                        enemyPrefab.GetComponent<BananaController>().SettingSpeed(0.3f);
                    }else if(enemyType == 2){
                         CreatePrefab("namaGomi",this.namaGomiPrefab);
                    }
                    if(this.time<=10){
                        if(enemyType == 0){
                            enemyPrefab = CreatePrefab("gomiBako",this.gomiBakoPrefab);
                            enemyPrefab.GetComponent<GomiBakoController>().SettingSpeed(0.5f);
                        }else if(enemyType == 1){
                            enemyPrefab = CreatePrefab("banana",this.bananaPrefab);
                            enemyPrefab.GetComponent<BananaController>().SettingSpeed(0.3f);
                        }else if(enemyType == 2){
                            enemyPrefab=CreatePrefab("namaGomi",this.namaGomiPrefab);
                            enemyPrefab.GetComponent<NamaGomiController>().SettingSpeed(0.1f);
                        }

                    }
                }
            }
            // coinの生成処理
            if(Time.frameCount % 180 == 0){
                int coinChance = Random.Range(0, 10);
                if(coinChance<2){
                    CreatePrefab("coin",this.coinPrefab);
                }
            }
        }else if(this.time<=0&&endFlag){
            // 時間を0にする
            this.time = 0;
            this.timerText.GetComponent<TextMeshProUGUI>().text = this.time.ToString("F1");
            this.gameClearPanel.SetActive(true);
            // 時間が0になったら、ゲームクリア
            DeleteEmnemy();
            // スコアの保存
            this.ranking.SetRank(int.Parse(this.scoreText.GetComponent<TextMeshProUGUI>().text));
            this.endFlag =false;
        }
    }

    // Prefabの作成
    // Note: それぞれの画像に合わせて向きを変える必要があるため関数化する
    GameObject CreatePrefab(string createEnemy,GameObject enemyPrefab){
        Vector3 vector3 = CreateVector3(this.maxXPos,this.maxYPos);
        if(createEnemy.Equals("namaGomi")){
            if(vector3.x > 0){
                enemyPrefab.transform.Rotate(0,180,0);
            }
            return Instantiate(enemyPrefab, vector3, Quaternion.identity);
        }else if(createEnemy.Equals("banana")){
            if(vector3.x > 0){
                enemyPrefab.transform.Rotate(0,180,0);
            }
            return Instantiate(enemyPrefab, vector3, Quaternion.identity);
        }else if(createEnemy.Equals("gomiBako")){
            return Instantiate(enemyPrefab, vector3, Quaternion.identity);
        }else if(createEnemy.Equals("coin")){
            return Instantiate(enemyPrefab, vector3, Quaternion.identity);
        }
        return null;
    }


    
    // create Vector3
    // Note: 決められた範囲何のランダムなVector3座標を作成する
    Vector3 CreateVector3(float maxXPos,float maxYPos){
        //-1.3~1.3の間でランダムな数値を生成
        // 引数には境界値は腑生まれないので、最大値に+1.0fをしている
        float createXPos = RandomFloat(this.maxXPos+1.0f, -this.maxXPos);
       
        // X座標が画面の横幅からはみ出していたら、Y座標は自由にする
        if(createXPos > this.maxXPos || createXPos < -this.maxXPos)
        {
            //Y座標は自由にする
            return new Vector3(createXPos,RandomFloat(this.maxYPos,-this.maxYPos) , 0);
        }
        // X座標が画面内だったr、Y座標は二つの値からランダムに選ぶ
        else
        {
            // 上下のどちらを生成するかを決めるためのランダムな数値
            int randomYPos = Random.Range(0, 2);
            if(randomYPos == 0)
            {
                // 上に生成
                return new Vector3(createXPos, this.maxYPos, 0);
            }else {
                // 下に生成
                return new Vector3(createXPos, -this.maxYPos, 0);
            }
        }
    }

    //引数で渡された範囲のランダムな数値を返す
    float RandomFloat(float maxRange,float minRange){
        return Random.Range(minRange,maxRange);
    }
     public void AddScore(int addScore){
       
        int score = int.Parse(this.scoreText.GetComponent<TextMeshProUGUI>().text);
        score += addScore;
        //足した結果マイナズだった時には0にする
        if(score <= 0){
            score = 0;
        }
        this.scoreText.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
    public void DeleteEmnemy(){
        // 画面内の敵キャラを全て取得
        GameObject[] namaGomi = GameObject.FindGameObjectsWithTag("NamaGomi");
        GameObject[] banana = GameObject.FindGameObjectsWithTag("Banana");
        GameObject[] gomiBako = GameObject.FindGameObjectsWithTag("GomiBako");
        GameObject[] coin = GameObject.FindGameObjectsWithTag("Coin");
        // 画面内の敵キャラを全て削除
        foreach (GameObject obj in namaGomi)
        {
            Destroy(obj);
        }
        foreach (GameObject obj in banana)
        {
            Destroy(obj);
        }
        foreach (GameObject obj in gomiBako)
        {
            Destroy(obj);
        }
        foreach (GameObject obj in coin)
        {
            Destroy(obj);
        }
    }
    public void GameOver(){
        // ゲームオーバー時には、画面内の敵キャラを全て削除
        DeleteEmnemy();
        // ゲームオーバーパネルを表示
        this.gameOverPanel.SetActive(true);
        // 時間も0にする
        this.time = 0;
        // Update関数のゲームクリア条件に引っかからないようにする
        this.endFlag = false;
    }
    
    public void ChangeScoreDetail(string color,int scoreDetail){
        if(scoreDetail == 0){
            return;
        }
        string plusOrMinus = "";
        if(scoreDetail >0){
            plusOrMinus = "+";
        }else if(scoreDetail <0){
            plusOrMinus = "-";
        }
        this.scoreDetailText.GetComponent<TextMeshProUGUI>().text = "<color="+color+">"+plusOrMinus+scoreDetail+"</color>";
        if(color.Equals("red")){
            this.scoreDetailText.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }else if(color.Equals("blue")){
            this.scoreDetailText.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
        }
    }
}
