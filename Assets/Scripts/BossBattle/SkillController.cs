using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour
{
    public GameObject skillMenu;
    public GameObject skillList;
    public GameObject pointer;

    public GameObject textBox;
    public Text text;

    public List<Button> buttons;

    public ScrollRect scrollView;

    List<Text> skills = new List<Text>();

    bool isOpenSkillList;

    int nowSkillIndex = 0;
    float moveValue = 0.2065f;

    DataBaseConnector dbc = new();
    DataTable dt = new();
    string sql;

    // Start is called before the first frame update
    void Start()
    {
        /*
                //DB����X�L�����e���擾����
                sql = "SELECT" +
                " �ǂ���.�Ȃɂ�"
                " FROM �ǂ���"
                " WHERE �ǂ���.�Ȃɂ� = �����ꂩ" +
                " ;";

                dbc.SetCommand();
                dt = dbc.ExecuteSQL(sql);
                foreach (DataRow row in dt.Rows)
                {
                    //row[�v�f��]�Ŏ擾
                    //object�^���炻�ꂼ��Ή�����^�ɃL���X�g����
                }
        */

        //�X�L���̎擾
        foreach (Text t in skillList.GetComponentsInChildren<Text>())
        {
            Debug.Log(t.text);
            skills.Add(t);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpenSkillList)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseMenu();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(UseSkill());
            }

            if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(nowSkillIndex - 2 < 0)
                {
                    nowSkillIndex = skills.Count + (nowSkillIndex - 2);
                    UpToDown(1f);
                }
                else
                {
                    nowSkillIndex -= 2;
                    DownToUp(moveValue);
                }

                MovePointer();
            }

            if (Input.GetKeyDown("a") || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(nowSkillIndex == 0 || nowSkillIndex % 2 == 0)
                {
                    nowSkillIndex += 1;
                }
                else
                {
                    nowSkillIndex -= 1;
                }

                MovePointer();

            }

            if (Input.GetKeyDown("s") || Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (nowSkillIndex + 2 > skills.Count - 1)
                {
                    nowSkillIndex = 2 - (skills.Count - nowSkillIndex);
                    DownToUp(1f);
                }
                else
                {
                    nowSkillIndex += 2;
                    UpToDown(moveValue);
                }

                MovePointer();

            }

            if (Input.GetKeyDown("d") || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (nowSkillIndex == 0 || nowSkillIndex % 2 == 0)
                {
                    nowSkillIndex += 1;
                }
                else
                {
                    nowSkillIndex -= 1;
                }

                MovePointer();
            }
        }
    }

    public void OnClick()
    {
        //�{�^���������Ȃ�����
        foreach (Button b in buttons)
        {
            b.interactable = false;
        }

        skillMenu.SetActive(true);

        isOpenSkillList = true;
    }

    private void CloseMenu()
    {
        //�{�^����������悤�ɂ���
        foreach (Button b in buttons)
        {
            b.interactable = true;
        }

        skillMenu.SetActive(false);

        isOpenSkillList = false;
    }

    private IEnumerator UseSkill()
    {
        Text targetSkill = skills[nowSkillIndex];

        skillMenu.SetActive(false);

        text.text = targetSkill.text + "���g�p����";
        textBox.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        textBox.SetActive(false);

        foreach (Button b in buttons)
        {
            b.interactable = true;
        }
    }

    private void MovePointer()
    {
        Text targetSkill = skills[nowSkillIndex];

        pointer.transform.SetParent(targetSkill.transform);

        pointer.transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    private void DownToUp(float moveValue)
    {
        // �X�N���[����������ɂ���āAnormalizedPosition��ύX
        Vector2 newNormalizedPosition = scrollView.normalizedPosition;

        // ��: ���ɃX�N���[������ꍇ
        newNormalizedPosition.y += moveValue;

        // �V����normalizedPosition���Z�b�g
        scrollView.normalizedPosition = newNormalizedPosition;
    }

    private void UpToDown(float moveValue)
    {
        // �X�N���[����������ɂ���āAnormalizedPosition��ύX
        Vector2 newNormalizedPosition = scrollView.normalizedPosition;

        // ��: ���ɃX�N���[������ꍇ
        newNormalizedPosition.y -= moveValue;

        // �V����normalizedPosition���Z�b�g
        scrollView.normalizedPosition = newNormalizedPosition;

    }
}
