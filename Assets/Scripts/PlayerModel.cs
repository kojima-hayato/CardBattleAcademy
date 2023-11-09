using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public PlayerDB player = new PlayerDB();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public PlayerDB PlayerSet()
    {
        //ステータス
        player.name = "ああああ";
        player.lv = 10;
        player.maxHp = 50;
        player.nowHp = 42;
        player.maxSp = 30;
        player.nowSp = 30;
        player.atk = 25;
        player.def = 15;

        //覚えてるスキル
        player.skillID.Add(1);
        if(player.lv >= 3)
        {
            player.skillID.Add(2);
        }
        if(player.lv >= 6)
        {
            player.skillID.Add(3);
        }
        if(player.lv >= 9)
        {
            player.skillID.Add(4);
        }
        if(player.lv >= 12)
        {
            player.skillID.Add(5);
        }

        //アイテム所持数
        player.haveItem.Add(1);
        player.haveItem.Add(1);
        player.haveItem.Add(0);
        player.haveItem.Add(0);
        player.haveItem.Add(0);
        player.haveItem.Add(0);

        return player;
    }

}
public class PlayerDB
{
    public string name;
    public int lv;
    public int maxHp;
    public int nowHp;
    public int maxSp;
    public int nowSp;
    public int atk;
    public int def;
    public List<int> skillID = new List<int>();
    public List<int> haveItem = new List<int>();
}