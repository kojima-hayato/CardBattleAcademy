using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    DataBaseConnector dbc;
    DataTable dt;

    //�X�N���v�g
    PlayerModel p;
    MonsterModel m;
    SkillModel sm;
    ItemModel im;
    QuestionModel qm;
    List<SkillModel> skills = new List<SkillModel>();
    List<ItemModel> items = new List<ItemModel>();

    public GameObject monsterImage;

    //�I�����E�B���h�E
    public GameObject choiceWindow1;
    public GameObject choiceWindow2;
    public GameObject choiceWindow3;
    public GameObject choiceWindow4;

    //�I�����e�L�X�g
    public GameObject choiceText1;
    public GameObject choiceText2;
    public GameObject choiceText3;
    public GameObject choiceText4;

    //���b�Z�[�W�E�B���h�E
    public GameObject messageWindow;
    public GameObject messageText;

    //���E�B���h�E
    public GameObject questionWindow;
    public GameObject questionText;

    //�s���E�B���h�E
    public GameObject actWindow;
    public GameObject actSelect;
    int act = 1;

    //�s���e�L�X�g
    public GameObject fightText;
    public GameObject skillText;
    public GameObject itemText;
    public GameObject escapeText;
    int skillAct = 1;
    int skillActMax;
    int itemActMax;

    //�X�L���E�B���h�E
    public GameObject skillWindow;
    public GameObject expoWindow;

    //�X�L���e�L�X�g
    public GameObject skillText1;
    public GameObject skillText2;
    public GameObject skillText3;
    public GameObject skillText4;
    public GameObject skillText5;
    public GameObject skillText6;
    List<GameObject> skillTextList = new List<GameObject>();

    //�A�C�e���e�L�X�g
    public GameObject itemText1;
    public GameObject itemText2;
    public GameObject itemText3;
    public GameObject itemText4;
    public GameObject itemText5;
    public GameObject itemText6;
    List<GameObject> itemTextList = new List<GameObject>();

    public GameObject costText;
    public GameObject expoText;

    //�T�E���h
    public GameObject BGM;
    public GameObject AttackSound;

    //HP�ASP�o�[
    public GameObject playerHpBar;
    public GameObject playerSpBar;
    public GameObject monsterHpBar;
    public GameObject nowHp;
    public GameObject nowSp;
    public GameObject maxHp;
    public GameObject maxSp;

    //��������
    public GameObject timeBar;
    public GameObject timeText;

    //�v���C���[���
    public GameObject playerName;

    //�����X�^�[���
    public string enemyId;
    public static int monsterID;
    public GameObject monsterText;
    int monsterMaxHp;

    //�^�[������
    bool isQuestionTurn;
    bool isActTurn;
    bool isSkillTurn;
    bool isItemTurn;

    //�^�C�}�[
    float remainTime;
    float timer;

    //�I�񂾑I����
    GameObject playerChoice;

    //�X�L���A�A�C�e��
    public int costSp;
    public float timeRate;
    public float playerAtkRate;
    public float playerDefRate;
    public int addDamage;

    int battleSpeed = 1;

    string sql;
    List<QuestionModel> questionList = new List<QuestionModel>();

    void Start()
    {
        dbc = new();
        dt = new();
        dbc.SetCommand();

        //�v���C���[
        sql = "SELECT" +
            " *" +
            " FROM" +
            " data_hero_status AS dhs" +
            " ;";
        dt = dbc.ExecuteSQL(sql);
        foreach (DataRow row in dt.Rows)
        {
            p = new();
            p.name = row["hero_name"].ToString();
            p.lv = (int)row["hero_level"];
            p.maxHp = (int)row["hero_max_hp"];
            p.nowHp = (int)row["hero_hp"];
            p.maxSp = (int)row["hero_max_sp"];
            p.nowSp = (int)row["hero_sp"];
            p.atk = (int)row["hero_attack"];
            p.def = (int)row["hero_defense"];
        }

        //�����X�^�[
        //enemyId�������
        enemyId = "e0002";

        sql = "SELECT" +
            " *" +
            " FROM" +
            " data_enemy_status AS des" +
            " WHERE" +
            " enemy_id = '" + enemyId + "'" +
            " ;";
        dt = dbc.ExecuteSQL(sql);

        foreach (DataRow row in dt.Rows)
        {
            m = new();
            m.name = row["enemy_name"].ToString();
            m.exp = (int)row["enemy_exp"];
            m.hp = (int)row["enemy_hp"];
            m.atk = (int)row["enemy_attack"];
            m.def = (int)row["enemy_defense"];
            m.imagePath = row["image_path"].ToString();
        }
        

        //�摜
        m.image = Resources.Load<Sprite>(m.imagePath);
        var monsterSprite = monsterImage.GetComponent<SpriteRenderer>();
        monsterSprite.sprite = m.image;

        //���
        sql = "SELECT" +
            " question," +
            " choice1," +
            " choice2," +
            " choice3," +
            " choice4" +
            " FROM" +
            " data_question AS dq" +
            " ;";
        dt = dbc.ExecuteSQL(sql);

        foreach (DataRow row in dt.Rows)
        {
            qm = new();
            qm.question = row["question"].ToString();
            qm.choice1 = row["choice1"].ToString();
            qm.choice2 = row["choice2"].ToString();
            qm.choice3 = row["choice3"].ToString();
            qm.choice4 = row["choice4"].ToString();
            questionList.Add(qm);
        }

        //�X�L��
        skillTextList.Add(skillText1);
        skillTextList.Add(skillText2);
        skillTextList.Add(skillText3);
        skillTextList.Add(skillText4);
        skillTextList.Add(skillText5);
        skillTextList.Add(skillText6);

        sql = "SELECT" +
            " skill_id," +
            " skill_name," +
            " cost," +
            " value," +
            " message," +
            " expo" +
            " FROM" +
            " data_hero_status AS dhs," +
            " data_skill AS ds" +
            " WHERE dhs.hero_level >= ds.hero_level" +
            " ;";
        dt = dbc.ExecuteSQL(sql);

        int i = 0;
        foreach (DataRow row in dt.Rows)
        {
            sm = new();
            sm.id = row["skill_id"].ToString();
            sm.name = row["skill_name"].ToString();
            sm.cost = (int)row["cost"];
            sm.value = (float)row["value"];
            sm.message = row["message"].ToString();
            sm.expo = row["expo"].ToString();
            skillTextList[i].GetComponent<Text>().text = sm.name;
            skills.Add(sm);
            i++;
        }
        skillActMax = skills.Count;

        //�A�C�e��
        itemTextList.Add(itemText1);
        itemTextList.Add(itemText2);
        itemTextList.Add(itemText3);
        itemTextList.Add(itemText4);
        itemTextList.Add(itemText5);
        itemTextList.Add(itemText6);

        sql = "SELECT" +
            " ii.item_id," +
            " item_name," +
            " item_type," +
            " item_value," +
            " quantity," +
            " item_expo" +
            " FROM" +
            " data_item AS di" +
            " JOIN" + 
            " inventory_item AS ii ON di.item_id = ii.item_id" +
            " WHERE" +
            " di.item_id LIKE 'i%'" +
            " AND" +
            " quantity <> 0" + 
            " ;";
        dt = dbc.ExecuteSQL(sql);
        i = 0;
        foreach (DataRow row in dt.Rows)
        {
            im = new();
            im.id = row["item_id"].ToString();
            im.name = row["item_name"].ToString();
            im.type = row["item_type"].ToString();
            im.value = (int)row["item_value"];
            im.quantity = (int)row["quantity"];
            im.expo = row["item_expo"].ToString();
            itemTextList[i].GetComponent<Text>().text = im.name;
            items.Add(im);
            i++;
        }
        itemActMax = items.Count;

        StartCoroutine("StartBattle");
    }

    //�o�g�������ݒ�
    IEnumerator StartBattle()
    {

        monsterText.GetComponent<Text>().text = m.name;
        messageText.GetComponent<Text>().text = m.name + "�������ꂽ�I";

        //HP�ݒ�
        playerName.GetComponent<Text>().text = p.name;
        playerHpBar.GetComponent<Slider>().maxValue = p.maxHp;
        playerHpBar.GetComponent<Slider>().value = p.nowHp;
        playerSpBar.GetComponent<Slider>().maxValue = p.maxSp;
        playerSpBar.GetComponent<Slider>().value = p.nowSp;
        maxHp.GetComponent<Text>().text = p.maxHp.ToString();
        nowHp.GetComponent<Text>().text = p.nowHp.ToString();
        maxSp.GetComponent<Text>().text = p.maxSp.ToString();
        nowSp.GetComponent<Text>().text = p.nowSp.ToString();

        monsterHpBar.GetComponent<Slider>().maxValue = m.hp;
        monsterHpBar.GetComponent<Slider>().value = m.hp;


        playerAtkRate = 1;
        playerDefRate = 1;
        timeRate = 1;
        remainTime = 15;



        yield return new WaitForSeconds(battleSpeed);
        
        //�s���^�[���ɂ���
        isActTurn = true;
        isQuestionTurn = false;
        ActActive();
    }


    void Update()
    {
        //�s���^�[����
        if (isActTurn)
        {
            if (Input.GetKeyDown(KeyCode.W) && act != 1)
            {
                //���ɖ��𓮂���
                act--;
                actSelect.transform.Translate(-0.65f,0,0);
            }
            if (Input.GetKeyDown(KeyCode.S) && act != 4)
            {
                //��ɖ��𓮂���
                act++;
                actSelect.transform.Translate(0.65f,0,0);
            }
            if(Input.GetKeyDown(KeyCode.Return))
            {
                //��������
                if(act == 1)
                {
                    isActTurn = false;
                    isQuestionTurn = true;
                    SetQuestion();
                    QuestionActive();
                }
                //�X�L��
                else if(act == 2)
                {
                    Invoke("SkillActive", 0.1f);
                    
                }
                else if(act == 3)
                {
                    Invoke("ItemActive", 0.1f);
                }
                //������
                else if(act == 4)
                {
                    isActTurn = false;
                    MessageActive();
                    StartCoroutine("EndBattle");
                }
            }
        }

        //�X�L���^�[����
        if (isSkillTurn)
        {
            costText.GetComponent<Text>().text = skills[skillAct - 1].cost.ToString() + " SP";
            expoText.GetComponent<Text>().text = skills[skillAct - 1].expo;

            if (Input.GetKeyDown(KeyCode.W) && skillAct != 1 && skillAct != 4)
            {
                //���𓮂���
                skillAct--;
                actSelect.transform.Translate(-0.8f, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.S) && skillAct != 3 && skillAct != 6 && skillAct < skillActMax)
            {
                //���𓮂���
                skillAct++;
                actSelect.transform.Translate(0.8f, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.A) && skillAct != 1 && skillAct != 2 && skillAct != 3)
            {
                //���𓮂���
                skillAct -= 3;
                actSelect.transform.Translate(0, -5.25f, 0);
            }
            if (Input.GetKeyDown(KeyCode.D) && skillAct != 4 && skillAct != 5 && skillAct != 6 && skillAct + 2 < skillActMax)
            {
                //���𓮂���
                skillAct += 3;
                actSelect.transform.Translate(0, 5.25f, 0);
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //���𓮂���
                ActActive();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine("SkillUse", skills[skillAct - 1]);
            }
        }

        //�A�C�e��
        if (isItemTurn)
        {
            costText.GetComponent<Text>().text = items[skillAct - 1].quantity.ToString() + " ��";
            expoText.GetComponent<Text>().text = items[skillAct - 1].expo;

            if (Input.GetKeyDown(KeyCode.W) && skillAct != 1 && skillAct != 4)
            {
                //���𓮂���
                skillAct--;
                actSelect.transform.Translate(-0.8f, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.S) && skillAct != 3 && skillAct != 6 && skillAct < itemActMax)
            {
                //���𓮂���
                skillAct++;
                actSelect.transform.Translate(0.8f, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.A) && skillAct != 1 && skillAct != 2 && skillAct != 3)
            {
                //���𓮂���
                skillAct -= 3;
                actSelect.transform.Translate(0, -5.25f, 0);
            }
            if (Input.GetKeyDown(KeyCode.D) && skillAct != 4 && skillAct != 5 && skillAct != 6 && skillAct + 2 < itemActMax)
            {
                //���𓮂���
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

        //���^�[����
        if (isQuestionTurn)
        {
            
            //�������ԓ���
            if (remainTime >= 0)
            {
                //�J�E���g�_�E��
                remainTime -= Time.deltaTime * timeRate;
                //�������ʂ܂�
                timeText.GetComponent<Text>().text = remainTime.ToString("f1");
                //�������ԃo�[���f
                timeBar.GetComponent<Slider>().value = remainTime;
            }
            else
            //���Ԃ�0�ɂȂ�����
            {
                messageText.GetComponent<Text>().text = "���Ԑ؂�...";
                isQuestionTurn = false;
                MessageActive();
                //�G�̃^�[��
                StartCoroutine("MonsterTurn");
            }
            //���N���b�N������
            if (Input.GetMouseButtonDown(0))
            {
                //�N���b�N�����ʒu���擾
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //�N���b�N�����ʒu�ɂ���I�u�W�F�N�g���擾
                RaycastHit2D hit2D = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

                //�I�u�W�F�N�g���擾���Ă�����
                if (hit2D)
                {
                    isQuestionTurn = false;
                    MessageActive();
                    playerChoice = hit2D.transform.gameObject;

                    //�������s������
                    if(playerChoice.tag == "true")
                    {
                        //�e�L�X�g�\��
                        messageText.GetComponent<Text>().text = "�����I" + p.name + "�̍U���I";
                        StartCoroutine("PlayerTurn");
                    }
                    else
                    {
                        //�e�L�X�g�\��
                        messageText.GetComponent<Text>().text = "�s����...";
                        //�G�̃^�[��
                        StartCoroutine("MonsterTurn");
                    }
                }
            }
        }
    }

    IEnumerator SkillUse(SkillModel skill)
    {
        //�X�L��
        isSkillTurn = false;
        MessageActive();

        if (0 > p.nowSp - skill.cost)
        {
            messageText.GetComponent<Text>().text = "SP������Ȃ��I";
            yield return new WaitForSeconds(battleSpeed);
            isActTurn = true;
            ActActive();
        }
        else
        {
            messageText.GetComponent<Text>().text = p.name + "��" + skill.name + "!\n" + skill.message;
            SkillSet(skill);
            p.nowSp -= skill.cost;
            //SP�o�[���f
            playerSpBar.GetComponent<Slider>().value = p.nowSp;
            nowSp.GetComponent<Text>().text = p.nowSp.ToString();

            yield return new WaitForSeconds(battleSpeed);
            
            isQuestionTurn = true;
            SetQuestion();
            QuestionActive();
        }
    }

    //�A�C�e��
    IEnumerator ItemUse(ItemModel item)
    {
        isItemTurn = false;
        MessageActive();
        messageText.GetComponent<Text>().text = p.name + "��" + item.name + "�g�����I";
        yield return new WaitForSeconds(battleSpeed);

        //�A�C�e���̌����炷
        items[skillAct - 1].quantity--;
        if(items[skillAct - 1].quantity == 0)
        {
            items.RemoveAt(skillAct - 1);
            for(int i = 0; i < items.Count; i++)
            {
                itemTextList[i].GetComponent<Text>().text = items[i].name;
            }
            itemActMax = items.Count;
            itemTextList[itemActMax].GetComponent<Text>().text = "";
        }

        ItemSet(item);
        yield return new WaitForSeconds(battleSpeed);

        isQuestionTurn = true;
        SetQuestion();
        QuestionActive();
    }

    
    IEnumerator PlayerTurn()
    {
        //2�b�҂�
        yield return new WaitForSeconds(battleSpeed);
        //�_���[�W�v�Z
        int damage = (int)Mathf.Floor(p.atk * playerAtkRate - m.def);

        //HP���f
        if (damage >= 1)
        {
            m.hp -= damage;
            messageText.GetComponent<Text>().text = m.name + "��" + damage + "�̃_���[�W��^����";
            //HP�o�[���f
            monsterHpBar.GetComponent<Slider>().value = m.hp;
            if (addDamage != 0)
            {
                yield return new WaitForSeconds(battleSpeed);
                m.hp -= addDamage;
                messageText.GetComponent<Text>().text = m.name + "�ɒǉ���" + addDamage + "�̃_���[�W��^����";
                //HP�o�[���f
                monsterHpBar.GetComponent<Slider>().value = m.hp;
            }
        }
        else
        {
            messageText.GetComponent<Text>().text = "�~�X�I�_���[�W��^�����Ȃ������I";
        }

        //�퓬�I������
        if (m.hp <= 0)
        {
            yield return new WaitForSeconds(battleSpeed);
            //�v���C���[�̏���
            StartCoroutine("EndBattle");
        }
        else
        {
            //�G�̃^�[��
            StartCoroutine("MonsterTurn");
        }

    }

    //�G�̃^�[��
    IEnumerator MonsterTurn()
    {
        //2�b�҂�
        yield return new WaitForSeconds(battleSpeed);
        messageText.GetComponent<Text>().text = m.name + "�̍U���I";
        //2�b�҂�
        yield return new WaitForSeconds(battleSpeed);
        //�_���[�W�v�Z
        int damage = (int)Mathf.Floor(m.atk - p.def * playerDefRate);
        if(damage >= 1)
        {
            //HP���f
            p.nowHp -= damage;
            messageText.GetComponent<Text>().text = damage + "�̃_���[�W���󂯂�";
            //HP�o�[���f
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
            messageText.GetComponent<Text>().text = "�~�X�I�_���[�W���󂯂Ȃ������I";
        }
        //�_���[�W�o�[�ύX
        playerHpBar.GetComponent<Slider>().value = p.nowHp;
        //2�b�҂�
        yield return new WaitForSeconds(battleSpeed);
        //�퓬�I������
        if (p.nowHp <= 0)
        {
            //�v���C���[�̕���
            StartCoroutine("EndBattle");
        }
        else
        {
            RateReset();
            //�s���^�[���ɂ���
            isActTurn = true;
            //�^�[���ύX
            ActActive();
        }
    }

    //�퓬�I��
    IEnumerator EndBattle()
    {
        string sceneName;
        isQuestionTurn = false;
        if (m.hp <= 0)
        {
            messageText.GetComponent<Text>().text = "�G��|�����I";
            sceneName = "WorldMap";
        }
        else if(p.nowHp <= 0)
        {
            messageText.GetComponent<Text>().text = "�S�ł���...";
            sceneName = "";
        }
        else
        {
            messageText.GetComponent<Text>().text = "�����o�����I";
            sceneName = "WorldMap";
        }
        //2�b�҂�
        yield return new WaitForSeconds(battleSpeed);
        SceneManager.LoadScene(sceneName);
    }

    
    //���Z�b�g
    void SetQuestion()
    {
        choiceWindow1.tag = "false";
        choiceWindow2.tag = "false";
        choiceWindow3.tag = "false";
        choiceWindow4.tag = "false";
        int q = UnityEngine.Random.Range(0, questionList.Count);//���ԍ�
        string[] choiceList = { questionList[q].choice1, questionList[q].choice2, questionList[q].choice3, questionList[q].choice4 };
        int[] a = { 0, 1, 2, 3 };

        //�I���������_��
        int[] order = GetRandomElements(a, a.Length);

        //�����Z�b�g
        for (int i = 0; i <= 3; i++)
        {
            if(order[i] == 0)
            {
                switch (i)
                {
                    case 0:
                        choiceWindow1.tag = "true";
                        break;
                    case 1:
                        choiceWindow2.tag = "true";
                        break;
                    case 2:
                        choiceWindow3.tag = "true";
                        break;
                    case 3:
                        choiceWindow4.tag = "true";
                        break;
                }
                break;
            }
        }

        //��蕶�ƑI����
        questionText.GetComponent<Text>().text = questionList[q].question;
        choiceText1.GetComponent<Text>().text = choiceList[order[0]];
        choiceText2.GetComponent<Text>().text = choiceList[order[1]];
        choiceText3.GetComponent<Text>().text = choiceList[order[2]];
        choiceText4.GetComponent<Text>().text = choiceList[order[3]];
    }

    //�{�����Z�b�g
    void RateReset()
    {
        playerAtkRate = 1;
        playerDefRate = 1;
        timeRate = 1;
        addDamage = 0;
    }

    //���o��
    void QuestionActive()
    {
        //���o��
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

        //���b�Z�[�W�E�B���h�E������
        messageText.SetActive(false);
        messageWindow.SetActive(false);

        //�s������
        actSelect.SetActive(false);
        actWindow.SetActive(false);
        fightText.SetActive(false);
        skillText.SetActive(false);
        itemText.SetActive(false);
        escapeText.SetActive(false);
    }

    //�s���o��
    void ActActive()
    {
        isActTurn = true;
        isSkillTurn = false;
        isItemTurn = false;
        //�s���o��
        actSelect.SetActive(true);
        actWindow.SetActive(true);
        fightText.SetActive(true);
        skillText.SetActive(true);
        itemText.SetActive(true);
        escapeText.SetActive(true);
        actSelect.transform.position = new Vector3(-5.7f, -1.6f, 0);
        skillAct = 1;
        act = 1; 

        //�X�L������
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

        //���b�Z�[�W�E�B���h�E������
        messageText.SetActive(false);
        messageWindow.SetActive(false);
    }

    //�X�L���o��
    void SkillActive()
    {
        //�X�L���o��
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

        //�s������
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
        //�X�L���o��
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

        //�s������
        actWindow.SetActive(false);
        fightText.SetActive(false);
        skillText.SetActive(false);
        itemText.SetActive(false);
        escapeText.SetActive(false);

        isItemTurn = true;
        isActTurn = false;
    }

    //���b�Z�[�W�E�B���h�E���o��
    void MessageActive()
    {
        //���b�Z�[�W�E�B���h�E���o��
        messageText.SetActive(true);
        messageWindow.SetActive(true);

        //�X�L������
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

        //�s������
        actSelect.SetActive(false);
        actWindow.SetActive(false);
        fightText.SetActive(false);
        skillText.SetActive(false);
        itemText.SetActive(false);
        escapeText.SetActive(false);

        //�I�����Ɩ�������
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

    public void SkillSet(SkillModel skill)
    {
        switch (skill.id)
        {
            case "s01":
                timeRate = 0.5f;
                break;

            case "s02":
                playerDefRate = 3;
                break;

            case "s03":
                playerAtkRate = 1.5f;
                break;

            case "s04":
                addDamage = 10;
                break;

            case "s05":

                break;
            case "s06":

                break;
        }
    }

    public void ItemSet(ItemModel item)
    {
        switch (item.id)
        {
            case "i00001":
                Heal(30,item.type);
                break;

            case "i00002":
                Heal(90,item.type);
                break;

            case "i00003":
                Heal(200, item.type);
                break;

            case "i00004":
                Heal(20, item.type);
                break;

            case "i00005":
                Heal(60, item.type);
                break;

            case "i00006":
                Heal(100, item.type);
                break;
        }
    }

    //��
    public void Heal(int heal, string healType)
    {
        int healValue;
        if(healType == "heal_hp")
        {
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
            messageText.GetComponent<Text>().text = p.name + "��HP��" + healValue + "�񕜂����I";
        }
        else if(healType == "heal_sp")
        {
            if (p.nowSp + heal > p.maxSp)
            {
                healValue = p.maxSp - p.nowSp;
                p.nowSp = p.maxSp;
            }
            else
            {
                healValue = heal;
                p.nowSp += heal;
            }
            playerSpBar.GetComponent<Slider>().value = p.nowSp;
            nowSp.GetComponent<Text>().text = p.nowSp.ToString();
            messageText.GetComponent<Text>().text = p.name + "��SP��" + healValue + "�񕜂����I";
        }
    }

    //�I���������_��
    static int[] GetRandomElements(int[] array, int count)
    {
        // Fisher-Yates�V���b�t���A���S���Y�����g�p���Ĕz����V���b�t��
        System.Random random = new System.Random();
        int[] shuffledArray = array.OrderBy(x => random.Next()).ToArray();

        // �w�肳�ꂽ�������v�f���擾
        return shuffledArray.Take(count).ToArray();
    }
}
