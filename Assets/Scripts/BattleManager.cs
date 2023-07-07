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

    //問題ウィンドウ
    public GameObject questionWindow;
    public GameObject questionText;

    //サウンド
    public GameObject BGM;
    public GameObject AttackSound;

    //HP、SPバー
    public GameObject playerHpBar;
    public GameObject playerSpBar;
    public GameObject monsterHpBar;

    //制限時間
    public GameObject timeBar;
    public GameObject timeText;

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

    bool isQuestionTurn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartBattle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetQuestion()
    {
        questionText.GetComponent<Text>().text = "人の不注意に付け込んで機密情報などを不正に入手する手法は？";
        choiceText1.GetComponent<Text>().text = "SQLインジェクション";
        choiceText2.GetComponent<Text>().text = "ソーシャルエンジニアリング";
        choiceText3.GetComponent<Text>().text = "DDOS攻撃";
        choiceText4.GetComponent<Text>().text = "バッファオーバーフロー";
    }

    IEnumerator StartBattle()
    {
        //2秒待つ
        yield return new WaitForSeconds(2);
        isQuestionTurn = true;
        SetQuestion();
        ChangePhase();
    }


    void ChangePhase()
    {
        if (isQuestionTurn)
        {
            //選択肢と問題を出す
            choiceText1.SetActive(true);
            choiceText2.SetActive(true);
            choiceText3.SetActive(true);
            choiceText4.SetActive(true);
            choiceWindow1.SetActive(true);
            choiceWindow2.SetActive(true);
            choiceWindow3.SetActive(true);
            choiceWindow4.SetActive(true);
            questionText.SetActive(true);
            questionWindow.SetActive(true);
            timeText.SetActive(true);
            timeBar.SetActive(true);

            //メッセージウィンドウを消す
            messageText.SetActive(false);
            messageWindow.SetActive(false);
        }
        else
        {
            //選択肢と問題を消す
            choiceText1.SetActive(false);
            choiceText2.SetActive(false);
            choiceText3.SetActive(false);
            choiceText4.SetActive(false);
            choiceWindow1.SetActive(false);
            choiceWindow2.SetActive(false);
            choiceWindow3.SetActive(false);
            choiceWindow4.SetActive(false);
            questionText.SetActive(false);
            questionWindow.SetActive(false);
            timeText.SetActive(false);
            timeBar.SetActive(false);

            //メッセージウィンドウを出す
            messageText.SetActive(true);
            messageWindow.SetActive(true);
        }
    }


}
