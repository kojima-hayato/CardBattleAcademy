using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    public GameObject closedChest; // •Â‚¶‚½•ó” ‚ÌGameObject
    public GameObject openChest;   // ŠJ‚¢‚½•ó” ‚ÌGameObject

    bool isOpen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isOpen)
        {
            isOpen = true;
            closedChest.SetActive(false);
            openChest.SetActive(true);
            // ‚±‚±‚É•ó” ‚ªŠJ‚¢‚½‚Ìˆ—‚ğ’Ç‰Á‚·‚é‚±‚Æ‚à‚Å‚«‚Ü‚·
        }
    }
}
