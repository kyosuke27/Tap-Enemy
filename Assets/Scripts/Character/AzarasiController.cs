using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AzarasiController: MonoBehaviour
{
    public Slider slider;
    private float Hp;
    private float damage;
    public GameObject director;
    // Start is called before the first frame update
    void Start()
    {
        this.Hp = 10.0f;
        this.damage = 1.0f;
        // Sliderの最大値を設定
        this.slider.maxValue = (float)Hp;
        // Sliderの値を設定
        // Note: Maxの値を変更しただけでは、Sliderの値は変わらないので、Sliderの値も変更する必要がある
        this.slider.value = (float)Hp;
        this.director = GameObject.Find("GameDirector");

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(this.Hp <= 0){
            this.director.GetComponent<GameDirector>().GameOver();
        }
        // Hpを減らす
        this.Hp -= this.damage;
        // Sliderを減らす
        slider.value = (float)Hp; 
        this.director.GetComponent<GameDirector>().AddScore(-100);
            this.director.GetComponent<GameDirector>().ChangeScoreDetail("blue",-100);
        Destroy(collision.gameObject);
    }
    
    public void recoveryHp(float recovery){
        this.Hp += recovery;
        slider.value = (float)Hp;
    }
}
