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

    private bool isMenu;
    private bool isRow;
    private bool isCol;

    private int row;
    private int col;

    private List<GameObject> rowList = new List<GameObject>();
    private List<GameObject> colList = new List<GameObject>();

    void Start()
    {
        isMenu = false;
        rowList.Add(item);
        rowList.Add(deck);
        rowList.Add(status);
        rowList.Add(save);
        rowList.Add(end);
    }

    void  Update()
    {
        //メニューを開く
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenu)
            {
                isMenu = false;
                isRow = false;
                MenuActive(isMenu);
            }
            else
            {
                isMenu = true;
                isRow = true;
                MenuActive(isMenu);
                //画像を差し替える
                rowList[0].transform.localScale = new Vector3(1.2f, 0.7f, 0);
            }
        }

        //Aまたは←を押すと左の要素に移る
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            col--;
        }

        //Dまたは→を押すと右の要素に移る
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            col++;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            row--;
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            row++;
        }
    }

    void MenuActive()
    {
        if (isMenu)
        {
            //大項目操作
            if (isRow)
            {
                //上に移動
                if (Input.GetKeyDown(KeyCode.W) && row > 0)
                {
                    Debug.Log("b");
                    //画像を差し替える
                    rowList[row].transform.localScale = new Vector3(1, 0.5f, 0);
                    row--;
                    rowList[row].transform.localScale = new Vector3(1.2f, 0.7f, 0);
                }
                //下に移動
                if (Input.GetKeyDown(KeyCode.S) && row < 4)
                {
                    rowList[row].transform.localScale = new Vector3(1, 0.5f, 0);
                    row++;
                    rowList[row].transform.localScale = new Vector3(1.2f, 0.7f, 0);
                }
                //列選択へ
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    isRow = false;
                    isCol = true;
                }
            }

            //小項目操作
            if (isCol)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    col--;

                }
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    col++;
                }
            }
            
        }
        
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
