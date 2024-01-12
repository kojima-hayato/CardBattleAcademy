using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private bool isPlayerNearNPC = false; // �v���C���[��NPC�ɋ߂Â��Ă��邩�ǂ����𔻒肷��t���O
    public Talkrog TalkrogScript; // Talkrog�X�N���v�g�ւ̎Q��


    void Start()
    {
        

        if (TalkrogScript == null)
        {
            // �蓮�ŃA�^�b�`���邩�AInspector�Őݒ肷��悤�Ɍx����\��
            Debug.LogWarning("Talkrog�X�N���v�g���A�^�b�`����Ă��܂���BInspector�ŃA�^�b�`���邩�A�蓮�ŃA�^�b�`���Ă��������B");
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
                Debug.LogWarning("TalkrogScript��null�ł��B");
            }
        }
    }

}
