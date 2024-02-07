using UnityEngine;

public class FitSpriteToSquare : MonoBehaviour
{
    public GameObject square; // Squareオブジェクトをインスペクターからアサイン

    void Start()
    {
        AdjustSpriteToSquareSize();
    }

    void AdjustSpriteToSquareSize()
    {
        if (square == null)
        {
            Debug.LogError("Square GameObjectがアサインされていません。");
            return;
        }

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null || spriteRenderer.sprite == null)
        {
            Debug.LogError("SpriteRendererまたはSpriteが見つかりません。");
            return;
        }

        // Squareのローカルスケールを取得
        Vector3 squareScale = square.transform.localScale;

        // スプライトのピクセルサイズとピクセルパーユニットを考慮して、目標サイズを計算
        float spriteWidth = spriteRenderer.sprite.rect.width / spriteRenderer.sprite.pixelsPerUnit;
        float spriteHeight = spriteRenderer.sprite.rect.height / spriteRenderer.sprite.pixelsPerUnit;

        // スプライトのサイズをSquareのサイズに合わせてスケーリング
        Vector3 scaleRatio = new Vector3(squareScale.x / spriteWidth, squareScale.y / spriteHeight, 1f);

        // さらにスケールを大きく調整する
        scaleRatio *= 1.2f; // ここでの「2f」は例です。必要に応じて調整してください。

        transform.localScale = scaleRatio;
    }
}
