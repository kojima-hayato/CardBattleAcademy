using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class EncountSceneChange : MonoBehaviour
{
    public string BossBattle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("��������񓯊��V�[���ǂݍ���");
            StartCoroutine(LoadBossBattleAsync());
        }
    }

    IEnumerator LoadBossBattleAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(BossBattle);

        // �V�[���̓ǂݍ��݂���������܂őҋ@
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log("�V�[���ǂݍ��ݒ�... " + (progress * 100f) + "%");
            yield return null;
        }

        Debug.Log("BossBattle�V�[���ǂݍ��݊���");
    }
}
