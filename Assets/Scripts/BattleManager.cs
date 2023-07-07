using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    //選択肢ウィンドウ
    public GameObject choiceWindow1;
    public GameObject choiceWindow2;
    public GameObject choiceWindow3;
    public GameObject choiceWindow4;

    //選択肢テキスト
    public GameObject choiceText1;
    public GameObject choiceText2;
    public GameObject choiceText3;
    public GameObject choiceText4;

    //メッセージウィンドウ
    public GameObject messageWindow;
    public GameObject messageText;

    //サウンド
    public GameObject BGM;
    public GameObject AttackSound;


    //プレイヤー情報
    int playerLv;
    int playerHp;
    int playerSp;
    int playerAtk;
    int playerDef;
    int playerSpeed;

    //モンスター情報
    int monsterHp;
    int monsterSp;
    int monsterAtk;
    int monsterDef;
    int monsterSpeed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartBattle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartBattle()
    {
        //2秒待つ
        yield return new WaitForSeconds(2);

        //メッセージウィンドウを消す
        messageText.SetActive(false);
        messageWindow.SetActive(false);

        //選択肢と問題を出す
        choiceText1.SetActive(true);
        choiceText2.SetActive(true);
        choiceText3.SetActive(true);
        choiceText4.SetActive(true);
        choiceWindow1.SetActive(true);
        choiceWindow2.SetActive(true);
        choiceWindow3.SetActive(true);
        choiceWindow4.SetActive(true);

    }
}
