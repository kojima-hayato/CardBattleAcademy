using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_change : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("BlueCastle");//�ړ���̃V�[���̖��O��K���J�ڐ�̃V�[�����ɂ���

        }
    }

}