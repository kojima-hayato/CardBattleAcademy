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
            Debug.Log("エンカウントエリアに入りました");
            StartCoroutine(LoadBossBattleAndDisplayConversation());
        }
    }

    IEnumerator LoadBossBattleAndDisplayConversation()
    {
        Debug.Log("ボスバトルシーンの読み込みを開始します");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(BossBattle);
        asyncLoad.allowSceneActivation = false; // シーン遷移を一時停止

        // 会話ログの制御を開始
        if (conversationScript != null)
        {
            yield return StartCoroutine(conversationScript.StartConversation()); // 会話が終了するまで待つ
        }

        Debug.Log("会話が終了したため、シーン遷移を開始します");
        asyncLoad.allowSceneActivation = true; // シーン遷移を許可

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}