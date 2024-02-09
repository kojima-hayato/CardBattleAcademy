using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EncountSceneChange : MonoBehaviour
{
    public string BossBattle;
    public ConversationScript conversationScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�G���J�E���g�G���A�ɓ���܂���");
            StartCoroutine(LoadBossBattleAndDisplayConversation());
        }
    }

    IEnumerator LoadBossBattleAndDisplayConversation()
    {
        Debug.Log("�{�X�o�g���V�[���̓ǂݍ��݂��J�n���܂�");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(BossBattle);
        asyncLoad.allowSceneActivation = false; // �V�[���J�ڂ��ꎞ��~

        // ��b���O�̐�����J�n
        if (conversationScript != null)
        {
            yield return StartCoroutine(conversationScript.StartConversation()); // ��b���I������܂ő҂�
        }

        Debug.Log("��b���I���������߁A�V�[���J�ڂ��J�n���܂�");
        asyncLoad.allowSceneActivation = true; // �V�[���J�ڂ�����

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}