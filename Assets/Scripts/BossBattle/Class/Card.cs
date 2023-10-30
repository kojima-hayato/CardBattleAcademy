using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private GameObject card;    //カードの実体
    private int value;          //カードが持つ値
    private string cardType;    //roop・if・攻撃・防御など

    public Card(GameObject card, int value, string cardType)
    {
        SetCard(card);
        SetValue(value);
        SetCardType(cardType);
    }

    private void SetCard(GameObject card)
    {
        this.card = card;
    }

    private void SetValue(int value)
    {
        this.value = value;
    }

    private void SetCardType(string cardType)
    {
        this.cardType = cardType;
    }

    public GameObject GetCard()
    {
        return card;
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