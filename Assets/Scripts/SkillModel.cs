using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillModel : MonoBehaviour
{
    BattleManager bm;
    Skill[] skills = new Skill[7];

    // Start is called before the first frame update
    void Start()
    {
        bm = GetComponent<BattleManager>();   
    }
    
    public void SkillUse(int skillAct)
    {
        switch (skillAct)
        {
            case 1:
                bm.timeRate = 0.5f;
                break;

            case 2:
                bm.playerDefRate = 3;
                break;

            case 3:
                bm.playerAtkRate = 1.5f;
                break; 

            case 4:
                bm.addDamage = 10;
                break;

            case 5:

                break;
            case 6:

                break;
        }
    }

    public void Set()
    {
        for (int i = 1; i < skills.Length; i++)
        {
            skills[i] = new Skill();
        }
        skills[1].id = 1;
        skills[1].name = "’´W’†";
        skills[1].cost = 2;
        skills[1].message = "–â‘è‚ÌŽvl‚ª‘‚­‚È‚éI";
        skills[1].expo = "§ŒÀŽžŠÔ‚ð2”{‚É‚·‚é";
        skills[2].id = 2;
        skills[2].name = "ŽOí";
        skills[2].cost = 4;
        skills[2].message = "‹óŽè“¹‚ÉŒÃ‚­‚©‚ç“`‚í‚éŽç‚è‚ÌŒ^I";
        skills[2].expo = "–hŒä—Í‚ð1ƒ^[ƒ“3”{‚É‚·‚é";
        skills[3].id = 3;
        skills[3].name = "•ªÍ";
        skills[3].cost = 4;
        skills[3].message = "“G‚Ì‹}Š‚ðŒ©•ª‚¯‚½I";
        skills[3].expo = "ŽŸ‚ÌUŒ‚—Í‚ª1.5”{‚É‚·‚é";
        skills[4].id = 4;
        skills[4].name = "’Ç”ö’e";
        skills[4].cost = 6;
        skills[4].message = "“G‚É10‚Ì’Ç‰ÁŒÅ’èƒ_ƒ[ƒWI";
        skills[4].expo = "ŽŸ‚ÌUŒ‚‚ÌŒã’Ç‰Á‚Å10‚ÌŒÅ’èƒ_ƒ[ƒW";


    }

    public Skill SkillSet(int skillID)
    {
        return skills[skillID];
    }

}

public class Skill
{
    public int id;
    public string name;
    public int cost;
    public string message;
    public string expo;
}
