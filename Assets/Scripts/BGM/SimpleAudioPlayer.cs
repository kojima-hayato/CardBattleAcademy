using UnityEngine;

public class SimpleAudioPlayer : MonoBehaviour
{
    public AudioClip audioClip; // Inspector�Őݒ肷�邽�߂̃I�[�f�B�I�N���b�v
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // ���̃X�N���v�g���A�^�b�`����Ă���GameObject��AudioSource���擾

        if (audioClip != null && audioSource != null)
        {
            // AudioSource�ɍĐ�����I�[�f�B�I�N���b�v��ݒ肵�čĐ�����
            audioSource.clip = audioClip;
            audioSource.Play();

            // AudioSource���Đ�����Ă��邩���f�o�b�O���O�Ŋm�F
            Debug.Log("AudioSource���Đ�����܂����B");
        }
        else
        {
            Debug.LogWarning("�I�[�f�B�I�N���b�v��AudioSource���ݒ肳��Ă��܂���B");
        }
    }
}
