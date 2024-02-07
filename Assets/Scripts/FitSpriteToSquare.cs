using UnityEngine;

public class FitSpriteToSquare : MonoBehaviour
{
    public GameObject square; // Square�I�u�W�F�N�g���C���X�y�N�^�[����A�T�C��

    void Start()
    {
        AdjustSpriteToSquareSize();
    }

    void AdjustSpriteToSquareSize()
    {
        if (square == null)
        {
            Debug.LogError("Square GameObject���A�T�C������Ă��܂���B");
            return;
        }

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null || spriteRenderer.sprite == null)
        {
            Debug.LogError("SpriteRenderer�܂���Sprite��������܂���B");
            return;
        }

        // Square�̃��[�J���X�P�[�����擾
        Vector3 squareScale = square.transform.localScale;

        // �X�v���C�g�̃s�N�Z���T�C�Y�ƃs�N�Z���p�[���j�b�g���l�����āA�ڕW�T�C�Y���v�Z
        float spriteWidth = spriteRenderer.sprite.rect.width / spriteRenderer.sprite.pixelsPerUnit;
        float spriteHeight = spriteRenderer.sprite.rect.height / spriteRenderer.sprite.pixelsPerUnit;

        // �X�v���C�g�̃T�C�Y��Square�̃T�C�Y�ɍ��킹�ăX�P�[�����O
        Vector3 scaleRatio = new Vector3(squareScale.x / spriteWidth, squareScale.y / spriteHeight, 1f);

        // ����ɃX�P�[����傫����������
        scaleRatio *= 1.2f; // �����ł́u2f�v�͗�ł��B�K�v�ɉ����Ē������Ă��������B

        transform.localScale = scaleRatio;
    }
}
