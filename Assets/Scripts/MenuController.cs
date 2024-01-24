﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MySql.Data.MySqlClient;
using System;
using System.Data;

public class MenuController : MonoBehaviour
{
    public GameObject back;
    public GameObject back2;
    public GameObject item;
    public GameObject deck;
    public GameObject status;
    public GameObject save;
    public GameObject end;
    public GameObject tool;
    public GameObject player;
    public GameObject skill;
    public GameObject important;
    public GameObject edit;
    public GameObject list;
    public GameObject yes;
    public GameObject no;
    public GameObject detail;

    public GameObject movePlayer;

    private bool isMenu;
    private bool isRow;
    private bool isCol;

    private int row;
    private int col;

    private List<GameObject> rowList = new List<GameObject>();
    private List<List<GameObject>> colList = new List<List<GameObject>>();
    private List<GameObject> itemColList = new List<GameObject>();
    private List<GameObject> deckColList = new List<GameObject>();
    private List<GameObject> statusColList = new List<GameObject>();
    private List<GameObject> choiceColList = new List<GameObject>();

    string toolText;
    string importantText;
    string deckText;
    string listText;
    string playerText;
    string skillText;

    string sql;

    DataBaseConnector dbc;
    DataTable dt;

    void Start()
    {
        dbc = new();
        dt = new();

        //アイテム
        sql = "SELECT" +
            " ii.item_id," +
            " item_name," +
            " quantity" +
            " FROM" +
            " data_item AS di," +
            " inventory_item AS ii" +
            " WHERE di.item_id = ii.item_id" +
            " ;";
        dbc.SetCommand();
        dt = dbc.ExecuteSQL(sql);
        foreach (DataRow row in dt.Rows)
        {
            if ((int)row["quantity"] == 0)
            {
                continue;
            }
            string s = (string)row["item_id"];
            if (s.StartsWith("t"))
            {
                importantText += "・" + row["item_name"] + "    ×" + row["quantity"] + "\n";
            }
            else
            {
                toolText += "・" + row["item_name"] + "    ×" + row["quantity"] + "\n";
            }
        }

        //デッキ
        sql = "SELECT" +
            " card_type," +
            " quantity" +
            " FROM" +
            " battle_card_deck AS bcd," +
            " data_card AS dc" +
            " WHERE bcd.card_id = dc.card_id" +
            " ;";
        dt = dbc.ExecuteSQL(sql);
        foreach (DataRow row in dt.Rows)
        {
            deckText += "・" + row["card_type"] + "    ×" + row["quantity"] + "\n";
        }

        //リスト
        sql = "SELECT" +
            " card_type," +
            " quantity" +
            " FROM" +
            " inventory_card AS ic," +
            " data_card AS dc" +
            " WHERE ic.card_id = dc.card_id" +
            " ;";
        dt = dbc.ExecuteSQL(sql);
        foreach (DataRow row in dt.Rows)
        {
            if ((int)row["quantity"] == 0)
            {
                continue;
            }
            listText += "・" + row["card_type"] + "    ×" + row["quantity"] + "\n";
        }

        //プレイヤー
        sql = "SELECT" +
            " *" +
            " FROM" +
            " data_hero_status AS dhs" +
            " ;";
        dt = dbc.ExecuteSQL(sql);
        foreach (DataRow row in dt.Rows)
        {
            playerText += "名前：" + row["hero_name"] + "\n" +
                      "レベル：" + row["hero_level"] + "\n" +
                      "HP：" + row["hero_hp"] + "/" + row["hero_max_hp"] + "\n" +
                      "SP：" + row["hero_sp"] + "/" + row["hero_max_sp"] + "\n" +
                      "攻撃力：" + row["hero_attack"] + "\n" +
                      "防御力：" + row["hero_defense"];
        }

        //スキル
        sql = "SELECT" +
            " skill_name," +
            " cost," +
            " value," +
            " expo" +
            " FROM" +
            " data_hero_status AS dhs," +
            " data_skill AS ds" +
            " WHERE dhs.hero_level >= ds.hero_level" +
            " ;";
        dt = dbc.ExecuteSQL(sql);
        foreach (DataRow row in dt.Rows)
        {
            skillText += "・" + row["skill_name"] + "\n";
        }

        isMenu = false;

        rowList.Add(item);
        rowList.Add(deck);
        rowList.Add(status);
        rowList.Add(save);
        rowList.Add(end);

        itemColList.Add(tool);
        itemColList.Add(important);

        deckColList.Add(edit);
        deckColList.Add(list);

        statusColList.Add(player);
        statusColList.Add(skill);

        choiceColList.Add(yes);
        choiceColList.Add(no);

        colList.Add(itemColList);
        colList.Add(deckColList);
        colList.Add(statusColList);
        colList.Add(choiceColList);
        colList.Add(choiceColList);
    }

