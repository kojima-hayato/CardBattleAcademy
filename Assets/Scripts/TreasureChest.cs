using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    public GameObject closedChest; // �����󔠂�GameObject
    public GameObject openChest;   // �J�����󔠂�GameObject

    bool isOpen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isOpen)
        {
            isOpen = true;
            closedChest.SetActive(false);
            openChest.SetActive(true);
            // �����ɕ󔠂��J�������̏�����ǉ����邱�Ƃ��ł��܂�
        }
    }
}
