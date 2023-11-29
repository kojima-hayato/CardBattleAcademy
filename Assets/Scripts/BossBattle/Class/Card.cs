using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    private GameObject cardItem;    //�J�[�h�̎���
    private string cardId;          //�J�[�h�̎��ʔԍ�
    private int value;              //�J�[�h�����l
    private string cardType;        //roop�Eif�E�U���E�h��Ȃ�

    public void CallSetCardItem(GameObject g)
    {
        SetCardItem(g);
    }

    public Card(string cardId, int value, string cardType)
    {
        SetCardId(cardId);
        SetValue(value);
        SetCardType(cardType);
    }

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