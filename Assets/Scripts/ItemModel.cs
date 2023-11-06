using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemModel : MonoBehaviour
{
    BattleManager bm;
    Item[] items = new Item[7];
    // Start is called before the first frame update
    void Start()
    {
        bm = GetComponent<BattleManager>();
    }

    public void Set()
    {
        for (int i = 1; i < items.Length; i++)
        {
            items[i] = new Item();
        }
        items[1].id = 1;
        items[1].name = "A";
        items[1].message = "HPが30回復した！";
        items[2].id = 2;
        items[2].name = "B";
        items[2].message = "HPが90回復した！";
        items[3].id = 3;
        items[3].name = "アロナイン";
        items[3].message = "HPが200回復した！";
        items[4].id = 4;
        items[4].name = "プロテイン";
        items[4].message = "攻撃力が1.5倍になった！";
        items[5].id = 5;
        items[5].name = "E";
        items[5].message = "防御力が1.5倍になった！";
        items[6].id = 6;
        items[6].name = "F";
        items[6].message = "状態異常がすべて回復した！";

    }

    public Item ItemSet(int haveItem, int itemId)
    {
        if(haveItem != 0)
        {
            return items[itemId];
        }
        else
        {
            return null;
        }
        
    }

    public void ItemUse(int itemId)
    {
        switch (itemId)
        {
            case 1:
                bm.Heal(30);
                break;

            case 2:
                bm.Heal(90);
                break;

            case 3:
                bm.Heal(200);
                break;

            case 4:

                break;
            case 5:

                break;
            case 6:

                break;
        }
    }
}
public class Item
{
    public int id;
    public string name;
    public int have;
    public string message;
}