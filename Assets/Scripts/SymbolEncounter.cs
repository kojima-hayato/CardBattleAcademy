using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SymbolEncounter : MonoBehaviour
{
    public float encounterInterval = 2.0f; // エンカウントの間隔（秒）
    public string bossBattleSceneName = "BossBattle"; // ボスバトルのシーン名

    private bool canEncounter = true;

    private void Start()
    {
        // シーン内からプレイヤーのPlayableDirectorを取得
        // ここで必要ならばPlaybleDirectorの設定も行う
    }

    // シンボルエンカウントのトリガーを発生させるメソッド
    public void TriggerEncount()
    {
        if (canEncounter)
        {
            StartCoroutine(EncountCoroutine());
        }
    }

    // エンカウントが発生したときの処理
    private void DoEncount()
    {
        Debug.Log("シンボルエンカウント！ここだ！");

        // ボスバトルのシーンを非同期でロード
        StartCoroutine(LoadBossBattleScene());
    }

    // ボスバトルのシーンを非同期でロードするコルーチン
    private IEnumerator LoadBossBattleScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(bossBattleSceneName);

        // ロードが完了するまで待機
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // シンボルエンカウントの処理を再開
        canEncounter = true;
    }

    // エンカウントが発生するまでの待機時間を設けるコルーチン
    private IEnumerator EncountCoroutine()
    {
        canEncounter = false; // エンカウントの連続発生を防ぐためにフラグをオフにする

        // エンカウントが発生するまでの待機時間
        yield return new WaitForSeconds(encounterInterval);

        DoEncount(); // エンカウントが発生したときの処理を実行
    }
}
