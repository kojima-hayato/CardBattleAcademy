using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class CardBuilder : MonoBehaviour
{
    public GameObject cardParent, attackCard, healCard, ifCard, roopCard;

    LayoutGroup layoutGroup;

    DataBaseConnector dbc = new();
    DataTable dt = new();
    string sql;

    Card card;
    ActCard actCard;
    IfCard ifc;

    List<Card> deck = new();
    public static List<Card> hand = new();
    public static List<ActCard> actList = new();
    public static List<IfCard> ifList = new();

    List<string> nameList = new();

    System.Random rnd = new();

    List<Card> acl;
    List<Card> icl;
    string cardType;

    // Start is called before the first frame update
    void Start()
    {

        //DBからデッキ内容を取得する
        sql = "SELECT" +
        " dc.card_id," +
        " dc.card_type," +
        " dc.value," +
        " bcd.quantity" +
        " FROM data_card AS dc," +
        " battle_card_deck AS bcd" +
        " WHERE dc.card_id = bcd.card_id" +
        " ;";

        dbc.SetCommand();
        dt = dbc.ExecuteSQL(sql);

        //取得した結果をリストに格納する
        string cardId;
        int value, quantity;

        foreach(DataRow row in dt.Rows)
        {
            //row[要素名]で取得
            //object型からそれぞれ対応する型にキャストする
            cardId = row["card_id"].ToString();
            value = (int)row["value"];
            cardType = row["card_type"].ToString();
            quantity = (int)row["quantity"];

            //枚数に合わせて格納回数を変える
            for(int i = 0; i < quantity; i++)
            {
                card = new(cardId, value, cardType);
                deck.Add(card);
            }
        }

        //行動カードの行動内容を取得する
        sql = "SELECT" +
            " bcd.card_id," +
            " dca.act_type" +
            " FROM" +
            " data_card_act AS dca," +
            " battle_card_deck AS bcd" +
            " WHERE dca.card_id = bcd.card_id" +
            " ;";

        dbc.SetCommand();
        dt = dbc.ExecuteSQL(sql);

        //cardTypeがactのものを取り出す
        acl = new();
        acl = deck.FindAll(x => x.GetCardType() == "act");


        //取得した結果をリストに格納する
        cardId = null;
        string actType;

        foreach (DataRow row in dt.Rows)
        {
            cardId = row["card_id"].ToString();
            actType = row["act_type"].ToString();

            foreach(Card c in acl)
            {
                if(c.GetCardId() == cardId)
                {
                    actCard = new(c.GetCardId(), c.GetValue(), c.GetCardType(), actType);
                    actList.Add(actCard);
                }
            }
        }

        //ifカードの分岐条件を取得する
        sql = "SELECT" +
            " bcd.card_id," +
            " dci.judge_pattern," +
            " dci.judge_target," +
            " dci.rate" +
            " FROM" +
            " data_card_if AS dci," +
            " battle_card_deck AS bcd" +
            " WHERE dci.card_id = bcd.card_id" +
            " ;";

        dbc.SetCommand();
        dt = dbc.ExecuteSQL(sql);

        //cardTypeがifのものを取り出す
        icl = new();
        icl = deck.FindAll(x => x.GetCardType() == "if");


        //取得した結果をリストに格納する
        cardId = null;
        string judgePattern, judgeTarget;
        double rate;

        foreach (DataRow row in dt.Rows)
        {

            cardId = row["card_id"].ToString();
            judgePattern = row["judge_pattern"].ToString();
            judgeTarget = row["judge_target"].ToString();
            rate = double.Parse(row["rate"].ToString());

            foreach (Card c in icl)
            {
                if (c.GetCardId() == cardId)
                {
                    ifc = new(c.GetCardId(), c.GetValue(), c.GetCardType(), judgePattern, judgeTarget, rate);
                    Debug.Log("judgePattern:" + ifc.GetJudgePattern());
                    ifList.Add(ifc);
                }
            }
        }

        //ランダムに6枚デッキから手札に加える
        //行動カードは2枚確定で引く、分岐カードは1枚確定で引く
        DrawCard(2, 1, 3);
    }

    public void ReturnDeck()
    {
        List<GameObject> cardItemList = CardCarryer.cardItemList;
        List<Card> RemoveTarget = new();

        foreach (GameObject g in cardItemList)
        {
            Card c = hand.Find(x => x.GetCardItem() == g);
            if(c != null)
            {
                RemoveTarget.Add(c);
            }
        }

        foreach(Card c in RemoveTarget)
        {

            deck.Add(c);
            hand.Remove(c);

            Destroy(c.GetCardItem());
        }
    }

    public void DrawCard(int actNum, int algoNum, int rndNum)
    {
        //行動カード
        int index;
        for (int i = 0; i < actNum; i++)
        {
            index = rnd.Next(0, acl.Count);
            hand.Add(acl[index]);
            deck.Remove(acl[index]);
        }

        //分岐カード
        List<Card> algoCardList = deck.FindAll(x => x.GetCardType() == "if" || x.GetCardType() == "roop");
        for (int i = 0; i < algoNum; i++)
        {
            index = rnd.Next(0, algoCardList.Count);
            hand.Add(algoCardList[index]);
            deck.Remove(algoCardList[index]);
        }

        //ランダム
        for (int i = 0; i < rndNum; i++)
        {
            index = rnd.Next(0, deck.Count);
            hand.Add(deck[index]);
            deck.RemoveAt(index);
        }

        //手札に基づいてゲームオブジェクトを生成する
        foreach (Card c in hand)
        {
            if(c.GetCardItem() == null)
            {
                cardType = c.GetCardType();
                if (cardType == "act")
                {
                    ActCard ac = actList.Find(x => x.GetCardId() == c.GetCardId());
                    switch (ac.GetActType())
                    {
                        case "attack":
                            c.CallSetCardItem(CreateGameObject(attackCard));
                            break;

                        case "heal":
                            c.CallSetCardItem(CreateGameObject(healCard));
                            break;
                    }
                }
                else
                {
                    switch (cardType)
                    {
                        case "if":
                            c.CallSetCardItem(CreateGameObject(ifCard));
                            break;

                        case "roop":
                            c.CallSetCardItem(CreateGameObject(roopCard));
                            break;
                    }
                }
            }
        }

        ResetCardPos();

    }
    private GameObject CreateGameObject(GameObject prefab)
    {
        GameObject g = Instantiate(prefab, cardParent.transform);
        g.name = g.GetInstanceID().ToString();
        return g;
    }

    private void ResetCardPos()
    {
        //レイアウトグループの再更新を行う
        layoutGroup = cardParent.GetComponent<LayoutGroup>();
        layoutGroup.CalculateLayoutInputHorizontal();
        layoutGroup.CalculateLayoutInputVertical();
        layoutGroup.SetLayoutHorizontal();
        layoutGroup.SetLayoutVertical();
    }
}
