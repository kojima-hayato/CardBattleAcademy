using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlgorithmExecuter
{
    private List<Card> algoList = new();

    List<Card> hand = CardBuilder.hand;
    List<ActCard> actList = CardBuilder.actList;
    List<IfCard> ifList = CardBuilder.ifList;

    ActCard ac;
    IfCard ic;

    bool isEnterIf, isEnterRoop;
    int roopCount;

    int heroHPValue, bossHPValue;

    public void BuildAlgo()
    {
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

    }

    public IEnumerator ExecuteAlgo(Slider heroHP, Slider bossHP, TextMeshProUGUI nowHP, GameObject textFrame, Text textBox, float waitTime)
    {
        heroHPValue = (int)heroHP.value;
        bossHPValue = (int)bossHP.value;

        //アルゴリズムを実行する
        isEnterIf = false;
        isEnterRoop = false;

        roopCount = 0;

        foreach (Card c in algoList)
        {
            //初期化処理
            int value = c.GetValue();

            Debug.Log("c.GetCardType():" + c.GetCardType());
            switch (c.GetCardType())
            {
                case "act":
                    ac = actList.Find(x => x.GetCardId() == c.GetCardId());

                    Debug.Log(ac.GetActType());
                    if (ac != null)
                    {
                        switch (ac.GetActType())
                        {
                            case "attack":
                                Debug.Log("攻撃");
                                if (isEnterIf)
                                {
                                    if (IfCheck(ic))
                                    {
                                        value = (int)(value * ic.GetRate());

                                        textBox.text = "判定成功！効果" + ic.GetRate() + "倍";
                                        Debug.Log("一時停止実行");
                                        yield return new WaitForSeconds(waitTime);
                                        Debug.Log("再開");

                                        textBox.text = "";
                                        Debug.Log("一時停止実行");
                                        yield return new WaitForSeconds(0.1f);
                                        Debug.Log("再開");

                                        Attack(value, bossHP, textBox);
                                    }
                                    else
                                    {
                                        textBox.text = "判定失敗・・・";
                                        Debug.Log("一時停止実行");
                                        yield return new WaitForSeconds(waitTime);
                                        Debug.Log("再開");

                                        textBox.text = "";
                                        Debug.Log("一時停止実行");
                                        yield return new WaitForSeconds(0.1f);
                                        Debug.Log("再開");
                                    }
                                }
                                else if (isEnterRoop)
                                {
                                    textBox.text = roopCount + "連行動";
                                    Debug.Log("一時停止実行");
                                    yield return new WaitForSeconds(waitTime);
                                    Debug.Log("再開");

                                    textBox.text = "";
                                    Debug.Log("一時停止実行");
                                    yield return new WaitForSeconds(0.1f);
                                    Debug.Log("再開");

                                    for (int i = 0; i < roopCount; i++)
                                    {
                                        if(bossHP.value <= 0)
                                        {
                                            break;
                                        }

                                        Attack(value, bossHP, textBox);

                                        Debug.Log("一時停止実行");
                                        yield return new WaitForSeconds(waitTime);
                                        Debug.Log("再開");

                                        textBox.text = "";
                                        Debug.Log("一時停止実行");
                                        yield return new WaitForSeconds(0.1f);
                                        Debug.Log("再開");
                                    }

                                }
                                else
                                {
                                    Attack(value, bossHP, textBox);
                                }
                                break;

                            case "heal":
                                Debug.Log("回復");
                                if (isEnterIf)
                                {
                                    if (IfCheck(ic))
                                    {
                                        Debug.Log("判定成功");
                                        value = (int)(value * ic.GetRate());

                                        textBox.text = "判定成功！効果" + ic.GetRate() + "倍";
                                        Debug.Log("一時停止実行");
                                        yield return new WaitForSeconds(waitTime);
                                        Debug.Log("再開");

                                        textBox.text = "";
                                        Debug.Log("一時停止実行");
                                        yield return new WaitForSeconds(0.1f);
                                        Debug.Log("再開");

                                        Heal(value, heroHP, nowHP, textBox);
                                    }
                                    else
                                    {
                                        textBox.text = "判定失敗・・・";
                                        Debug.Log("一時停止実行");
                                        yield return new WaitForSeconds(waitTime);
                                        Debug.Log("再開");

                                        textBox.text = "";
                                        Debug.Log("一時停止実行");
                                        yield return new WaitForSeconds(0.1f);
                                        Debug.Log("再開");
                                    }
                                }
                                else if (isEnterRoop)
                                {
                                    textBox.text = roopCount + "連行動";
                                    Debug.Log("一時停止実行");
                                    yield return new WaitForSeconds(waitTime);
                                    Debug.Log("再開");

                                    textBox.text = "";
                                    Debug.Log("一時停止実行");
                                    yield return new WaitForSeconds(0.1f);
                                    Debug.Log("再開");

                                    for (int i = 0; i < roopCount; i++)
                                    {
                                        if(heroHP.value >= heroHP.maxValue)
                                        {
                                            textBox.text = "これ以上回復できない！";

                                            Debug.Log("一時停止実行");
                                            yield return new WaitForSeconds(waitTime);
                                            Debug.Log("再開");

                                            break;
                                        }

                                        Heal(value, heroHP, nowHP, textBox);

                                        Debug.Log("一時停止実行");
                                        yield return new WaitForSeconds(waitTime);
                                        Debug.Log("再開");

                                        textBox.text = "";
                                        Debug.Log("一時停止実行");
                                        yield return new WaitForSeconds(0.1f);
                                        Debug.Log("再開");
                                    }

                                }
                                else
                                {
                                    Heal(value, heroHP, nowHP, textBox);
                                }
                                break;
                        }

                        if (!isEnterRoop)
                        {
                            Debug.Log("一時停止実行");
                            yield return new WaitForSeconds(waitTime);
                            Debug.Log("再開");

                            textBox.text = "";
                            Debug.Log("一時停止実行");
                            yield return new WaitForSeconds(0.1f);
                            Debug.Log("再開");
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

        }
    }

    private bool IfCheck(IfCard i)
    {
        bool result = false;
        int targetValue = 0;

        //判定対象を格納
        switch (i.GetJudgeTarget())
        {
            case "hero_hp":
                targetValue = heroHPValue;
                break;

            case "boss_hp":
                targetValue = bossHPValue;
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

    private void Attack(int damageValue, Slider bossHP, Text textBox)
    {
        bossHP.value -= damageValue;
        bossHPValue = (int)bossHP.value;

        textBox.text = damageValue + "のダメージを与えた";
    }

    private void Heal(int healValue, Slider heroHP, TextMeshProUGUI nowHP, Text textBox)
    {
        heroHP.value += healValue;

        heroHPValue = (int)heroHP.value;

        //現在HP(数字)の更新
        int nowHPValue = (int)heroHP.value;
        nowHP.text = nowHPValue.ToString();

        textBox.text = healValue + "回復";
    }
}
