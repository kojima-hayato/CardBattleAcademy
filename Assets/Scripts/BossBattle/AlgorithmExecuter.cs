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
        //������
        algoList.Clear();

        //GameObject�̔z�u����Card��z�u����
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
                //���u���̂܂܂Ȃ��p�̃J�[�h(�������Ȃ�)�𐶐�
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

        //�A���S���Y�������s����
        isEnterIf = false;
        isEnterRoop = false;

        roopCount = 0;

        foreach (Card c in algoList)
        {
            //����������
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
                                Debug.Log("�U��");
                                if (isEnterIf)
                                {
                                    if (IfCheck(ic))
                                    {
                                        value = (int)(value * ic.GetRate());

                                        textBox.text = "���萬���I����" + ic.GetRate() + "�{";
                                        Debug.Log("�ꎞ��~���s");
                                        yield return new WaitForSeconds(waitTime);
                                        Debug.Log("�ĊJ");

                                        textBox.text = "";
                                        Debug.Log("�ꎞ��~���s");
                                        yield return new WaitForSeconds(0.1f);
                                        Debug.Log("�ĊJ");

                                        Attack(value, bossHP, textBox);
                                    }
                                    else
                                    {
                                        textBox.text = "���莸�s�E�E�E";
                                        Debug.Log("�ꎞ��~���s");
                                        yield return new WaitForSeconds(waitTime);
                                        Debug.Log("�ĊJ");

                                        textBox.text = "";
                                        Debug.Log("�ꎞ��~���s");
                                        yield return new WaitForSeconds(0.1f);
                                        Debug.Log("�ĊJ");
                                    }
                                }
                                else if (isEnterRoop)
                                {
                                    textBox.text = roopCount + "�A�s��";
                                    Debug.Log("�ꎞ��~���s");
                                    yield return new WaitForSeconds(waitTime);
                                    Debug.Log("�ĊJ");

                                    textBox.text = "";
                                    Debug.Log("�ꎞ��~���s");
                                    yield return new WaitForSeconds(0.1f);
                                    Debug.Log("�ĊJ");

                                    for (int i = 0; i < roopCount; i++)
                                    {
                                        if(bossHP.value <= 0)
                                        {
                                            break;
                                        }

                                        Attack(value, bossHP, textBox);

                                        Debug.Log("�ꎞ��~���s");
                                        yield return new WaitForSeconds(waitTime);
                                        Debug.Log("�ĊJ");

                                        textBox.text = "";
                                        Debug.Log("�ꎞ��~���s");
                                        yield return new WaitForSeconds(0.1f);
                                        Debug.Log("�ĊJ");
                                    }

                                }
                                else
                                {
                                    Attack(value, bossHP, textBox);
                                }
                                break;

                            case "heal":
                                Debug.Log("��");
                                if (isEnterIf)
                                {
                                    if (IfCheck(ic))
                                    {
                                        Debug.Log("���萬��");
                                        value = (int)(value * ic.GetRate());

                                        textBox.text = "���萬���I����" + ic.GetRate() + "�{";
                                        Debug.Log("�ꎞ��~���s");
                                        yield return new WaitForSeconds(waitTime);
                                        Debug.Log("�ĊJ");

                                        textBox.text = "";
                                        Debug.Log("�ꎞ��~���s");
                                        yield return new WaitForSeconds(0.1f);
                                        Debug.Log("�ĊJ");

                                        Heal(value, heroHP, nowHP, textBox);
                                    }
                                    else
                                    {
                                        textBox.text = "���莸�s�E�E�E";
                                        Debug.Log("�ꎞ��~���s");
                                        yield return new WaitForSeconds(waitTime);
                                        Debug.Log("�ĊJ");

                                        textBox.text = "";
                                        Debug.Log("�ꎞ��~���s");
                                        yield return new WaitForSeconds(0.1f);
                                        Debug.Log("�ĊJ");
                                    }
                                }
                                else if (isEnterRoop)
                                {
                                    textBox.text = roopCount + "�A�s��";
                                    Debug.Log("�ꎞ��~���s");
                                    yield return new WaitForSeconds(waitTime);
                                    Debug.Log("�ĊJ");

                                    textBox.text = "";
                                    Debug.Log("�ꎞ��~���s");
                                    yield return new WaitForSeconds(0.1f);
                                    Debug.Log("�ĊJ");

                                    for (int i = 0; i < roopCount; i++)
                                    {
                                        if(heroHP.value >= heroHP.maxValue)
                                        {
                                            textBox.text = "����ȏ�񕜂ł��Ȃ��I";

                                            Debug.Log("�ꎞ��~���s");
                                            yield return new WaitForSeconds(waitTime);
                                            Debug.Log("�ĊJ");

                                            break;
                                        }

                                        Heal(value, heroHP, nowHP, textBox);

                                        Debug.Log("�ꎞ��~���s");
                                        yield return new WaitForSeconds(waitTime);
                                        Debug.Log("�ĊJ");

                                        textBox.text = "";
                                        Debug.Log("�ꎞ��~���s");
                                        yield return new WaitForSeconds(0.1f);
                                        Debug.Log("�ĊJ");
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
                            Debug.Log("�ꎞ��~���s");
                            yield return new WaitForSeconds(waitTime);
                            Debug.Log("�ĊJ");

                            textBox.text = "";
                            Debug.Log("�ꎞ��~���s");
                            yield return new WaitForSeconds(0.1f);
                            Debug.Log("�ĊJ");
                        }
                    }

                    //������
                    isEnterIf = false;
                    isEnterRoop = false;
                    ic = null;

                    break;

                case "if":
                    isEnterIf = true;

                    ic = ifList.Find(x => x.GetCardId() == c.GetCardId());
                    if (ic == null)
                    {
                        Debug.LogError("ifCard�擾���s");
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

        //����Ώۂ��i�[
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


        //������s
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

        textBox.text = damageValue + "�̃_���[�W��^����";
    }

    private void Heal(int healValue, Slider heroHP, TextMeshProUGUI nowHP, Text textBox)
    {
        heroHP.value += healValue;

        heroHPValue = (int)heroHP.value;

        //����HP(����)�̍X�V
        int nowHPValue = (int)heroHP.value;
        nowHP.text = nowHPValue.ToString();

        textBox.text = healValue + "��";
    }
}
