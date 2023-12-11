using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    private bool isPlayerNearNPC = false; // �v���C���[��NPC�ɋ߂Â��Ă��邩�ǂ����𔻒肷��t���O
    private Talkrog TalkrogScript; // Talkrog�X�N���v�g�ւ̎Q��

    void Start()
    {
        TalkrogScript = GetComponent<Talkrog>(); // Talkrog�X�N���v�g�ւ̎Q�Ƃ��擾
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            if (TalkrogScript != null) // null�`�F�b�N��ǉ����邱�Ƃ�NullReferenceException��h���܂�
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
            isPlayerNearNPC = false; // �v���C���[��NPC���痣�ꂽ��t���O��������
            TalkrogScript.EndConversation(); // ��b���I���������Ƃ�ʒm���čēx���͂��󂯕t����
        }
    }
}
