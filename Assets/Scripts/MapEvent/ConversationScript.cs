



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationScript : MonoBehaviour
{
    public Canvas conversationCanvas;
    public Text conversationText;
    public string[] conversationLines;
    private int currentLine = 0;

    void Start()
    {
        // �ŏ���Canvas���\���ɂ���
        conversationCanvas.enabled = false;

        if (conversationText != null && conversationLines.Length > 0)
        {
            // �����̉�b����\��
            conversationText.text = conversationLines[currentLine];
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // �v���C���[���G�ꂽ��Canvas��\��
            conversationCanvas.enabled = true;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // �N���b�N�����玟�̉�b����
            ShowNextLine();
        }
    }

    void ShowNextLine()
    {
        if (currentLine < conversationLines.Length - 1)
        {
            currentLine++;
            conversationText.text = conversationLines[currentLine];
        }
        else
        {
            // ��b���I�������ꍇ�ACanvas���\���ɂ���
            conversationCanvas.enabled = false;
            Debug.Log("��b�I��");
        }
    }

    // ��b���J�n���邽�߂̃��\�b�h��ǉ�
    public IEnumerator StartConversation()
    {
        // �ŏ���Canvas��\��
        conversationCanvas.enabled = true;

        while (currentLine < conversationLines.Length - 1)
        {
            // ���̉�b����\��
            conversationText.text = conversationLines[currentLine];

            // �����ŃN���b�N�҂��Ȃǂ̓��͂�҂�
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            // ���̉�b���ɐi��
            currentLine++;
        }

        // ��b���I��������Canvas���\���ɂ���
        conversationCanvas.enabled = false;
    }

    //��b���O���I�����Ă��邩����
    public bool IsConversationFinished()
    {
        return currentLine >= conversationLines.Length - 1;

    }
}
