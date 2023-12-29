using TMPro;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public static bool isUINow; //UI���J���Ă��邩���ʂ���g�[�N��

    public GameObject ui, parents;   //UI�S�̂̊i�[

    TextMeshProUGUI[] tMP;

    int nowIndex = 0;   //���݂̃J�[�\���ʒu

    void Start()
    {
        tMP = parents.GetComponentsInChildren<TextMeshProUGUI>();

        tMP[nowIndex].color = Color.yellow;

        //UI���\���ɂ���
        isUINow = false;
        ui.SetActive(isUINow);
    }

    void Update()
    {
        //Esc�������ƃ}�b�v�ɖ߂�
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isUINow)
            {
                tMP[nowIndex].color = Color.white;
                nowIndex = 0;
                tMP[nowIndex].color = Color.yellow;


                isUINow = false;
                ui.SetActive(isUINow);
            }
        }

        //A�܂��́��������ƍ��̗v�f�Ɉڂ�
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            tMP[nowIndex].color = Color.white;

            if (nowIndex == 0)
            {
                nowIndex = tMP.Length - 1;
            }
            else
            {
                nowIndex -= 1;
            }

            tMP[nowIndex].color = Color.yellow;
        }

        //D�܂��́��������ƉE�̗v�f�Ɉڂ�
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            tMP[nowIndex].color = Color.white;

            if (nowIndex == tMP.Length - 1)
            {
                nowIndex = 0;
            }
            else
            {
                nowIndex += 1;
            }

            tMP[nowIndex].color = Color.yellow;
        }
    }
}
