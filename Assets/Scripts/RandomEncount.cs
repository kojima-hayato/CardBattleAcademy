using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomEncount : MonoBehaviour
{
    public float encountInterval = 0.1f; // ランダムエンカウントの間隔（秒）
    public float encountChance = 0.2f;
    public move_chara playerMovement;

    void Start()
    {
        InvokeRepeating("Encount", 0f, encountInterval);
    }

    public void PlayerIsMoving(bool isMoving)
    {
        if (isMoving)
        {
            float RateEncount = Random.Range(0f, 1f);

            if (RateEncount < encountChance)
            {
                SceneManager.LoadScene("BattleScene");
            }
        }
    }

    private void Encount()
    {
        if (playerMovement != null)
        {
            bool isMoving = playerMovement.IsMoving();

            if (isMoving)
            {
                float RateEncount = Random.Range(0f, 1f);

                if (RateEncount < encountChance)
                {
                    //バトルシーン位置渡す
                    SceneManager.LoadScene("BattleScene");
                }
            }
        }
    }
}
