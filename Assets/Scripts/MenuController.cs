using System.Collections;
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
    public GameObject equipment;
    public GameObject important;
    public GameObject edit;
    public GameObject list;
    public GameObject yes;
    public GameObject no;
    public GameObject detail;

    private bool isMenu;
    private bool isRow;
    private bool isCol;

    private int row;
    private int col;

    private List<GameObject> rowList = new List<GameObject>();
    private List<List<GameObject>> colList = new List<List<GameObject>>();
    private List<GameObject> itemColList = new List<GameObject>(); //道具
    private List<GameObject> deckColList = new List<GameObject>(); //デッキ
    private List<GameObject> choiceColList = new List<GameObject>(); //セーブ、ゲーム終了

    string toolText;
    string deckText;
    string playerText;

    string toolSql;
    string deckSql;
    string playerSql;

    DataBaseConnector dbc;
    DataTable dt;

    void Start()
    {
        dbc = new();
        dt = new();

        //アイテム
        toolSql = "SELECT" +
            " item_name," +
            " quantity" +
            " FROM" +
            " data_item AS di," +
            " inventory_item AS ii" +
            " WHERE di.item_id = ii.item_id" +
            " ;";
        dbc.SetCommand();
        dt = dbc.ExecuteSQL(toolSql);
        foreach (DataRow row in dt.Rows)
        {
            if((int)row["quantity"] == 0)
            {
                continue;
            }
            toolText += "・" + row["item_name"] + "    ×" + row["quantity"] + "\n";
        }
        detail.GetComponent<Text>().text = toolText;

        //デッキ
        deckSql = "SELECT" +
            " card_type," +
            " quantity" +
            " FROM" +
            " battle_card_deck AS bcd," +
            " data_card AS dc" +
            " WHERE bcd.card_id = dc.card_id" +
            " ;";
        dt = dbc.ExecuteSQL(deckSql);
        foreach (DataRow row in dt.Rows)
        {
            deckText += "・" + row["card_type"] + "    ×" + row["quantity"] + "\n";
        }

        //プレイヤー
        playerSql = "SELECT" +
            " *" +
            " FROM" +
            " data_hero_status AS dhs" +
            " ;";
        dt = dbc.ExecuteSQL(playerSql);
        foreach (DataRow row in dt.Rows)
        {
            playerText += "名前：" + row["hero_name"] + "\n" +
                      "レベル：" + row["hero_level"] + "\n" +
                      "HP：" + row["hero_hp"] + "\n" +
                      "攻撃力：" + row["hero_attack"];
        }
        

        isMenu = false;
        rowList.Add(item);
        rowList.Add(deck);
        rowList.Add(status);
        rowList.Add(save);
        rowList.Add(end);

        itemColList.Add(tool);
        itemColList.Add(equipment);
        itemColList.Add(important);

        deckColList.Add(edit);
        deckColList.Add(list);

        choiceColList.Add(yes);
        choiceColList.Add(no);

        colList.Add(itemColList);
        colList.Add(deckColList);
        colList.Add(null);
        colList.Add(choiceColList);
        colList.Add(choiceColList);
    }

    void  Update()
    {
        //メニューを開く
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isMenu == false)
            {
                //開く
                isMenu = true;
                isRow = true;
                //画像を差し替える
                rowList[0].transform.localScale = new Vector3(1.2f, 0.7f, 0);
            }
            else
            {
                //閉じる
                isMenu = false;
                isRow = false;
                rowList[row].transform.localScale = new Vector3(1.0f, 0.5f, 0);
                row = 0;
            }
            Invoke("MenuActive", 0.1f);
        }

        //縦
        if (isRow)
        {
            if (Input.GetKeyDown(KeyCode.W) && row > 0)
            {
                row--;
                ChangeRowImage(row + 1, row);
                ChangeColList(row + 1, row);
                ChangeDetailRow();
            }

            if (Input.GetKeyDown(KeyCode.S) && row < 4)
            {
                row++;
                ChangeRowImage(row - 1, row);
                ChangeColList(row - 1, row);
                ChangeDetailRow();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                colList[row][0].transform.localScale = new Vector3(1.2f, 0.7f, 0);
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
            }

            //Dまたは→を押すと右の要素に移る
            if (Input.GetKeyDown(KeyCode.D) && col < colList[row].Count - 1)
            {
                col++;
                ChangeColImage(col - 1, col);
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
                Invoke("Interval", 0.1f);
            }
        }
    }

    void ChangeColList(int before, int after)
    {
        if(colList[before] != null)
        {
            foreach (GameObject g in colList[before])
            {
                g.SetActive(false);
            }
        }
        
        if(colList[after] != null)
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
        colList[row][after].transform.localScale = new Vector3(1.2f, 0.7f, 0);
        colList[row][before].transform.localScale = new Vector3(1.0f, 0.5f, 0);
    }

    void ChangeRowImage(int before,int after)
    {
        //画像を差し替える
        rowList[after].transform.localScale = new Vector3(1.2f, 0.7f, 0);
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
        equipment.SetActive(isMenu);
        important.SetActive(isMenu);
        detail.SetActive(isMenu);

        if(isMenu == false)
        {
            list.SetActive(isMenu);
            edit.SetActive(isMenu);
            yes.SetActive(isMenu);
            no.SetActive(isMenu);
        }
    }

    void ChangeDetailRow()
    {
        switch (row)
        {
            case 0:
                detail.GetComponent<Text>().text = toolText;
                break;
            case 1:
                detail.GetComponent<Text>().text = deckText;
                break;
            case 2:
                detail.GetComponent<Text>().text = playerText;
                break;
            case 3:
                detail.GetComponent<Text>().text = "セーブしますか？";
                break;
            case 4:
                detail.GetComponent<Text>().text = "ゲームを終了しますか？";
                break;
        }
        Debug.Log(detail.GetComponent<Text>().text);
        Debug.Log(playerText);
    }

    void Interval()
    {

    }
}
