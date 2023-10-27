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
        items[1].name = "A";
        items[1].message = "HPが30回復した！";
        items[2].name = "B";
        items[2].message = "HPが90回復した！";
        items[3].name = "アロナイン";
        items[3].message = "HPが200回復した！";
        items[4].name = "プロテイン";
        items[4].message = "攻撃力が1.5倍になった！";
        items[5].name = "E";
        items[5].message = "防御力が1.5倍になった！";
        items[6].name = "F";
        items[6].message = "状態異常がすべて回復した！";

    }

        public void ItemUse(int skillAct)
    {
        switch (skillAct)
        {
            case 0:

                break;

            case 1:

                break;

            case 2:

                break;

            case 3:

                break;

            case 4:

                break;
            case 5:

                break;
        }
    }
}
public class Item
{
    public string name;
    public string message;
}