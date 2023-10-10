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
                m.name = "ƒXƒ‰ƒCƒ€";
                m.hp = 5;
                m.atk = 4;
                m.def = 2;
                return m;

            case 2:
                m.name = "‚½‚¿‚Î‚È‚Î‚¿‚È";
                m.hp = 20;
                m.atk = 7;
                m.def = 4;
                return m;
        }
        return null;
    }
}
public class MonsterDB
{
    public int id;
    public string name;
    public int hp;
    public int atk;
    public int def;
    public List<int> skillID = new List<int>();
}