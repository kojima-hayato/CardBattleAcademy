using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillModel : MonoBehaviour
{
    BattleManager bm;
    string skillName;
    string skillMessage;
    Skill[] skills = new Skill[99];

    // Start is called before the first frame update
    void Start()
    {

        bm = GetComponent<BattleManager>();   
        for(int i = 1; i < skills.Length; i++)
        {
            skills[i] = new Skill();
        }
        skills[1].name = "超集中";
        skills[1].cost = 2;
        skills[1].message = "問題の思考が早くなる！";
        skills[2].name = "三戦";
        skills[2].cost = 4;
        skills[2].message = "空手道に古くから伝わる守りの型！";
        skills[3].name = "分析";
        skills[3].cost = 4;
        skills[3].message = "敵の急所を見分けた！";
    }
    
    public void SkillUse(int skillAct, int playerSp)
    {

        switch (skillAct)
        {
            case 1:
                bm.timeRate = 0.5f;
                bm.costSp = 2;
                skillName = "超集中";
                skillMessage = "問題の思考が早くなる！";
                break;
            case 2:
                bm.playerDefRate = 3;
                bm.costSp = 4;
                skillName = "三戦";
                skillMessage = "空手道に古くから伝わる守りの型！";
                break;
            case 3:
                bm.playerAtkRate = 1.5f;
                bm.costSp = 4;
                skillName = "分析";
                skillMessage = "敵の急所を見分けた！";
                break; 
        }
        bm.messageText.GetComponent<Text>().text = "主人公の" + skillName + "!\n" + skillMessage;
    }

    
}

public class Skill
{
    public string name;
    public int cost;
    public string message;
}
