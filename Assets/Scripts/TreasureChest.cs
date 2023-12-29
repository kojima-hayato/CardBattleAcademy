using UnityEngine;

public class TreasureChest : MonoBehaviour
{
    public GameObject closedChest; // 閉じた宝箱のGameObject
    public GameObject openChest;   // 開いた宝箱のGameObject

    bool isOpen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isOpen)
        {
            isOpen = true;
            closedChest.SetActive(false);
            openChest.SetActive(true);
            // ここに宝箱が開いた時の処理を追加することもできます
        }
    }
}
