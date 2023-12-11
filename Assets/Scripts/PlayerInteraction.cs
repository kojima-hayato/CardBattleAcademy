using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    private bool isPlayerNearNPC = false; // プレイヤーがNPCに近づいているかどうかを判定するフラグ
    private Talkrog TalkrogScript; // Talkrogスクリプトへの参照

    void Start()
    {
        TalkrogScript = GetComponent<Talkrog>(); // Talkrogスクリプトへの参照を取得
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            if (TalkrogScript != null) // nullチェックを追加することでNullReferenceExceptionを防ぎます
            {
                isPlayerNearNPC = true;
                TalkrogScript.StartConversation();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            isPlayerNearNPC = false; // プレイヤーがNPCから離れたらフラグを下げる
            TalkrogScript.EndConversation(); // 会話が終了したことを通知して再度入力を受け付ける
        }
    }
}
