using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private GameObject cardItem;    //カードの実態
    private string cardId;          //カードの識別番号
    private int value;              //カードが持つ値
    private string cardType;        //roop・if・攻撃・防御など

    public Card(int value, string cardType)
    {
        SetValue(value);
        SetCardType(cardType);
    }

    public Card(string cardId, int value, string cardType)
    {
        SetCardId(cardId);
        SetValue(value);
        SetCardType(cardType);
    }

    public void CallSetCardItem(GameObject g)
    {
        SetCardItem(g);
    }

    //以下Setter,Getter
    private void SetCardId(string cardId)
    {
        this.cardId = cardId;
    }

    private void SetValue(int value)
    {
        this.value = value;
    }

    private void SetCardType(string cardType)
    {
        this.cardType = cardType;
    }

    private void SetCardItem(GameObject cardItem)
    {
        Debug.Log("SetCardItem:" + cardItem);
        this.cardItem = cardItem;
    }

    public GameObject GetCardItem()
    {
        return cardItem;
    }

    public string GetCardId()
    {
        return cardId;
    }

    public int GetValue()
    {
        return value;
    }

    public string GetCardType()
    {
        return cardType;
    }
}