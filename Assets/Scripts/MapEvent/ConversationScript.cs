using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ConversationScript : MonoBehaviour
{
    public Canvas conversationCanvas;
    public Text conversationText;
    private int currentLine = 0;
    public string[] conversationLines;
    private move_chara playerController;

    //�L�����N�^�[���Ƃ̉�b�f�[�^���w�肷�邽�߂̕ϐ�
    public string dialogueFileName;

    // DialogueData�N���X��`���K�v�ł�
    [System.Serializable]
    public class DialogueData
    {
        public string[] lines;
    }

   private void Start()
    {
       
        playerController = FindObjectOfType<move_chara>();

        // �ŏ���Canvas���\����
        conversationCanvas.enabled = false;

        // JSON�����b�f�[�^��ǂݍ���
        LoadDialogueFromJSON(dialogueFileName);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // �v���C���[���G�ꂽ���b���J�n
            StartCoroutine(StartConversation());
        }
    }

    
    public IEnumerator StartConversation()
    {
        // �ŏ���Canvas��\��
        conversationCanvas.enabled = true;

        // ��b�J�n���Ƀv���C���[�̓�����F�~�߂�
        if (playerController != null)
        {
            playerController.SetCanMove(false);
            
        }

        while (currentLine < conversationLines.Length)
        {
            //���̉�b����\��
            conversationText.text = conversationLines[currentLine];
            
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // �N���b�N��҂�
            yield return new WaitForSeconds(0.1f); // �����Ȓx����}�����ă_�u���N���b�N��h��
            currentLine++;
        }

        // ��b�I�����Ƀv���C���[�̓������ĊJ����
        if (playerController != null)
        {
            playerController.SetCanMove(true);
        }




        // ��b���I��������Canvas���\���ɂ���
        conversationCanvas.enabled = false;
        currentLine = 0; // ��b�����Z�b�g
    }

    //��b���O���I�����Ă��邩����
    public bool IsConversationFinished()
    {
        return currentLine >= conversationLines.Length;

    }

    // JSON�t�@�C������f�[�^��ǂݍ��ރ��\�b�h
    public void LoadDialogueFromJSON(string fileName)
    {
        TextAsset fileData = Resources.Load<TextAsset>(fileName);
        if (fileData == null)
        {
            Debug.LogError("�t�@�C����������Ȃ�: " + fileName);
            return;
        }

        DialogueData dialogueData = JsonUtility.FromJson<DialogueData>(fileData.text);
        conversationLines = dialogueData.lines;
        currentLine = 0; // ���݂̃��C�������Z�b�g
    }

    // ���̑��̃��\�b�h
}
