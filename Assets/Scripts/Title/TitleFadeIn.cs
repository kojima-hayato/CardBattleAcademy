using UnityEngine;

public class TitleFadeIn : MonoBehaviour
{
    public float fallSpeed = 2.0f; // �~�����x
    public Vector3 startPosition; // �摜�̊J�n�ʒu
    public Vector3 endPosition; // �摜�̏I���ʒu

    void Start()
    {
        // �摜�̊J�n�ʒu����ʏ㕔�̒����ɐݒ肷��
        startPosition = new Vector3(Screen.width / 2, Screen.height, 0);

        Debug.Log("Start Position: " + startPosition); // �J�n�ʒu���f�o�b�O���O�ŕ\��

        // �摜�̏I���ʒu����ʒ�����菭����̈ʒu�ɐݒ肷��i��Ƃ��āA��ʂ̍�����3/4�ɐݒ�j
        endPosition = new Vector3(Screen.width / 2, Screen.height * 3 / 5, 0);

        Debug.Log("End Position: " + endPosition); // �I���ʒu���f�o�b�O���O�ŕ\��
        transform.position = startPosition;
    }

    void Update()
    {
        // �摜���ォ�牺�Ɉړ�������
        transform.position = Vector3.Lerp(transform.position, endPosition, fallSpeed * Time.deltaTime);
    }
}
