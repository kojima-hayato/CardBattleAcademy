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

        //DB����f�b�L���e���擾����
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

        //�擾�������ʂ����X�g�Ɋi�[����
        string cardId;
        int value, quantity;

        foreach(DataRow row in dt.Rows)
        {
            //row[�v�f��]�Ŏ擾
            //object�^���炻�ꂼ��Ή�����^�ɃL���X�g����
            cardId = row["card_id"].ToString();
            value = (int)row["value"];
            cardType = row["card_type"].ToString();
            quantity = (int)row["quantity"];

            //�����ɍ��킹�Ċi�[�񐔂�ς���
            for(int i = 0; i < quantity; i++)
            {
                card = new(cardId, value, cardType);
                deck.Add(card);
            }
        }

        //�s���J�[�h�̍s�����e���擾����
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

        //cardType��act�̂��̂����o��
        acl = new();
        acl = deck.FindAll(x => x.GetCardType() == "act");


        //�擾�������ʂ����X�g�Ɋi�[����
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

        //if�J�[�h�̕���������擾����
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

        //cardType��if�̂��̂����o��
        icl = new();
        icl = deck.FindAll(x => x.GetCardType() == "if");


        //�擾�������ʂ����X�g�Ɋi�[����
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

        //�����_����6���f�b�L�����D�ɉ�����
        //�s���J�[�h��2���m��ň����A����J�[�h��1���m��ň���
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
        //�s���J�[�h
        int index;
        for (int i = 0; i < actNum; i++)
        {
            index = rnd.Next(0, acl.Count);
            hand.Add(acl[index]);
            deck.Remove(acl[index]);
        }

        //����J�[�h
        List<Card> algoCardList = deck.FindAll(x => x.GetCardType() == "if" || x.GetCardType() == "roop");
        for (int i = 0; i < algoNum; i++)
        {
            index = rnd.Next(0, algoCardList.Count);
            hand.Add(algoCardList[index]);
            deck.Remove(algoCardList[index]);
        }

        //�����_��
        for (int i = 0; i < rndNum; i++)
        {
            index = rnd.Next(0, deck.Count);
            hand.Add(deck[index]);
            deck.RemoveAt(index);
        }

        //��D�Ɋ�Â��ăQ�[���I�u�W�F�N�g�𐶐�����
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
        //���C�A�E�g�O���[�v�̍čX�V���s��
        layoutGroup = cardParent.GetComponent<LayoutGroup>();
        layoutGroup.CalculateLayoutInputHorizontal();
        layoutGroup.CalculateLayoutInputVertical();
        layoutGroup.SetLayoutHorizontal();
        layoutGroup.SetLayoutVertical();
    }
}
