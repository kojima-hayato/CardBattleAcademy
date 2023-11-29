using UnityEngine;
using UnityEngine.UI;

public class YesNoSelection : MonoBehaviour
{
    public Button yesButton;
    public Button noButton;

    private bool isYesSelected = true; // �u�͂��v���I������Ă����Ԃ������t���O

    void Start()
    {
        // �ŏ��Ɂu�͂��v���I������Ă��邱�Ƃ�����
        yesButton.Select();
    }

    void Update()
    {
        // �㉺�L�[�őI����؂�ւ���
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            isYesSelected = !isYesSelected; // �I����؂�ւ���

            // �I�����ꂽ�{�^����ύX����
            if (isYesSelected)
            {
                yesButton.Select();
            }
            else
            {
                noButton.Select();
            }
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
