using UnityEngine;

public class FitSpriteToShape : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Transform targetShape;

    void Start()
    {
        if (spriteRenderer == null || targetShape == null) return;

        // �^�[�Q�b�g�̃T�C�Y���擾�i���[���h�P�ʁj
        Vector3 shapeSize = targetShape.localScale;

        // �X�v���C�g�̃T�C�Y���擾�i�s�N�Z���P�ʁj
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        // �s�N�Z��/���j�b�g�䗦���l�����ăX�P�[�����O
        float pixelsPerUnit = spriteRenderer.sprite.pixelsPerUnit;
        Vector3 newScale = new Vector3(shapeSize.x / (spriteSize.x / pixelsPerUnit), shapeSize.y / (spriteSize.y / pixelsPerUnit), 1f);
        spriteRenderer.transform.localScale = newScale;
    }
}
