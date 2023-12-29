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
        //�_���[�W�\���N���X
        fdc = fdcParam;

        heroHPvalue = (int)heroHP.value;
        bossHPvalue = (int)bossHP.value;

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

        foreach (Card c in algoList)
        {
            Debug.Log("algoList:" + c.GetCardType());
        }

        //�A���S���Y�������s����

        isEnterIf = false;
        isEnterRoop = false;

        roopCount = 0;

        foreach (Card c in algoList)
        {
            //����������
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
                                Debug.Log("�U��");
                                if (isEnterIf)
                                {
                                    if(ifCheck(ic))
                                    {
                                        Debug.Log("���萬��");
                                        value = (int)(value * ic.GetRate());

                                        Debug.Log(value + "�̃_���[�W");
                                        Attack(value, bossHP, fdc);
                                    }
                                    else
                                    {
                                        Debug.Log("���莸�s");
                                    }
                                }
                                else if (isEnterRoop)
                                {
                                    Debug.Log((roopCount) + "�A�s��");
                                    for (int i = 0; i < roopCount; i++)
                                    {
                                        Debug.Log(value + "�̃_���[�W");
                                        Attack(value, bossHP, fdc);
                                    }

                                }
                                else
                                {
                                    Debug.Log(value + "�̃_���[�W");
                                    Attack(value, bossHP, fdc);
                                }
                                break;

                            case "heal":
                                Debug.Log("��");
                                if (isEnterIf)
                                {
                                    if (ifCheck(ic))
                                    {
                                        Debug.Log("���萬��");
                                        value = (int)(value * ic.GetRate());

                                        Debug.Log(value + "��HP����");
                                        heroHP.value += value;
                                    }
                                    else
                                    {
                                        Debug.Log("���莸�s");
                                    }
                                }
                                else if (isEnterRoop)
                                {
                                    Debug.Log((roopCount) + "�A�s��");
                                    for (int i = 0; i < roopCount; i++)
                                    {
                                        Debug.Log(value + "��HP����");
                                        heroHP.value += value;
                                    }

                                }
                                else
                                {
                                    Debug.Log(value + "��HP����");
                                    heroHP.value += value;
                                }
                                break;
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
            
            yield return new WaitForSeconds(1.0f);
        }
    }

    private bool ifCheck(IfCard i)
    {
        bool result = false;
        int targetValue = 0;

        //����Ώۂ��i�[
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

    private void Attack(int damageValue, Slider bossHP, FloatingDamageController fdc)
    {
        bossHP.value -= damageValue;

        fdc.ShowDamage(damageValue);
    }
}
