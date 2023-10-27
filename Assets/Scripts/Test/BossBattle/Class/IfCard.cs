using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCard : Card
{
    private int trueIndex, falseIndex;  //判定成功時の処理, 判定失敗時の処理(こっちは消すかも)
    private string judgePattern;        //判定基準(一致・以上・以下など)

    //コンストラクタ(: base()で親のコンストラクタを呼び出す)
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