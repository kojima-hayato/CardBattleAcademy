using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomEncount : MonoBehaviour
{
    public float encountChance = 0.2f;
    public move_chara playerMovement;

    void Update()
    {
        Debug.Log("たぶんここ");
        Encount();
    }

    private void Encount()
    {
        Debug.Log("playerMovement の値: " + playerMovement);// playerMovement 変数の値を表示
        if (playerMovement != null)
        {
            Debug.Log("プレイヤーが動いているときの処理");

            Rigidbody2D rb = playerMovement.GetComponent<Rigidbody2D>();
            var PlayerSpeed = rb.velocity.magnitude;
            var RateEncount = Random.Range(0f, 1f); // 0から1の間でランダムな値を取得
            Debug.Log(PlayerSpeed);
            Debug.Log(RateEncount);

            if (PlayerSpeed > 0.5 && RateEncount < encountChance) // encountChanceを利用して確率を設定
            {
                SceneManager.LoadScene("BattleScene");
            }
        }
    }
}