using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public static bool isUINow; //UIを開いているか判別するトークン

    public GameObject ui;   //UI全体の格納

    TextMeshProUGUI[] tMP;

    int nowIndex = 0;   //現在のカーソル位置

    // Start is called before the first frame update
    void Start()
    {
        tMP = ui.GetComponentsInChildren<TextMeshProUGUI>();

        tMP[nowIndex].color = Color.yellow;

        //UIを非表示にする
        isUINow = false;
        ui.SetActive(isUINow);
    }

    // Update is called once per frame
    void Update()
    {
        //Escを押すとマップに戻る
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isUINow)
            {
                tMP[nowIndex].color = Color.white;
                nowIndex = 0;
                tMP[nowIndex].color = Color.yellow;


                isUINow = false;
                ui.SetActive(isUINow);
            }
        }

        //Aまたは←を押すと左の要素に移る
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            tMP[nowIndex].color = Color.white;

            if (nowIndex == 0)
            {
                nowIndex = tMP.Length - 1;
            } else
            {
                nowIndex -= 1;
            }

            tMP[nowIndex].color = Color.yellow;
        }

        //Dまたは→を押すと右の要素に移る
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            tMP[nowIndex].color = Color.white;

            if (nowIndex == tMP.Length - 1)
            {
                nowIndex = 0;
            }
            else
            {
                nowIndex += 1;
            }

            tMP[nowIndex].color = Color.yellow;
        }
    }
}
