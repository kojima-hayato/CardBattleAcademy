using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public Slider heroHP;
    public Slider bossHP;

    public TextMeshProUGUI maxHP;
    public TextMeshProUGUI nowHP;

    int maxHPValue, nowHPValue;

    CardBuilder cb;

    AlgorithmBuilder ab = new();
    // Start is called before the first frame update
    void Start()
    {
        //HP�̍ő�l�ݒ�
        heroHP.maxValue = 100;
        bossHP.maxValue = 100;

        //����HP���ő�l�ɍ��킹��
        heroHP.value = heroHP.maxValue;
        bossHP.value = bossHP.maxValue;

        //���������킹��
        maxHPValue = (int)heroHP.maxValue;
        maxHP.text = maxHPValue.ToString();
        ChangeNowHP();

        cb = FindObjectOfType<CardBuilder>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick()
    {
        Attack();
    }

    private void Attack()
    {
        Debug.Log("�E�҂̍s��");
        ab.ExecuteAlgo(heroHP, bossHP);

        Debug.Log("�G�̍s��");
        heroHP.value -= 10;

        ChangeNowHP();

        if(bossHP.value <= 0)
        {
            Debug.Log("�|����");
        }else
        {
            Debug.Log("���̃^�[��");
            cb.ReturnDeck();
            cb.DrawCard(0,0,3);
        }

    }

    private void ChangeNowHP()
    {
        //����HP(����)�̍X�V
        nowHPValue = (int)heroHP.value;
        nowHP.text = nowHPValue.ToString();
    }
}
