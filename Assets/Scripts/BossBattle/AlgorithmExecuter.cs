using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlgorithmBuilder
{
    FloatingDamageController fdc;

    private List<Card> algoList = new();

    List<Card> hand = CardBuilder.hand;
    List<ActCard> actList = CardBuilder.actList;
    List<IfCard> ifList = CardBuilder.ifList;

    ActCard ac;
    IfCard ic;

    bool isEnterIf, isEnterRoop;
    int roopCount;

    int heroHPvalue, bossHPvalue;

    public IEnumerator ExecuteAlgo(Slider heroHP, Slider bossHP, FloatingDamageController fdcParam)
    {
        //ダメージ表示クラス
        fdc = fdcParam;

        heroHPvalue = (int)heroHP.value;
        bossHPvalue = (int)bossHP.value;

        //初期化
        algoList.Clear();

        //GameObjectの配置順にCardを配置する
        List<GameObject> cardItemList = CardCarryer.cardItemList;
        foreach (GameObject g in cardItemList)
        {
            Debug.Log("cardItemList:" + g);
            Card c = hand.Find(x => x.GetCardItem() == g);
            if (c != null)
            {
                algoList.Add(c);
            }
            else
            {
                //仮置きのままなら専用のカード(何もしない)を生成
                if (g.CompareTag("Frame"))
                {
                    c = new(0, "none");
                    algoList.Add(c);
                }
            }
        }

        foreach (Card c in algoList)
        {
            Debug.Log("algoList:" + c.GetCardType());
        }

        //アルゴリズムを実行する

        isEnterIf = false;
        isEnterRoop = false;

        roopCount = 0;

        foreach (Card c in algoList)
        {
            //初期化処理
            int value = c.GetValue();

            switch (c.GetCardType())
            {
                case "act":
                    ac = actList.Find(x => x.GetCardId() == c.GetCardId());
                    if (ac != null)
                    {

                        switch (ac.GetActType())
                        {
                            case "attack":
                                Debug.Log("攻撃");
                                if (isEnterIf)
                                {
                                    if(ifCheck(ic))
                                    {
                                        Debug.Log("判定成功");
                                        value = (int)(value * ic.GetRate());

                                        Debug.Log(value + "のダメージ");
                                        Attack(value, bossHP, fdc);
                                    }
                                    else
                                    {
                                        Debug.Log("判定失敗");
                                    }
                                }
                                else if (isEnterRoop)
                                {
                                    Debug.Log((roopCount) + "連行動");
                                    for (int i = 0; i < roopCount; i++)
                                    {
                                        Debug.Log(value + "のダメージ");
                                        Attack(value, bossHP, fdc);
                                    }

                                }
                                else
                                {
                                    Debug.Log(value + "のダメージ");
                                    Attack(value, bossHP, fdc);
                                }
                                break;

                            case "heal":
                                Debug.Log("回復");
                                if (isEnterIf)
                                {
                                    if (ifCheck(ic))
                                    {
                                        Debug.Log("判定成功");
                                        value = (int)(value * ic.GetRate());

                                        Debug.Log(value + "のHPを回復");
                                        heroHP.value += value;
                                    }
                                    else
                                    {
                                        Debug.Log("判定失敗");
                                    }
                                }
                                else if (isEnterRoop)
                                {
                                    Debug.Log((roopCount) + "連行動");
                                    for (int i = 0; i < roopCount; i++)
                                    {
                                        Debug.Log(value + "のHPを回復");
                                        heroHP.value += value;
                                    }

                                }
                                else
                                {
                                    Debug.Log(value + "のHPを回復");
                                    heroHP.value += value;
                                }
                                break;
                        }
                    }

                    //初期化
                    isEnterIf = false;
                    isEnterRoop = false;
                    ic = null;

                    break;

                case "if":
                    isEnterIf = true;

                    ic = ifList.Find(x => x.GetCardId() == c.GetCardId());
                    if (ic == null)
                    {
                        Debug.LogError("ifCard取得失敗");
                    }
                    break;

                case "roop":
                    isEnterRoop = true;
                    roopCount = value;
                    break;

                case "none":
                    isEnterIf = false;
                    isEnterRoop = false;
                    break;
            }
            
            yield return new WaitForSeconds(1.0f);
        }
    }

    private bool ifCheck(IfCard i)
    {
        bool result = false;
        int targetValue = 0;

        //判定対象を格納
        switch (i.GetJudgeTarget())
        {
            case "hero_hp":
                targetValue = heroHPvalue;
                break;

            case "boss_hp":
                targetValue = bossHPvalue;
                break;
        }

        Debug.Log("target:" + i.GetJudgeTarget());
        Debug.Log("targetValue:" + targetValue);
        Debug.Log("pattern:" + i.GetJudgePattern());
        Debug.Log("checkValue:" + i.GetValue());


        //判定実行
        switch (i.GetJudgePattern())
        {
            case "==":
                if(targetValue == i.GetValue())
                {
                    result = true;
                }
                break;

            case "<=":
                if (targetValue <= i.GetValue())
                {
                    result = true;
                }
                break;

            case ">=":
                if (targetValue >= i.GetValue())
                {
                    result = true;
                }
                break;

            case "<":
                if (targetValue < i.GetValue())
                {
                    result = true;
                }
                break;

            case ">":
                if (targetValue > i.GetValue())
                {
                    result = true;
                }
                break;
        }
        return result;
    }

    private void Attack(int damageValue, Slider bossHP, FloatingDamageController fdc)
    {
        bossHP.value -= damageValue;

        fdc.ShowDamage(damageValue);
    }
}
