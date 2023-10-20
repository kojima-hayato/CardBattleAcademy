using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCard : Card
{
    private int trueIndex, falseIndex;  //”»’è¬Œ÷‚Ìˆ—, ”»’è¸”s‚Ìˆ—(‚±‚Á‚¿‚ÍÁ‚·‚©‚à)
    private string judgePattern;        //”»’èŠî€(ˆê’vEˆÈãEˆÈ‰º‚È‚Ç)

    public IfCard(GameObject card, int value, string cardType, int trueIndex, int falseIndex, string judgePattern) : base(card, value, cardType)
    {
        SetTrueIndex(trueIndex);
        SetFalseIndex(falseIndex);
        SetJudgePattern(judgePattern);
    }

    private void SetTrueIndex(int trueIndex)
    {
        this.trueIndex = trueIndex;
    }

    private void SetFalseIndex(int falseIndex)
    {
        this.falseIndex = falseIndex;
    }

    private void SetJudgePattern(string judgePattern)
    {
        this.judgePattern = judgePattern;
    }
}