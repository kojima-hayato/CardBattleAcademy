using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActCard : Card
{
    private string actType;

    public ActCard(string cardId, int value, string cardType, string actType) : base(cardId, value, cardType)
    {
        SetActType(actType);
    }

    private void SetActType(string actType)
    {
        this.actType = actType;
    }

    public string GetActType()
    {
        return actType;
    }
}
