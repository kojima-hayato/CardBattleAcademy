using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private bool isMenu;
    private bool isRow;
    private bool isCol;

    private int row;
    private int col;

    private List<GameObject> rowList = new List<GameObject>();
    private List<List<GameObject>> colList = new List<List<GameObject>>();
    private List<GameObject> itemColList = new List<GameObject>();
    private List<GameObject> deckColList = new List<GameObject>();
    private List<GameObject> choiceColList = new List<GameObject>();

    void Start()
    {
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
        if (Input.GetKeyDown(KeyCode.Escape) && isMenu == false)
        {
            isMenu = true;
            isRow = true;
            MenuActive(isMenu);
            //画像を差し替える
            rowList[0].transform.localScale = new Vector3(1.2f, 0.7f, 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isMenu == true)
        {
            isMenu = false;
            isRow = false;
            MenuActive(isMenu);
        }

        if (isRow)
        {
            if (Input.GetKeyDown(KeyCode.W) && row > 0)
            {
                row--;
                ChangeRowImage(row + 1, row);
                ChangeColList(row + 1, row);
            }

            if (Input.GetKeyDown(KeyCode.S) && row < 4)
            {
                row++;
                ChangeRowImage(row - 1, row);
                ChangeColList(row - 1, row);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                colList[0][0].transform.localScale = new Vector3(1.2f, 0.7f, 0);
                isRow = false;
                isCol = true;
            }
        }

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

    void MenuActive(bool isMenu)
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
    }
}
