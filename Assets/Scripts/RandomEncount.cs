using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomEncount : MonoBehaviour
{
    public float encountChance = 0.2f;

    private move_chara playerMovement; // move_charaスクリプトを参照するための変数

    void Start()
    {
        playerMovement = GetComponent<move_chara>(); // move_charaスクリプトを取得
        
    }

    void Update()
    {
        if (playerMovement != null && playerMovement.IsMoving()) // move_charaスクリプトから移動状態を取得
        {
            TryEncount();
            Debug.Log("ここ");
        }
    }

    void TryEncount()
    {
        if (ShouldEncountOccur())
        {
            StartBattleScene();
        }
    }

    bool ShouldEncountOccur()
    {
        return Random.value < encountChance;
    }

    void StartBattleScene()
    {
        SceneManager.LoadScene("BattleScene");
    }
}
