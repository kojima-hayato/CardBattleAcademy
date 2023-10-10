using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemModel : MonoBehaviour
{
    BattleManager bm;
    // Start is called before the first frame update
    void Start()
    {
        bm = GetComponent<BattleManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ItemUse(int skillAct)
    {
        switch (skillAct)
        {
            //case 1:
            //    bm.playerHp += 30;
            //    bm.playerHpBar.GetComponent<Slider>().value = bm.playerHp;
            //    if (bm.playerHp > bm.playerMaxHp)
            //    {
            //        bm.playerHp = bm.playerMaxHp;
            //    }
            //    bm.messageText.GetComponent<Text>().text = "ålŒö‚Í–ò‘‚ğg‚Á‚½!";
            //    break;
        }
    }
}