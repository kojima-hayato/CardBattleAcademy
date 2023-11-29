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

    List<Card> deck = new();
    public static List<Card> hand = new();
    public static List<ActCard> actList = new();

    List<string> nameList = new();

    System.Random rnd = new();

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
        string cardId, cardType;
        int value, quantity;

        foreach(DataRow row in dt.Rows)
        {
            //row[�v�f��]�Ŏ擾
            //object�^���炻�ꂼ��Ή�����^�ɃL���X�g����
            cardId = row["card_id"].ToString();
            value = (int)row["value"];
            cardType = row["card_type"].ToString();
            quantity = (int)row["quantity"];

            card = new(cardId, value, cardType);

            //�����ɍ��킹�Ċi�[�񐔂�ς���
            for(int i = 0; i < quantity; i++)
            {
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
        List<Card> acl = new();
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

        //�����_����6���f�b�L�����D�ɉ�����
        //�s���J�[�h��2���m��ň���
        int index;
        for (int i = 0; i < 2; i++)
        {
            index = rnd.Next(0, acl.Count);
            hand.Add(acl[index]);
            deck.Remove(acl[index]);
        }

        //����J�[�h��1���m��ň���
        List<Card> algoCardList = deck.FindAll(x => x.GetCardType() == "if" || x.GetCardType() == "roop");
        index = rnd.Next(0, algoCardList.Count);
        hand.Add(algoCardList[index]);
        deck.Remove(algoCardList[index]);

        for (int i = 0; i < 3; i++)
        {
            index = rnd.Next(0, deck.Count);
            hand.Add(deck[index]);
            deck.RemoveAt(index);
        }

        //��D�Ɋ�Â��ăQ�[���I�u�W�F�N�g�𐶐�����
        foreach (Card c in hand)
        {
            cardType = c.GetCardType();
            if(cardType == "act")
            {
                ActCard ac = actList.Find(x => x.GetCardId() == c.GetCardId());
                switch (actCard.GetActType())
                {
                    case "attack":
                        c.CallSetCardItem(CreateGameObject(attackCard));
                        break;

                    case "heal":
                        c.CallSetCardItem(CreateGameObject(healCard));
                        break;
                }
            }else
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

        //���C�A�E�g�O���[�v�̍čX�V���s��
        layoutGroup = cardParent.GetComponent<LayoutGroup>();
        layoutGroup.CalculateLayoutInputHorizontal();
        layoutGroup.CalculateLayoutInputVertical();
        layoutGroup.SetLayoutHorizontal();
        layoutGroup.SetLayoutVertical();

        foreach(Card c in hand)
        {
            Debug.Log(c);
            Debug.Log(c.GetCardType() + "," + c.GetCardId() + "," + c.GetCardItem().name);
        }

    }

    private GameObject CreateGameObject(GameObject prefab)
    {
        string uniqueName, duplicationName;

        do
        {
            uniqueName = Guid.NewGuid().ToString();
            duplicationName = nameList.Find(x => x == uniqueName);
        } while (duplicationName != null);
        nameList.Add(uniqueName);

        Debug.Log(uniqueName);

        GameObject g = Instantiate(prefab, cardParent.transform);
        g.name = uniqueName;
        return g;
    }
}
