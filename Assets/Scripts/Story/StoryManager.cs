using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance { get; private set; }

    public int StoryProgress { get; set; } = 1;// �����l��1�ɐݒ�

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeStory();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeStory()
    {
        // �X�g�[���[�̏�����Ԃ�ݒ�
        StoryProgress = 1;
    }

    public void AdvanceStory()
    {
        // �X�g�[���[�i�s�󋵂�i�߂�
        StoryProgress++;
        CheckStoryProgress();
    }

    private void CheckStoryProgress()
    {
        // �X�g�[���[�i�s�󋵂ɉ����āA�K�v�ȏ��������s
        switch (StoryProgress)
        {
            case 1:
                // �X�g�[���[�i�s�󋵂�1�̎��̏���
                break;
            case 2:
                // �X�g�[���[�i�s�󋵂�2�̎��̏���
                break;
                // ���̑��̐i�s�󋵂ɑΉ����鏈����ǉ�
        }
    }
}
