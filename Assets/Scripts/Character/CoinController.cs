using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController: MonoBehaviour
{
    private float speed;
    public GameObject director;
    public GameObject azarashi;
    // Start is called before the first frame update
    void Start()
    {
        this.speed = 0.1f;
        this.director = GameObject.Find("GameDirector");
        this.azarashi = GameObject.Find("Azarasi");
    }

    // Update is called once per frame
    void Update()
    {
       // 原点までのオブジェクトのベクトルを求める
        Vector3 pos = Vector3.zero-transform.position;
        // ベクトルを正規化して、スピードをかける
        transform.position +=pos.normalized * speed*0.1f;
    }
    // Click時のイベント
    public void OnclickCoin(){
        // Scoreの更新
        this.director.GetComponent<GameDirector>().AddScore(200);
            this.director.GetComponent<GameDirector>().ChangeScoreDetail("red",200);
        // azarashiのHP回復
        this.azarashi.GetComponent<AzarasiController>().recoveryHp(1.0f);
        // クリックされたら、自分を消す
        Destroy(this.gameObject);
    }
    public void SettingSpeed(float speed){
        this.speed = speed;
    }
}
