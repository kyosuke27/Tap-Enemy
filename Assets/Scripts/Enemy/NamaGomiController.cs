using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NamaGomiController: MonoBehaviour
{
    private float speed;
    private float Hp; 
    private float damage;
    public Slider slider;
    public GameObject director;
    void Start()
    {
        Application.targetFrameRate = 60;
        this.speed = 0.05f;
        this.Hp = 1.5f;
        this.damage = 0.5f;
        // Sliderの最大値を設定
        this.slider.maxValue = (float)Hp;
        // Sliderの値を設定
        // Note: Maxの値を変更しただけでは、Sliderの値は変わらないので、Sliderの値も変更する必要がある
        this.slider.value = (float)Hp;
        this.director = GameObject.Find("GameDirector");
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
    public void OnclickNamaGomi(){
        // Hpを減らす
        this.Hp -= this.damage;
        // Sliderを減らす
        slider.value = (float)Hp; 
        if(this.Hp <= 0){
            this.director.GetComponent<GameDirector>().AddScore(150);
            this.director.GetComponent<GameDirector>().ChangeScoreDetail("red",150);
            // クリックされたら、自分を消す
            Destroy(this.gameObject);
        }
    }
    
    //虫のスピードを設定する
    public void SettingSpeed(float speed){
        this.speed = speed;
    }

}
