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

    private int row;
    private int col;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Escを押すとマップに戻る
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenu)
            {
                isMenu = false;
            }
            else
            {
                isMenu = true;
            }
            MenuActive();
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
            back.SetActive(false);
            back2.SetActive(false);
            item.SetActive(false);
            deck.SetActive(false);
            status.SetActive(false);
            save.SetActive(false);
            end.SetActive(false);
            tool.SetActive(false);
            equipment.SetActive(false);
            important.SetActive(false);
        }
        else
        {
            back.SetActive(true);
            back2.SetActive(true);
            item.SetActive(true);
            deck.SetActive(true);
            status.SetActive(true);
            save.SetActive(true);
            end.SetActive(true);
            tool.SetActive(true);
            equipment.SetActive(true);
            important.SetActive(true);
        }
    }
}
