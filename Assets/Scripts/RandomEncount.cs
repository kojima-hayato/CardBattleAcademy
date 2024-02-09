using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomEncount : MonoBehaviour
{
    public float encountInterval; // ランダムエンカウントの間隔（秒）
    public float encountChance;
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
                    // プレイヤーの位置を保存
                    PlayerPrefs.SetFloat("PlayerPositionX", playerMovement.transform.position.x);
                    PlayerPrefs.SetFloat("PlayerPositionY", playerMovement.transform.position.y);
                    PlayerPrefs.Save();

                    SceneManager.LoadScene("BattleScene");
                }
            }
        }
    }
}