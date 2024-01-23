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
    public GameObject player;
    public GameObject important;
    public GameObject edit;
    public GameObject list;
    public GameObject yes;
    public GameObject no;
    public GameObject detail;

    public GameObject movePlayer;
    public GameObject encount;

    private bool isMenu;
    private bool isRow;
    private bool isCol;

    private int row;
    private int col;

    private List<GameObject> rowList = new List<GameObject>();
    private List<List<GameObject>> colList = new List<List<GameObject>>();
    private List<GameObject> itemColList = new List<GameObject>(); //����
    private List<GameObject> deckColList = new List<GameObject>(); //�f�b�L
    private List<GameObject> statusColList = new List<GameObject>(); //�X�e�[�^�X
    private List<GameObject> choiceColList = new List<GameObject>(); //�Z�[�u�A�Q�[���I��

    string toolText;
    string importantText;
    string deckText;
    string listText;
    string playerText;

    string toolSql;
    string importantSql;
    string deckSql;
    string listSql;
    string playerSql;

    DataBaseConnector dbc;
    DataTable dt;

    void Start()
    {
        dbc = new();
        dt = new();

        //�A�C�e��
        toolSql = "SELECT" +
            " ii.item_id," +
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
            string s = (string)row["item_id"];
            if (s.StartsWith("t"))
            {
                importantText += "�E" + row["item_name"] + "    �~" + row["quantity"] + "\n";
            }
            else
            {
                toolText += "�E" + row["item_name"] + "    �~" + row["quantity"] + "\n";
            }
        }

        //�f�b�L
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
            deckText += "�E" + row["card_type"] + "    �~" + row["quantity"] + "\n";
        }

        //���X�g
        listSql = "SELECT" +
            " card_type," +
            " quantity" +
            " FROM" +
            " inventory_card AS ic," +
            " data_card AS dc" +
            " WHERE ic.card_id = dc.card_id" +
            " ;";
        dt = dbc.ExecuteSQL(listSql);
        foreach (DataRow row in dt.Rows)
        {
            if ((int)row["quantity"] == 0)
            {
                continue;
            }
            listText += "�E" + row["card_type"] + "    �~" + row["quantity"] + "\n";
        }

        //�v���C���[
        playerSql = "SELECT" +
            " *" +
            " FROM" +
            " data_hero_status AS dhs" +
            " ;";
        dt = dbc.ExecuteSQL(playerSql);
        foreach (DataRow row in dt.Rows)
        {
            playerText += "���O�F" + row["hero_name"] + "\n" +
                      "���x���F" + row["hero_level"] + "\n" +
                      "HP�F" + row["hero_hp"] + "/" + row["hero_max_hp"] + "\n" +
                      "SP�F" + row["hero_sp"] + "/" + row["hero_max_sp"] + "\n" +
                      "�U���́F" + row["hero_attack"] + "\n" +
                      "�h��́F" + row["hero_defense"];
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

        choiceColList.Add(yes);
        choiceColList.Add(no);

        colList.Add(itemColList);
        colList.Add(deckColList);
        colList.Add(statusColList);
        colList.Add(choiceColList);
        colList.Add(choiceColList);
    }

    void  Update()
    {
        //���j���[���J��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isMenu == false)
            {
                //�J��
                isMenu = true;
                isRow = true;
                //�摜�������ւ���
                rowList[0].transform.localScale = new Vector3(1.4f, 0.7f, 0);
                detail.GetComponent<Text>().text = toolText;
                movePlayer.SetActive(false);
                MenuActive();
            }
            else if(isMenu == true && isRow == true)
            {
                //����
                isMenu = false;
                isRow = false;
                rowList[row].transform.localScale = new Vector3(1.0f, 0.5f, 0);
                row = 0;
                movePlayer.SetActive(true);
                MenuActive();
            }
        }

        //�c
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

        //��
        if (isCol)
        {
            //A�܂��́��������ƍ��̗v�f�Ɉڂ�
            if (Input.GetKeyDown(KeyCode.A) && col > 0)
            {
                col--;
                ChangeColImage(col + 1, col);
                ChangeDetail();
            }

            //D�܂��́��������ƉE�̗v�f�Ɉڂ�
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
        colList[row][after].transform.localScale = new Vector3(1.4f, 0.7f, 0);
        colList[row][before].transform.localScale = new Vector3(1.0f, 0.5f, 0);
    }

    void ChangeRowImage(int before,int after)
    {
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

        if(isMenu == false)
        {
            player.SetActive(isMenu);
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
                detail.GetComponent<Text>().text = playerText;
                break;
            case 3:
                detail.GetComponent<Text>().text = "�Z�[�u���܂����H";
                break;
            case 4:
                detail.GetComponent<Text>().text = "�Q�[�����I�����܂����H";
                break;
        }
    }
}
