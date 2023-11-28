using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("触られました");
            // プレイヤーが触れたら、宝箱のAnimatorを取得して "Open" トリガーを送信する
            Animator chestAnimator = GetComponent<Animator>();
            if (chestAnimator != null)
            {
                chestAnimator.SetTrigger("Open");
            }
        }
    }
}
