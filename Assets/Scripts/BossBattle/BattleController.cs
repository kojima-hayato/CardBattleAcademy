using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public Slider heroHP;
    public Slider bossHP;

    public TextMeshProUGUI maxHP;
    public TextMeshProUGUI nowHP;

    int maxHPValue, nowHPValue;

    CardBuilder cb;
    FloatingDamageController fdc;

    AlgorithmBuilder ab = new();
    // Start is called before the first frame update
    void Start()
    {
        //HPの最大値設定
        heroHP.maxValue = 100;
        bossHP.maxValue = 100;

        //現在HPを最大値に合わせる
        heroHP.value = heroHP.maxValue;
        bossHP.value = bossHP.maxValue;

        //数字を合わせる
        maxHPValue = (int)heroHP.maxValue;
        maxHP.text = maxHPValue.ToString();
        ChangeNowHP();

        cb = FindObjectOfType<CardBuilder>();
        fdc = FindObjectOfType<FloatingDamageController>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick()
    {
        StartAttack();
    }

    private void StartAttack()
    {
        Debug.Log("勇者の行動");
        ab.ExecuteAlgo(heroHP, bossHP, fdc);

        if (bossHP.value <= 0)
        {
            Debug.Log("倒した");
            //ここに遷移処理(マップ)
        }
        else
        {
            Debug.Log("敵の行動");
            heroHP.value -= 10;

            if (heroHP.value <= 0)
            {
                Debug.Log("やられた");
                //ここに遷移処理(ゲームオーバー)
            }
            else
            {
                Debug.Log("次のターン");
                ChangeNowHP();

                cb.ReturnDeck();
                cb.DrawCard(0, 0, 3);
            }
        }
    }

    private void ChangeNowHP()
    {
        //現在HP(数字)の更新
        nowHPValue = (int)heroHP.value;
        nowHP.text = nowHPValue.ToString();
    }
}
