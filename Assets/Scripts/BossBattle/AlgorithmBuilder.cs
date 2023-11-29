using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlgorithmBuilder
{
    private List<Card> algoList = new();

    List<Card> hand = CardBuilder.hand;
    List<ActCard> actList = CardBuilder.actList;

    List<int> reserveList = new();

    public void AddToAlgo(GameObject g, bool isInChildFrame)
    {
        int index = algoList.Count;
        Debug.Log(g.name);

        Card c = hand.Find(x => x.GetCardItem().name == g.name);
        Debug.Log(c);
        Debug.Log(c.GetCardType() + "," + c.GetCardId());

        if(c != null)
        {
            if (c.GetCardType() == "roop" || c.GetCardType() == "if")
            {
                algoList.Insert(index, c);
                reserveList.Add(index + 1);
            }
            else
            {
                int hitIndex = reserveList.Find(x => x == index);
                if ((!isInChildFrame && hitIndex == 0) || isInChildFrame)
                {
                    algoList.Insert(index, c);
                }
            }
        }
    }

    public void RemoveFromAlgo(GameObject g)
    {
        Card c = algoList.Find(x => x.GetCardItem() == g);

        if (c.GetCardType() == "roop" || c.GetCardType() == "if")
        {
            int index = algoList.IndexOf(c);
            if(index + 1 < algoList.Count)
            {
                algoList.RemoveAt(index + 1);
            }
        }
        algoList.Remove(c);
    }

    public void ExecuteAlgo(Slider heroHP, Slider bossHP)
    {
        ActCard ac;

        bool isEnterIf = false;
        bool isEnterRoop = false;

        int roopCount = 0;

        foreach (Card c in algoList)
        {
            if(c.GetCardType() == "act")
            {
                ac = actList.Find(x => x.GetCardId() == c.GetCardId());
                if(ac != null)
                {
                    int value = ac.GetValue();
                    switch (ac.GetActType())
                    {
                        case "attack":
                            Debug.Log("攻撃");
                            if (isEnterIf)
                            {

                            } else if (isEnterRoop)
                            {
                                Debug.Log((roopCount - 1) + "連行動");
                                for(int i = 0; i < roopCount; i++)
                                {
                                    bossHP.value -= value;
                                }

                            } else
                            {
                                Debug.Log(value + "のダメージ");
                                bossHP.value -= value;
                            }
                            break;

                        case "heal":
                            Debug.Log("回復");
                            if (isEnterIf)
                            {

                            }
                            else if (isEnterRoop)
                            {
                                Debug.Log((roopCount - 1) + "連行動");
                                for (int i = 0; i < roopCount; i++)
                                {
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
            }
            else
            {
                switch (c.GetCardType())
                {
                    case "if":
                        isEnterIf = true;
                        break;

                    case "roop":
                        isEnterRoop = true;
                        roopCount = c.GetValue();
                        break;
                }
            }
        }

    }

}
