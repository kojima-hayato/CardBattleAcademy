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
        player.name = "‚ ‚ ‚ ‚ ";
        player.lv = 1;
        player.maxHp = 15;
        player.nowHp = 12;
        player.maxSp = 10;
        player.nowSp = 10;
        player.atk = 5;
        player.def = 2;
        player.skillID.Add(1);
        player.skillID.Add(2);
        player.skillID.Add(3);
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
}