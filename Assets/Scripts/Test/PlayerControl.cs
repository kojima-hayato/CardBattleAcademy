using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    float speed = 0.01f;    //移動速度の基本倍率

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Wキーを押すと上に進む
        if (Input.GetKey(KeyCode.W))
        {
            //(x,y,z)で指定
            this.transform.Translate(0.0f, 1.0f * speed, 0.0f);
        }

        //Aキーを押すと左に進む
        if (Input.GetKey(KeyCode.A))
        {
            //(x,y,z)で指定
            this.transform.Translate(-1.0f * speed, 0.0f, 0.0f);
        }

        //Sキーを押すと上に進む
        if (Input.GetKey(KeyCode.S))
        {
            //(x,y,z)で指定
            this.transform.Translate(0.0f, -1.0f * speed, 0.0f);
        }

        //Dキーを押すと左に進む
        if (Input.GetKey(KeyCode.D))
        {
            //(x,y,z)で指定
            this.transform.Translate(1.0f * speed, 0.0f, 0.0f);
        }

        //Escキーを押すとメニューを表示する
        if (Input.GetKey(KeyCode.Escape))
        {
        }
    }
}
