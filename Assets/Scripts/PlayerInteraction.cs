using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool isPlayerNearNPC = false; // プレイヤーがNPCに近づいているかどうかを判定するフラグ
    public Talkrog TalkrogScript; // Talkrogスクリプトへの参照


    void Start()
    {
        

        if (TalkrogScript == null)
        {
            // 手動でアタッチするか、Inspectorで設定するように警告を表示
            Debug.LogWarning("Talkrogスクリプトがアタッチされていません。Inspectorでアタッチするか、手動でアタッチしてください。");
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            isPlayerNearNPC = true;

            if (TalkrogScript != null)
            {
                TalkrogScript.StartConversation();
            }
            else
            {
                Debug.LogWarning("TalkrogScriptがnullです。");
            }
        }
    }

}
