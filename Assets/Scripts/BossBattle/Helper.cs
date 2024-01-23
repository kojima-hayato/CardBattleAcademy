using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helper : MonoBehaviour
{

    public GameObject helpWindow;
    public Text helpText;

    public List<Button> buttons;

    List<Card> hand = CardBuilder.hand;
    List<ActCard> actList = CardBuilder.actList;

    private bool isOpenHelp = false;

    private BattleController bc;

    // Start is called before the first frame update
    void Start()
    {
        bc = FindObjectOfType<BattleController>();
    }

    // Update is called once per frame
    void Update()
    {
        // �}�E�X�̈ʒu�����[���h���W�ɕϊ�
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // �}�E�X�̈ʒu���N�_�Ƃ��ă��C�L���X�g�𔭎�
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        // �Փ˂�����ꍇ
        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;

            //���N���b�N�������ƃw���v��\��(�퓬��Ԃł͕\�����Ȃ�)
            if (!bc.isBattleNow && Input.GetMouseButtonDown(1))
            {

                if (!isOpenHelp)
                {
                    isOpenHelp = true;

                    //�{�^���������Ȃ�����
                    foreach (Button b in buttons)
                    {
                        b.interactable = false;
                    }

                    Card c = hand.Find(x => x.GetCardItem() == hitObject);
                    if (c != null)
                    {
                        helpWindow.SetActive(true);

                        switch (c.GetCardType())
                        {
                            case "act":
                                ActCard ac = actList.Find(x => x.GetCardId() == c.GetCardId());

                                switch (ac.GetActType())
                                {

                                    case "attack":
                                        helpText.text = "�U���J�[�h\n" +
                                                        "�G��HP�𐔒l�����炷";
                                        break;

                                    case "heal":
                                        helpText.text = "�񕜃J�[�h\n" +
                                                        "���g��HP�𐔒l���񕜂���";
                                        break;

                                }
                                break;

                            case "roop":
                                helpText.text = "���[�v�J�[�h\n" +
                                                "���ɔz�u�����J�[�h�𐔒l���J��Ԃ����s����";
                                break;

                            case "if":
                                helpText.text = "����J�[�h\n" +
                                                "��������Ɉ�v�����ꍇ�A���ɔz�u�����J�[�h�̐��l�����̃J�[�h�̐��l����Z����\n" +
                                                "��������Ɉ�v���Ȃ������ꍇ�A���̃J�[�h�͎��s�ł��Ȃ�";
                                break;
                        }

                    }
                }
                else
                {
                    isOpenHelp = false;

                    //�{�^����������悤�ɂ���
                    foreach (Button b in buttons)
                    {
                        b.interactable = true;
                    }

                    helpWindow.SetActive(false);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            helpWindow.SetActive(false);
        }

    }
}
