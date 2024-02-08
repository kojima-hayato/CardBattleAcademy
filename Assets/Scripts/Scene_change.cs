using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_change : MonoBehaviour
{
    public string sceneName;
    public Vector3 playerPosition;

    private bool isFromAnotherScene = false; // シーン遷移したか判別する

    // 遷移元のシーンから遷移する場合にフラグを設定する
    public void LoadNextSceneFromAnotherScene()
    {
        isFromAnotherScene = true;
        SceneManager.LoadScene(sceneName);
    }

    // シーンが読み込まれた時にプレイヤーの位置を設定する
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // イベントの登録を解除する
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 遷移後のシーンでプレイヤーの位置を指定する
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 遷移元のシーンから遷移した場合のみプレイヤーの位置を変更する
        if (isFromAnotherScene)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = playerPosition;
            }
        }
        // フラグをリセットする
        isFromAnotherScene = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LoadNextSceneFromAnotherScene(); // フラグを設定して遷移
        }
    }
}
