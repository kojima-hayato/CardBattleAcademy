using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoopCard : Card
{
    private int childIndex; //ƒ‹[ƒv‘ÎÛ‚Ìˆ—

    public RoopCard(GameObject card, int value, string type, int childIndex) : base(card, value, type)
    {
        SetChildIndex(childIndex);
    }

    private void SetChildIndex(int childIndex)
    {
        this.childIndex = childIndex;
    }

    public int GetChildIndex()
    {
        return childIndex;
    }
}