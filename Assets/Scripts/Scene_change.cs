using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_change : MonoBehaviour
{
    public string sceneName;
    public Vector3 playerPosition;

    private bool isFromAnotherScene = false; // �V�[���J�ڂ��������ʂ���

    // �J�ڌ��̃V�[������J�ڂ���ꍇ�Ƀt���O��ݒ肷��
    public void LoadNextSceneFromAnotherScene()
    {
        isFromAnotherScene = true;
        SceneManager.LoadScene(sceneName);
    }

    // �V�[�����ǂݍ��܂ꂽ���Ƀv���C���[�̈ʒu��ݒ肷��
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // �C�x���g�̓o�^����������
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // �J�ڌ�̃V�[���Ńv���C���[�̈ʒu���w�肷��
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �J�ڌ��̃V�[������J�ڂ����ꍇ�̂݃v���C���[�̈ʒu��ύX����
        if (isFromAnotherScene)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = playerPosition;
            }
        }
        // �t���O�����Z�b�g����
        isFromAnotherScene = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LoadNextSceneFromAnotherScene(); // �t���O��ݒ肵�đJ��
        }
    }
}
