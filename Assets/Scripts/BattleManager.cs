using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    //スクリプト
    PlayerModel pm;
    SkillModel sm;
    ItemModel im;
    MonsterModel mm;
    PlayerDB p;
    MonsterDB m;
    List<Skill> skills = new List<Skill>();
    List<Item> items = new List<Item>();

    public GameObject monsterImage;

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
    int skillAct = 1;
    int skillActMax;
    int itemActMax;

    //スキルウィンドウ
    public GameObject skillWindow;
    public GameObject expoWindow;

    //スキルテキスト
    public GameObject skillText1;
    public GameObject skillText2;
    public GameObject skillText3;
    public GameObject skillText4;
    public GameObject skillText5;
    public GameObject skillText6;
    List<GameObject> skillTextList = new List<GameObject>();

    //アイテムテキスト
    public GameObject itemText1;
    public GameObject itemText2;
    public GameObject itemText3;
    public GameObject itemText4;
    public GameObject itemText5;
    public GameObject itemText6;
    List<GameObject> itemTextList = new List<GameObject>();

    public GameObject costText;
    public GameObject expoText;

    //サウンド
    public GameObject BGM;
    public GameObject AttackSound;

    //HP、SPバー
    public GameObject playerHpBar;
    public GameObject playerSpBar;
    public GameObject monsterHpBar;
    public GameObject nowHp;
    public GameObject nowSp;
    public GameObject maxHp;
    public GameObject maxSp;

    //制限時間
    public GameObject timeBar;
    public GameObject timeText;

    //プレイヤー情報
    public GameObject playerName;

    //モンスター情報
    public static int monsterID;
    public GameObject monsterText;
    int monsterMaxHp;

    //ターン判定
    bool isQuestionTurn;
    bool isActTurn;
    bool isSkillTurn;
    bool isItemTurn;

    //タイマー
    float remainTime;
    float timer;

    //選んだ選択肢
    GameObject playerChoice;

    //スキル、アイテム
    public int costSp;
    public float timeRate;
    public float playerAtkRate;
    public float playerDefRate;
    public int addDamage;

    int battleSpeed = 1;

    void Start()
    {
        sm = GetComponent<SkillModel>(); //スキル情報
        im = GetComponent<ItemModel>(); //アイテム情報
        mm = GetComponent<MonsterModel>(); //モンスター情報
        pm = GetComponent<PlayerModel>();//プレイヤー情報
        StartCoroutine("StartBattle");
    }

    //バトル初期設定
    IEnumerator StartBattle()
    {

        //プレイヤー情報
        p = pm.PlayerSet();

        //モンスター情報
        m = mm.MonsterDB(3);

        //画像
        var monsterSprite = monsterImage.GetComponent<SpriteRenderer>();
        monsterSprite.sprite = m.image;

        monsterText.GetComponent<Text>().text = m.name;
        messageText.GetComponent<Text>().text = m.name + "があらわれた！";

        //HP設定
        playerName.GetComponent<Text>().text = p.name;
        playerHpBar.GetComponent<Slider>().maxValue = p.maxHp;
        playerHpBar.GetComponent<Slider>().value = p.nowHp;
        playerSpBar.GetComponent<Slider>().maxValue = p.maxSp;
        playerSpBar.GetComponent<Slider>().value = p.nowSp;
        monsterHpBar.GetComponent<Slider>().maxValue = m.hp;
        monsterHpBar.GetComponent<Slider>().value = m.hp;
        maxHp.GetComponent<Text>().text = p.maxHp.ToString();
        nowHp.GetComponent<Text>().text = p.nowHp.ToString();
        maxSp.GetComponent<Text>().text = p.maxSp.ToString();
        nowSp.GetComponent<Text>().text = p.nowSp.ToString();

        playerAtkRate = 1;
        playerDefRate = 1;
        timeRate = 1;
        remainTime = 15;

        skillTextList.Add(skillText1);
        skillTextList.Add(skillText2);
        skillTextList.Add(skillText3);
        skillTextList.Add(skillText4);
        skillTextList.Add(skillText5);
        skillTextList.Add(skillText6);
        itemTextList.Add(itemText1);
        itemTextList.Add(itemText2);
        itemTextList.Add(itemText3);
        itemTextList.Add(itemText4);
        itemTextList.Add(itemText5);
        itemTextList.Add(itemText6);

        sm.Set();
        int a = 0;
        foreach(int x in p.skillID)
        {
            skills.Add(sm.SkillSet(x));
            skillTextList[a].GetComponent<Text>().text = skills[a].name;
            a++;
        }

        skillActMax = skills.Count;

        im.Set();
        int itemId = 1;
        a = 0;
        foreach (int x in p.haveItem)
        {
            Item i = im.ItemSet(x, itemId);
            if(i != null)
            {
                items.Add(i);
                itemTextList[items.Count - 1].GetComponent<Text>().text = items[items.Count - 1].name;
            }
            itemId++;
        }

        itemActMax = items.Count;

        yield return new WaitForSeconds(battleSpeed);
        
        //行動ターンにする
        isActTurn = true;
        isQuestionTurn = false;
        ActActive();
    }


    void Update()
    {
        //行動ターンか
        if (isActTurn)
        {
            if (Input.GetKeyDown(KeyCode.W) && act != 1)
            {
                //下に矢印を動かす
                act--;
                actSelect.transform.Translate(-0.65f,0,0);
            }
            if (Input.GetKeyDown(KeyCode.S) && act != 4)
            {
                //上に矢印を動かす
                act++;
                actSelect.transform.Translate(0.65f,0,0);
            }
            if(Input.GetKeyDown(KeyCode.Return))
            {
                //たたかう
                if(act == 1)
                {
                    isActTurn = false;
                    isQuestionTurn = true;
                    SetQuestion();
                    QuestionActive();
                }
                //スキル
                else if(act == 2)
                {
                    Invoke("SkillActive", 0.1f);
                    
                }
                else if(act == 3)
                {
                    Invoke("ItemActive", 0.1f);
                }
                //逃げる
                else if(act == 4)
                {
                    isActTurn = false;
                    MessageActive();
                    StartCoroutine("EndBattle");
                }
            }
        }

        //スキルターンか
        if (isSkillTurn)
        {
            costText.GetComponent<Text>().text = skills[skillAct - 1].cost.ToString() + " SP";
            expoText.GetComponent<Text>().text = skills[skillAct - 1].expo;

            if (Input.GetKeyDown(KeyCode.W) && skillAct != 1 && skillAct != 4)
            {
                //矢印を動かす
                skillAct--;
                actSelect.transform.Translate(-0.8f, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.S) && skillAct != 3 && skillAct != 6 && skillAct < skillActMax)
            {
                //矢印を動かす
                skillAct++;
                actSelect.transform.Translate(0.8f, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.A) && skillAct != 1 && skillAct != 2 && skillAct != 3)
            {
                //矢印を動かす
                skillAct -= 3;
                actSelect.transform.Translate(0, -5.25f, 0);
            }
            if (Input.GetKeyDown(KeyCode.D) && skillAct != 4 && skillAct != 5 && skillAct != 6 && skillAct + 2 < skillActMax)
            {
                //矢印を動かす
                skillAct += 3;
                actSelect.transform.Translate(0, 5.25f, 0);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //矢印を動かす
                ActActive();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine("SkillUse", skills[skillAct - 1]);
            }
        }

        //アイテム
        if (isItemTurn)
        {
            costText.GetComponent<Text>().text = items[skillAct - 1].have.ToString() + " 個";
            expoText.GetComponent<Text>().text = items[skillAct - 1].expo;

            if (Input.GetKeyDown(KeyCode.W) && skillAct != 1 && skillAct != 4)
            {
                //矢印を動かす
                skillAct--;
                actSelect.transform.Translate(-0.8f, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.S) && skillAct != 3 && skillAct != 6 && skillAct < itemActMax)
            {
                //矢印を動かす
                skillAct++;
                actSelect.transform.Translate(0.8f, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.A) && skillAct != 1 && skillAct != 2 && skillAct != 3)
            {
                //矢印を動かす
                skillAct -= 3;
                actSelect.transform.Translate(0, -5.25f, 0);
            }
            if (Input.GetKeyDown(KeyCode.D) && skillAct != 4 && skillAct != 5 && skillAct != 6 && skillAct + 2 < itemActMax)
            {
                //矢印を動かす
                skillAct += 3;
                actSelect.transform.Translate(0, 5.25f, 0);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ActActive();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine("ItemUse", items[skillAct - 1]);
            }
        }

        //問題ターンか
        if (isQuestionTurn)
        {
            
            //制限時間内か
            if (remainTime >= 0)
            {
                //カウントダウン
                remainTime -= Time.deltaTime * timeRate;
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
                MessageActive();
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
                    MessageActive();
                    playerChoice = hit2D.transform.gameObject;

                    //正解か不正解か
                    if(playerChoice.tag == "true")
                    {
                        //テキスト表示
                        messageText.GetComponent<Text>().text = "正解！" + p.name + "の攻撃！";
                        StartCoroutine("PlayerTurn");
                    }
                    else
                    {
                        //テキスト表示
                        messageText.GetComponent<Text>().text = "不正解...";
                        //敵のターン
                        StartCoroutine("MonsterTurn");
                    }
                }
            }
        }
    }

    IEnumerator SkillUse(Skill skill)
    {
        //スキル
        isSkillTurn = false;
        MessageActive();

        if (0 > p.nowSp - skill.cost)
        {
            messageText.GetComponent<Text>().text = "SPが足りない！";
            yield return new WaitForSeconds(battleSpeed);
            isActTurn = true;
            ActActive();
        }
        else
        {
            messageText.GetComponent<Text>().text = p.name + "の" + skill.name + "!\n" + skill.message;
            sm.SkillUse(skill.id);
            p.nowSp -= skill.cost;
            //SPバー反映
            playerSpBar.GetComponent<Slider>().value = p.nowSp;
            nowSp.GetComponent<Text>().text = p.nowSp.ToString();

            yield return new WaitForSeconds(battleSpeed);
            
            isQuestionTurn = true;
            SetQuestion();
            QuestionActive();
        }
    }

    //アイテム
    IEnumerator ItemUse(Item item)
    {
        isItemTurn = false;
        MessageActive();
        messageText.GetComponent<Text>().text = p.name + "は" + item.name + "使った！";
        yield return new WaitForSeconds(battleSpeed);

        //アイテムの個数減らす
        items[skillAct - 1].have--;
        if(items[skillAct - 1].have == 0)
        {
            items.RemoveAt(skillAct - 1);
            for(int i = 0; i < items.Count; i++)
            {
                itemTextList[i].GetComponent<Text>().text = items[i].name;
            }
            itemActMax = items.Count;
            itemTextList[itemActMax].GetComponent<Text>().text = "";
        }

        im.ItemUse(item.id);
        yield return new WaitForSeconds(battleSpeed);

        isQuestionTurn = true;
        SetQuestion();
        QuestionActive();
    }

    //回復
    public void Heal(int heal)
    {
        int healValue;
        if (p.nowHp + heal > p.maxHp)
        {
            healValue = p.maxHp - p.nowHp;
            p.nowHp = p.maxHp;
        }
        else
        {
            healValue = heal;
            p.nowHp += heal;
        }
        playerHpBar.GetComponent<Slider>().value = p.nowHp;
        nowHp.GetComponent<Text>().text = p.nowHp.ToString();
        messageText.GetComponent<Text>().text = p.name + "は" + healValue + "回復した！";
    }

    IEnumerator PlayerTurn()
    {
        //2秒待つ
        yield return new WaitForSeconds(battleSpeed);
        //ダメージ計算
        int damage = (int)Mathf.Floor(p.atk * playerAtkRate - m.def);

        //HP反映
        if (damage >= 1)
        {
            m.hp -= damage;
            messageText.GetComponent<Text>().text = m.name + "に" + damage + "のダメージを与えた";
            //HPバー反映
            monsterHpBar.GetComponent<Slider>().value = m.hp;
            if (addDamage != 0)
            {
                yield return new WaitForSeconds(battleSpeed);
                m.hp -= addDamage;
                messageText.GetComponent<Text>().text = m.name + "に追加で" + addDamage + "のダメージを与えた";
                //HPバー反映
                monsterHpBar.GetComponent<Slider>().value = m.hp;
            }
        }
        else
        {
            messageText.GetComponent<Text>().text = "ミス！ダメージを与えられなかった！";
        }

        //戦闘終了判定
        if (m.hp <= 0)
        {
            //プレイヤーの勝ち
            StartCoroutine("EndBattle");
        }
        else
        {
            //敵のターン
            StartCoroutine("MonsterTurn");
        }

    }

    //敵のターン
    IEnumerator MonsterTurn()
    {
        //2秒待つ
        yield return new WaitForSeconds(battleSpeed);
        messageText.GetComponent<Text>().text = m.name + "の攻撃！";
        //2秒待つ
        yield return new WaitForSeconds(battleSpeed);
        //ダメージ計算
        int damage = (int)Mathf.Floor(m.atk - p.def * playerDefRate);
        if(damage >= 1)
        {
            //HP反映
            p.nowHp -= damage;
            messageText.GetComponent<Text>().text = damage + "のダメージを受けた";
            //HPバー反映
            if (p.nowHp <= 0)
            {
                playerHpBar.GetComponent<Slider>().value = 0;
                nowHp.GetComponent<Text>().text = "0";

            }
            else
            {
                playerHpBar.GetComponent<Slider>().value = p.nowHp;
                nowHp.GetComponent<Text>().text = p.nowHp.ToString();
            }
        }
        else
        {
            messageText.GetComponent<Text>().text = "ミス！ダメージを受けなかった！";
        }
        //ダメージバー変更
        playerHpBar.GetComponent<Slider>().value = p.nowHp;
        //2秒待つ
        yield return new WaitForSeconds(battleSpeed);
        //戦闘終了判定
        if (p.nowHp <= 0)
        {
            //プレイヤーの負け
            StartCoroutine("EndBattle");
        }
        else
        {
            RateReset();
            //行動ターンにする
            isActTurn = true;
            //ターン変更
            ActActive();
        }
    }

    //戦闘終了
    IEnumerator EndBattle()
    {
        //2秒待つ
        isQuestionTurn = false;
        if (m.hp <= 0)
        {
            messageText.GetComponent<Text>().text = "敵を倒した！";
        }
        else if(p.nowHp <= 0)
        {
            messageText.GetComponent<Text>().text = "全滅した...";
        }
        else
        {
            messageText.GetComponent<Text>().text = "逃げ出した！";
        }
        //2秒待つ
        yield return new WaitForSeconds(battleSpeed);
    }

    
    //問題セット
    void SetQuestion()
    {
        //問題文と選択肢
        questionText.GetComponent<Text>().text = "人の不注意に付け込んで機密情報などを不正に入手する手法は？";
        choiceText1.GetComponent<Text>().text = "プログラミング言語を使ってソースコードをかくこと。";
        choiceText2.GetComponent<Text>().text = "ソーシャルエンジニアリング";
        choiceText3.GetComponent<Text>().text = "DDOS攻撃";
        choiceText4.GetComponent<Text>().text = "バッファオーバーフロー";

        //正答セット
        choiceWindow1.tag = "false";
        choiceWindow2.tag = "true";
        choiceWindow3.tag = "false";
        choiceWindow4.tag = "false";
    }

    //倍率リセット
    void RateReset()
    {
        playerAtkRate = 1;
        playerDefRate = 1;
        timeRate = 1;
        addDamage = 0;
    }

    //問題出す
    void QuestionActive()
    {
        //問題出す
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
        remainTime = 15;

        //メッセージウィンドウを消す
        messageText.SetActive(false);
        messageWindow.SetActive(false);

        //行動消す
        actSelect.SetActive(false);
        actWindow.SetActive(false);
        fightText.SetActive(false);
        skillText.SetActive(false);
        itemText.SetActive(false);
        escapeText.SetActive(false);
    }

    //行動出す
    void ActActive()
    {
        isActTurn = true;
        isSkillTurn = false;
        isItemTurn = false;
        //行動出す
        actSelect.SetActive(true);
        actWindow.SetActive(true);
        fightText.SetActive(true);
        skillText.SetActive(true);
        itemText.SetActive(true);
        escapeText.SetActive(true);
        actSelect.transform.position = new Vector3(-5.7f, -1.6f, 0);
        skillAct = 1;
        act = 1; 

        //スキル消す
        skillWindow.SetActive(false);
        skillText1.SetActive(false);
        skillText2.SetActive(false);
        skillText3.SetActive(false);
        skillText4.SetActive(false);
        skillText5.SetActive(false);
        skillText6.SetActive(false);

        itemText1.SetActive(false);
        itemText2.SetActive(false);
        itemText3.SetActive(false);
        itemText4.SetActive(false);
        itemText5.SetActive(false);
        itemText6.SetActive(false);

        expoWindow.SetActive(false);
        expoText.SetActive(false);
        costText.SetActive(false);

        //メッセージウィンドウを消す
        messageText.SetActive(false);
        messageWindow.SetActive(false);
    }

    //スキル出す
    void SkillActive()
    {
        //スキル出す
        skillWindow.SetActive(true);
        skillText1.SetActive(true);
        skillText2.SetActive(true);
        skillText3.SetActive(true);
        skillText4.SetActive(true);
        skillText5.SetActive(true);
        skillText6.SetActive(true);
        actSelect.transform.position = new Vector3(-5, -1.9f, 0);

        expoWindow.SetActive(true);
        expoText.SetActive(true);
        costText.SetActive(true);

        //行動消す
        actWindow.SetActive(false);
        fightText.SetActive(false);
        skillText.SetActive(false);
        itemText.SetActive(false);
        escapeText.SetActive(false);

        isSkillTurn = true;
        isActTurn = false;
    }

    void ItemActive()
    {
        //スキル出す
        skillWindow.SetActive(true);
        itemText1.SetActive(true);
        itemText2.SetActive(true);
        itemText3.SetActive(true);
        itemText4.SetActive(true);
        itemText5.SetActive(true);
        itemText6.SetActive(true);
        actSelect.transform.position = new Vector3(-5, -1.9f, 0);

        expoWindow.SetActive(true);
        expoText.SetActive(true);
        costText.SetActive(true);

        //行動消す
        actWindow.SetActive(false);
        fightText.SetActive(false);
        skillText.SetActive(false);
        itemText.SetActive(false);
        escapeText.SetActive(false);

        isItemTurn = true;
        isActTurn = false;
    }

    //メッセージウィンドウを出す
    void MessageActive()
    {
        //メッセージウィンドウを出す
        messageText.SetActive(true);
        messageWindow.SetActive(true);

        //スキル消す
        skillWindow.SetActive(false);
        skillText1.SetActive(false);
        skillText2.SetActive(false);
        skillText3.SetActive(false);
        skillText4.SetActive(false);
        skillText5.SetActive(false);
        skillText6.SetActive(false);
        actSelect.SetActive(false);

        itemText1.SetActive(false);
        itemText2.SetActive(false);
        itemText3.SetActive(false);
        itemText4.SetActive(false);
        itemText5.SetActive(false);
        itemText6.SetActive(false);

        //行動消す
        actSelect.SetActive(false);
        actWindow.SetActive(false);
        fightText.SetActive(false);
        skillText.SetActive(false);
        itemText.SetActive(false);
        escapeText.SetActive(false);

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

        expoWindow.SetActive(false);
        expoText.SetActive(false);
        costText.SetActive(false);
    }
}
