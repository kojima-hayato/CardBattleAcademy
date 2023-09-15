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

    //行動ウィンドウ
    public GameObject actWindow;
    public GameObject actSelect;
    int act = 1;

    //行動テキスト
    public GameObject fightText;
    public GameObject skillText;
    public GameObject itemText;
    public GameObject escapeText;

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
    bool isActTurn;

    //タイマー
    float remainTime = 15;
    float timer;

    //選んだ選択肢
    GameObject playerChoice;


    void Start()
    {
        StartCoroutine("StartBattle");
    }

    //バトル初期設定
    IEnumerator StartBattle()
    {
        //2秒待つ
        yield return new WaitForSeconds(2);
        //HP設定
        playerHpBar.GetComponent<Slider>().maxValue = playerHp;
        playerHpBar.GetComponent<Slider>().value = playerHp;
        monsterHpBar.GetComponent<Slider>().maxValue = monsterHp;
        monsterHpBar.GetComponent<Slider>().value = monsterHp;
        
        //行動ターンにする
        isActTurn = true;
        isQuestionTurn = false;
        //ターン変更
        ChangeTurn();
    }

    //問題ターン処理
    void Update()
    {
        //行動ターンか
        if (isActTurn)
        {
            if (Input.GetKeyDown(KeyCode.W) && act != 1)
            {
                //矢印を動かす
                act--;
                actSelect.transform.Translate(-0.5f,0,0);
            }
            if (Input.GetKeyDown(KeyCode.S) && act != 4)
            {
                //矢印を動かす
                act++;
                actSelect.transform.Translate(0.5f,0,0);
            }
            if(Input.GetKeyDown(KeyCode.Return) && act == 1)
            {
                isActTurn = false;
                isQuestionTurn = true;
                SetQuestion();
                ChangeTurn();
            }
        }

        //問題ターンか
        if (isQuestionTurn)
        {
            
            //制限時間内か
            if (remainTime >= 0)
            {
                //カウントダウン
                remainTime -= Time.deltaTime;
                //小数第一位まで
                timeText.GetComponent<Text>().text = remainTime.ToString("f1");
                //制限時間バー反映
                timeBar.GetComponent<Slider>().value = remainTime;
            }
            else
            //時間が0になったら
            {
                messageText.GetComponent<Text>().text = "時間切れ...";
                isQuestionTurn = false;
                ChangeTurn();
                //敵のターン
                StartCoroutine("MonsterTurn");
            }
            //左クリックしたら
            if (Input.GetMouseButtonDown(0))
            {
                //クリックした位置を取得
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //クリックした位置にあるオブジェクトを取得
                RaycastHit2D hit2D = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

                //オブジェクトを取得していたら
                if (hit2D)
                {
                    isQuestionTurn = false;
                    ChangeTurn();
                    playerChoice = hit2D.transform.gameObject;

                    //正解か不正解か
                    if(playerChoice.tag == "true")
                    {
                        //ダメージ計算
                        int damage = playerAtk - monsterDef;
                        //テキスト表示
                        messageText.GetComponent<Text>().text = "正解!" + monsterName + "に" + damage + "のダメージを与えた";
                        //HP反映
                        monsterHp -= damage;
                        //HPバー反映
                        monsterHpBar.GetComponent<Slider>().value = monsterHp;
                    }
                    else
                    {
                        //テキスト表示
                        messageText.GetComponent<Text>().text = "不正解...";
                    }
                    //戦闘終了判定
                    if (monsterHp <= 0)
                    {
                        isActTurn = false;
                        isQuestionTurn = false;
                        EndBattle();
                    }
                    else
                    {
                        //敵のターン
                        StartCoroutine("MonsterTurn");
                    }
                }
            }
        }
    }

    //敵のターン
    IEnumerator MonsterTurn()
    {
        //2秒待つ
        yield return new WaitForSeconds(2);
        //ダメージ計算
        int damage = monsterAtk - playerDef;
        //メッセージ出力
        messageText.GetComponent<Text>().text = "スライムの攻撃!" + damage + "のダメージを受けた";
        //HP反映
        playerHp -= damage;
        //ダメージバー変更
        playerHpBar.GetComponent<Slider>().value = playerHp;
        //2秒待つ
        yield return new WaitForSeconds(2);
        //戦闘終了判定
        if (playerHp <= 0)
        {
            isActTurn = false;
            isQuestionTurn = false;
            EndBattle();
        }
        else
        {
            //行動ターンにする
            isActTurn = true;
            //ターン変更
            ChangeTurn();
        }
    }

    IEnumerator EndBattle()
    {
        if(playerHp == 0)
        {
            messageText.GetComponent<Text>().text = "敵を倒した！";
        }
        else
        {
            messageText.GetComponent<Text>().text = "全滅した...";
        }
        //2秒待つ
        yield return new WaitForSeconds(2);
    }

    //問題セット
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

        //制限時間リセット
        remainTime = 15;
    }

    //問題ターンとその他ターンの変更
    void ChangeTurn()
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

        //行動ターンか
        if (isActTurn)
        {
            actSelect.SetActive(true);
            actWindow.SetActive(true);
            fightText.SetActive(true);
            skillText.SetActive(true);
            itemText.SetActive(true);
            escapeText.SetActive(true);

            //メッセージウィンドウを消す
            messageText.SetActive(false);
            messageWindow.SetActive(false);
        }
        else
        {
            actSelect.SetActive(false);
            actWindow.SetActive(false);
            fightText.SetActive(false);
            skillText.SetActive(false);
            itemText.SetActive(false);
            escapeText.SetActive(false);
        }
    }
}
