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
    string playerName;
    int playerLv;
    int playerHp = 12;
    int playerSp;
    int playerAtk = 5;
    int playerDef = 2;
    int playerSpeed;

    //モンスター情報
    string monsterName = "スライム";
    int monsterHp = 5;
    int monsterSp;
    int monsterAtk = 4;
    int monsterDef = 2;
    int monsterSpeed;

    //問題ターンか
    bool isQuestionTurn;

    //タイマー
    float leftTime = 15;
    float timer;

    GameObject clickedGameObject;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartBattle");
    }

    // Update is called once per frame
    void Update()
    {
        //問題ターンか
        if (isQuestionTurn)
        {
            
            //制限時間内か
            if (leftTime >= 0)
            {
                //カウントダウン
                leftTime -= Time.deltaTime;
                timeText.GetComponent<Text>().text = leftTime.ToString("f1");
                timeBar.GetComponent<Slider>().value = leftTime;
            }
            if (Input.GetMouseButtonDown(0))
            {
                //選択肢をクリック
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit2D = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

                if (hit2D)
                {
                    isQuestionTurn = false;
                    ChangePhase();
                    clickedGameObject = hit2D.transform.gameObject;
                    //正解か不正解か
                    if(clickedGameObject.tag == "true")
                    {
                        Debug.Log("正解");
                        //ダメージ計算
                        int damage = playerAtk - monsterDef;
                        messageText.GetComponent<Text>().text = "正解!" + monsterName + "に" + damage + "のダメージを与えた";
                        monsterHpBar.GetComponent<Slider>().value = monsterHp - damage;
                    }
                    else
                    {
                        Debug.Log("不正解");
                        messageText.GetComponent<Text>().text = "不正解!";
                    }
                    StartCoroutine("Interval");
                    MonsterTurn();
                }
            }
        }
    }

    void MonsterTurn()
    {
        int damage = monsterAtk - playerDef;
        messageText.GetComponent<Text>().text = "スライムの攻撃!" + damage + "のダメージを受けた";
        playerHpBar.GetComponent<Slider>().value = playerHp - damage;
        StartCoroutine("Interval");
        SetQuestion();
        ChangePhase();
    }

    void SetQuestion()
    {
        //問題文と選択肢
        questionText.GetComponent<Text>().text = "人の不注意に付け込んで機密情報などを不正に入手する手法は？";
        choiceText1.GetComponent<Text>().text = "SQLインジェクション";
        choiceText2.GetComponent<Text>().text = "ソーシャルエンジニアリング";
        choiceText3.GetComponent<Text>().text = "DDOS攻撃";
        choiceText4.GetComponent<Text>().text = "バッファオーバーフロー";

        //正答セット
        choiceWindow1.tag = "false";
        choiceWindow2.tag = "true";
        choiceWindow3.tag = "false";
        choiceWindow4.tag = "false";
    }

    IEnumerator Interval()
    {
        //2秒待つ
        yield return new WaitForSeconds(2);
    }

 

    IEnumerator StartBattle()
    {
        //2秒待つ
        yield return new WaitForSeconds(2);
        playerHpBar.GetComponent<Slider>().maxValue = playerHp;
        playerHpBar.GetComponent<Slider>().value = playerHp;
        monsterHpBar.GetComponent<Slider>().maxValue = monsterHp;
        monsterHpBar.GetComponent<Slider>().value = monsterHp;
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
