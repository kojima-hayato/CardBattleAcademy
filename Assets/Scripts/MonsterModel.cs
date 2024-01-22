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
                m.name = "�X���C��";
                m.hp = 5;
                m.atk = 4;
                m.def = 2;
                m.image = Resources.Load<Sprite>("monster01");
                
                break;
            case 2:
                m.name = "��ڋ�";
                m.hp = 20;
                m.atk = 7;
                m.def = 4;
                m.image = Resources.Load<Sprite>("monster02");
                break;
            case 3:
                m.name = "���_";
                m.hp = 100;
                m.atk = 30;
                m.def = 15;
                m.image = Resources.Load<Sprite>("monster04");
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
    public int exp;
    public int hp;
    public int atk;
    public int def;
    public Sprite image;
    public List<int> skillID = new List<int>();
}