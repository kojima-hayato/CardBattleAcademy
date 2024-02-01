using UnityEngine;

public class FitSpriteToShape : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Transform targetShape;

    void Start()
    {
        if (spriteRenderer == null || targetShape == null) return;

        // ターゲットのサイズを取得（ワールド単位）
        Vector3 shapeSize = targetShape.localScale;

        // スプライトのサイズを取得（ピクセル単位）
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        // ピクセル/ユニット比率を考慮してスケーリング
        float pixelsPerUnit = spriteRenderer.sprite.pixelsPerUnit;
        Vector3 newScale = new Vector3(shapeSize.x / (spriteSize.x / pixelsPerUnit), shapeSize.y / (spriteSize.y / pixelsPerUnit), 1f);
        spriteRenderer.transform.localScale = newScale;
    }
}
