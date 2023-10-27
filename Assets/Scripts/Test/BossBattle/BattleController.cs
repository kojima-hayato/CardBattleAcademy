using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public Slider HeroHP;
    public Slider BossHP;

    public TextMeshProUGUI maxHP;
    public TextMeshProUGUI nowHP;

    int maxHPValue, nowHPValue;

    // Start is called before the first frame update
    void Start()
    {
        //HPの最大値設定
        HeroHP.maxValue = 100;
        BossHP.maxValue = 100;

        //現在HPを最大値に合わせる
        HeroHP.value = HeroHP.maxValue;
        BossHP.value = BossHP.maxValue;

        //数字を合わせる
        maxHPValue = (int)HeroHP.maxValue;
        maxHP.text = maxHPValue.ToString();
        ChangeNowHP();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Attack();
    }

    private void Attack()
    {
        Debug.Log("交戦した");
        BossHP.value -= 50;
        HeroHP.value -= 20;

        ChangeNowHP();
    }

    private void ChangeNowHP()
    {
        //現在HP(数字)の更新
        nowHPValue = (int)HeroHP.value;
        nowHP.text = nowHPValue.ToString();
    }
}