    void Update()
    {
        //メニューを開く
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenu == false)
            {
                isMenu = true;
                isRow = true;
                rowList[0].transform.localScale = new Vector3(1.4f, 0.7f, 0);
                detail.GetComponent<Text>().text = toolText;
                movePlayer.SetActive(false);
                MenuActive();
            }
            else if (isMenu == true && isRow == true)
            {
                isMenu = false;
                isRow = false;
                rowList[row].transform.localScale = new Vector3(1.0f, 0.5f, 0);
                row = 0;
                movePlayer.SetActive(true);
                MenuActive();
            }
        }

        //縦
        if (isRow)
        {
            if (Input.GetKeyDown(KeyCode.W) && row > 0)
            {
                row--;
                ChangeRowImage(row + 1, row);
                ChangeColList(row + 1, row);
                ChangeDetail();
            }

            if (Input.GetKeyDown(KeyCode.S) && row < 4)
            {
                row++;
                ChangeRowImage(row - 1, row);
                ChangeColList(row - 1, row);
                ChangeDetail();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                colList[row][0].transform.localScale = new Vector3(1.4f, 0.7f, 0);
                isRow = false;
                isCol = true;
            }
        }

        //横
        if (isCol)
        {
            //Aまたは←を押すと左の要素に移る
            if (Input.GetKeyDown(KeyCode.A) && col > 0)
            {
                col--;
                ChangeColImage(col + 1, col);
                ChangeDetail();
            }

            //Dまたは→を押すと右の要素に移る
            if (Input.GetKeyDown(KeyCode.D) && col < colList[row].Count - 1)
            {
                col++;
                ChangeColImage(col - 1, col);
                ChangeDetail();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {

            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isRow = true;
                isCol = false;
                colList[row][col].transform.localScale = new Vector3(1.0f, 0.5f, 0);
                col = 0;
            }
        }
    }

    void ChangeColList(int before, int after)
    {
        if (colList[before] != null)
        {
            foreach (GameObject g in colList[before])
            {
                g.SetActive(false);
            }
        }

        if (colList[after] != null)
        {
            foreach (GameObject g in colList[after])
            {
                g.SetActive(true);
            }
        }
    }

    void ChangeColImage(int before, int after)
    {
        //画像を差し替える
        colList[row][after].transform.localScale = new Vector3(1.4f, 0.7f, 0);
        colList[row][before].transform.localScale = new Vector3(1.0f, 0.5f, 0);
    }

    void ChangeRowImage(int before, int after)
    {
        //画像を差し替える
        rowList[after].transform.localScale = new Vector3(1.4f, 0.7f, 0);
        rowList[before].transform.localScale = new Vector3(1.0f, 0.5f, 0);
    }

    void MenuActive()
    {
        back.SetActive(isMenu);
        back2.SetActive(isMenu);
        item.SetActive(isMenu);
        deck.SetActive(isMenu);
        status.SetActive(isMenu);
        save.SetActive(isMenu);
        end.SetActive(isMenu);
        tool.SetActive(isMenu);
        important.SetActive(isMenu);
        detail.SetActive(isMenu);

        if (isMenu == false)
        {
            player.SetActive(isMenu);
            skill.SetActive(isMenu);
            list.SetActive(isMenu);
            edit.SetActive(isMenu);
            yes.SetActive(isMenu);
            no.SetActive(isMenu);
        }
    }

    void ChangeDetail()
    {
        switch (row)
        {
            case 0:
                if (col == 0)
                {
                    detail.GetComponent<Text>().text = toolText;
                }
                else
                {
                    detail.GetComponent<Text>().text = importantText;
                }
                break;
            case 1:
                if (col == 0)
                {
                    detail.GetComponent<Text>().text = deckText;
                }
                else
                {
                    detail.GetComponent<Text>().text = listText;
                }
                break;
            case 2:
                if (col == 0)
                {
                    detail.GetComponent<Text>().text = playerText;
                }
                else
                {
                    detail.GetComponent<Text>().text = skillText;
                }
                break;
            case 3:
                detail.GetComponent<Text>().text = "セーブしますか？";
                break;
            case 4:
                detail.GetComponent<Text>().text = "ゲームを終了しますか？";
                break;
        }
    }
}