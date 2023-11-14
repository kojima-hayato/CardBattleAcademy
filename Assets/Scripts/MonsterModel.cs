using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterModel : MonoBehaviour
{
    MonsterDB m= new MonsterDB();

    // Start is called before the first frame update
    void Start()
    {

    }

    public MonsterDB MonsterDB(int monsterID)
    {
        switch (monsterID)
        {
            case 1:
                m.name = "ÉXÉâÉCÉÄ";
                m.hp = 5;
                m.atk = 4;
                m.def = 2;
                m.image = Resources.Load<Sprite>("Monster01");
                
                break;
            case 2:
                m.name = "ÇΩÇøÇŒÇ»ÇŒÇøÇΩ";
                m.hp = 20;
                m.atk = 7;
                m.def = 4;
                m.image = Resources.Load<Sprite>("MonsterImages/monster02");
                break;
            case 3:
                m.name = "ñÇêl";
                m.hp = 100;
                m.atk = 30;
                m.def = 15;
                m.image = Resources.Load<Sprite>("MonsterImages/monster03");
                Debug.Log(m.image);
                break;
        }
        return m;
    }

    public void MonsterTurn()
    {

    }
}
public class MonsterDB
{
    public int id;
    public string name;
    public int hp;
    public int atk;
    public int def;
    public Sprite image;
    public List<int> skillID = new List<int>();
}