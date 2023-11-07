using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomEncount : MonoBehaviour
{
    public float encounterRate = 0.05f; // ランダムエンカウントの発生率 (0から1の間で設定)

    void Update()
    {
        if (Random.value < encounterRate)
        {
            StartEncounter();
        }
    }

    void StartEncounter()
    {
        // バトルシーンに遷移
        SceneManager.LoadScene("BattleScene");
    }
}
