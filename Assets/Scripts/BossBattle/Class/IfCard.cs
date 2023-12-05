using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCard : Card
{
    private string judgePattern;    //判定基準(一致・以上・以下など)
    private string judgeTarget;     //判定基準(一致・以上・以下など)
    private double rate;               //上昇倍率

    //コンストラクタ(: base()で親のコンストラクタを呼び出す)
    public IfCard(string cardId, int value, string cardType, string judgePattern, string judgeTarget, double rate) : base(cardId, value, cardType)
    {
        SetJudgePattern(judgePattern);
        SetJudgeTarget(judgeTarget);
        SetRate(rate);
    }

    private void SetJudgePattern(string judgePattern)
    {
        this.judgePattern = judgePattern;
    }
    private void SetJudgeTarget(string judgeTarget)
    {
        this.judgeTarget = judgeTarget;
    }
    private void SetRate(double rate)
    {
        this.rate = rate;
    }

    public string GetJudgePattern()
    {
        return judgePattern;
    }
    public string GetJudgeTarget()
    {
        return judgeTarget;
    }
    public double GetRate()
    {
        return rate;
    }
}