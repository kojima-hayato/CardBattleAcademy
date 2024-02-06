using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ConversationScript : MonoBehaviour
{
    public Canvas conversationCanvas;
    public Text conversationText;
    private int currentLine = 0;
    private string[] conversationLines;
    public string fileNameBase; // ��b�t�@�C���̃x�[�X��

    [System.Serializable]
    public class DialogueData
    {
        public string[] lines;
    }

    private void Start()
    {
        // Canvas���ŏ��ɔ�\���ɐݒ�
        conversationCanvas.enabled = false;

        // ����̃V�[���ł̂݉�b���O��ǂݍ���
        if (SceneManager.GetActiveScene().name == "BlueCastle")
        {
            LoadDialogueFromJSON();
        }
        else if(SceneManager.GetActiveScene().name == "WorldMap")
        {
            LoadDialogueFromJSON();
        }
        else if (SceneManager.GetActiveScene().name == "Village")
        {
            LoadDialogueFromJSON();
        }
        else {

        // ����̃V�[���łȂ��ꍇ�͉������Ȃ�

        }
    }

    private void LoadDialogueFromJSON()
    {
        // ���݂̃X�g�[���[�i�s�󋵂ɉ������t�@�C�����𐶐�
        Debug.Log("LoadDialogueFromJSON���Ă΂�܂����B���݂̃X�g�[���[�i�s��: " + StoryManager.Instance.StoryProgress);
        string fileName = fileNameBase + StoryManager.Instance.StoryProgress;
        Debug.Log("fileNameBase: " + fileNameBase);

        TextAsset fileData = Resources.Load<TextAsset>(fileName);
        if (fileData == null)
        {
            Debug.LogError("�t�@�C����������Ȃ�: " + fileName);
            return;
        }

        DialogueData dialogueData = JsonUtility.FromJson<DialogueData>(fileData.text);
        conversationLines = dialogueData.lines;
        currentLine = 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(StartConversation());
        }
    }

    public IEnumerator StartConversation()
    {
        conversationCanvas.enabled = true;
        bool waitingForInput = true;

        FindObjectOfType<move_chara>().SetCanMove(false);


        while (currentLine < conversationLines.Length)
        {
            // Debug.Log("���݂̃��C��: " + currentLine);   ��b���O����񂾎��p�̊m�F�f�o�b�O

            conversationText.text = conversationLines[currentLine];

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            // �_�u���N���b�N�h�~�̂��߂̒Z���x��
            yield return new WaitForSeconds(0.2f);

            currentLine++;
            waitingForInput = false;
        }

        conversationCanvas.enabled = false;
        currentLine = 0;

        FindObjectOfType<move_chara>().SetCanMove(true);
    }

    private IEnumerator WaitForStoryManagerInitialization()
    {
        // StoryManager�̏���������������̂�҂�
        yield return new WaitUntil(() => StoryManager.Instance != null);

        // ������������������A��b�f�[�^�����[�h
        LoadDialogueFromJSON();

        // ���̑��̏���������������΂����ɋL�q
    }


    // ��b���e���X�V���邽�߂̃��\�b�h
    public void UpdateDialogue()
    {
        LoadDialogueFromJSON();
    }
}
