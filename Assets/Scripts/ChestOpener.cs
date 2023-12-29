using UnityEngine;

public class ChestOpener : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("�G���܂���");
            // �v���C���[���G�ꂽ��A�󔠂�Animator���擾���� "Open" �g���K�[�𑗐M����
            Animator chestAnimator = GetComponent<Animator>();
            if (chestAnimator != null)
            {
                chestAnimator.SetTrigger("Open");
            }
        }
    }
}
