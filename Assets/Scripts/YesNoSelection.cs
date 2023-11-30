using UnityEngine;
using UnityEngine.UI;

public class YesNoSelection : MonoBehaviour
{
    public Button yesButton;
    public Button noButton;
    public GameObject cursor; // �J�[�\��������GameObject
    public GameObject yesNoUI; // �u�͂��v�u�������vUI��\������GameObject

    private bool isYesSelected = true; // �u�͂��v���I������Ă����Ԃ������t���O
    private bool isTouching = false; // �v���C���[���G��Ă��邩�ǂ����̃t���O

    void Start()
    {
        HideYesNoUI(); // �ŏ��́u�͂��v�u�������vUI���\���ɂ���
    }

    void Update()
    {
        if (isTouching)
        {
            // �㉺�L�[�őI����؂�ւ���
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                isYesSelected = !isYesSelected; // �I����؂�ւ���
                SelectButton(isYesSelected); // �I�����ꂽ�{�^����ύX����
            }

            // Enter�L�[�őI�����m�肷��i����̂��߁A�K�X�ύX�\�j
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (isYesSelected)
                {
                    // �u�͂��v���I�����ꂽ�ꍇ�̏���
                    Debug.Log("�͂����I������܂���");
                    // �����Ɂu�͂��v���I�����ꂽ���̏������L�q
                }
                else
                {
                    // �u�������v���I�����ꂽ�ꍇ�̏���
                    Debug.Log("���������I������܂���");
                    // �����Ɂu�������v���I�����ꂽ���̏������L�q
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTouching = true;
            ShowYesNoUI(); // �v���C���[���G�ꂽ���Ɂu�͂��v�u�������vUI��\������
            SelectButton(isYesSelected); // �����̑I����Ԃ�ݒ肷��
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTouching = false;
            HideYesNoUI(); // �v���C���[�����ꂽ���Ɂu�͂��v�u�������vUI���\���ɂ���
        }
    }

    // �I�����ꂽ�{�^�����X�V���郁�\�b�h
    void SelectButton(bool isYes)
    {
        if (isYes)
        {
            yesButton.Select();
            cursor.transform.position = yesButton.transform.position;
        }
        else
        {
            noButton.Select();
            cursor.transform.position = noButton.transform.position;
        }
    }

    // �u�͂��v�u�������vUI��\�����郁�\�b�h
    void ShowYesNoUI()
    {
        yesNoUI.SetActive(true);
    }

    // �u�͂��v�u�������vUI���\���ɂ��郁�\�b�h
    void HideYesNoUI()
    {
        yesNoUI.SetActive(false);
    }
}
