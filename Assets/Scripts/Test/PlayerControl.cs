using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerControl : MonoBehaviour
{
    float speed = 0.005f;    //移動速度の基本倍率
    public GameObject ui;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!UIControl.isUINow)
        {
            //Wまたは↑を押すと上に進む
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                //(x,y,z)で指定
                this.transform.Translate(0.0f, 1.0f * speed, 0.0f);
            }

            //Aまたは←を押すと左に進む
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                //(x,y,z)で指定
                this.transform.Translate(-1.0f * speed, 0.0f, 0.0f);
            }

            //Sまたは↓を押すと下に進む
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                //(x,y,z)で指定
                this.transform.Translate(0.0f, -1.0f * speed, 0.0f);
            }

            //Dまたは→を押すと右に進む
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                //(x,y,z)で指定
                this.transform.Translate(1.0f * speed, 0.0f, 0.0f);
            }

            //Shiftを押すとダッシュする
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = 0.01f;
            }

            //Shiftを離すと元の速さに戻る
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = 0.005f;
            }

            //Escを押すとメニューを表示する
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UIControl.isUINow = true;
                ui.SetActive(UIControl.isUINow);
            }
        }
    }
}
