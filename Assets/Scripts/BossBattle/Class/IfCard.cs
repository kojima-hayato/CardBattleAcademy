using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCard : Card
{
    private string judgePattern;    //����(��v�E�ȏ�E�ȉ��Ȃ�)
    private string judgeTarget;     //����(��v�E�ȏ�E�ȉ��Ȃ�)
    private double rate;               //�㏸�{��

    //�R���X�g���N�^(: base()�Őe�̃R���X�g���N�^���Ăяo��)
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