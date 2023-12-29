using UnityEngine;
using UnityEngine.UI;

public class Talkrog : MonoBehaviour
{
    public GameObject dialogueBox; // ��bUI�̃p�l���Ȃǂ��Q�Ƃ��邽�߂̃I�u�W�F�N�g
    public Text dialogueText; // ��b����\������UI�e�L�X�g
    public string[] conversation; // ��b�̓��e���i�[����z��
    private int index; // ��b�̃C���f�b�N�X���Ǘ����邽�߂̕ϐ�
    private bool isConversationActive = false; // ��b�����ǂ����𔻒肷��t���O

    void Start()
    {
        index = 0; // �ŏ��̉�b����J�n���邽�߁A�C���f�b�N�X��0�ɐݒ肷��
        dialogueBox.SetActive(false); // �ŏ��͉�bUI���\���ɂ���
    }

    void Update()
    {
        if (isConversationActive && Input.GetMouseButtonDown(0))
        {
            DisplayNextSentence(); // �}�E�X�N���b�N�Ŏ��̉�b��\������
        }
    }

    public void StartConversation()

    {
        Debug.Log("��b���J�n����܂���");
        isConversationActive = true; // ��b���t���O�𗧂Ă�
        dialogueBox.SetActive(true); // ��bUI��\������
        DisplayNextSentence(); // �ŏ��̉�b��\������
    }

    public void DisplayNextSentence()
    {
        // �C���f�b�N�X�𑝂₵�Ď��̉�b��\������
        if (index < conversation.Length)
        {
            dialogueText.text = conversation[index]; // ��b����\������
            index++;
        }
        else
        {
            EndConversation(); // ��b���I�������I���������s��
        }
    }

    public void EndConversation()
    {
        isConversationActive = false; // ��b�I���������t���O��������
        dialogueBox.SetActive(false); // ��bUI���\���ɂ���
        index = 0; // �C���f�b�N�X�����Z�b�g����
    }
}
