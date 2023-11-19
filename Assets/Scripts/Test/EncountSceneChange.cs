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
            Debug.Log("ここから非同期シーン読み込み");
            StartCoroutine(LoadBossBattleAsync());
        }
    }

    IEnumerator LoadBossBattleAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(BossBattle);

        // シーンの読み込みが完了するまで待機
        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            Debug.Log("シーン読み込み中... " + (progress * 100f) + "%");
            yield return null;
        }

        Debug.Log("BossBattleシーン読み込み完了");
    }
}
