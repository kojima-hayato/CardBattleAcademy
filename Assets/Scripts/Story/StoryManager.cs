using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance { get; private set; }

    public int StoryProgress { get; set; } = 1;// 初期値を1に設定

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
        // ストーリーの初期状態を設定
        StoryProgress = 1;
    }

    public void AdvanceStory()
    {
        // ストーリー進行状況を進める
        StoryProgress++;
        CheckStoryProgress();
    }

    private void CheckStoryProgress()
    {
        // ストーリー進行状況に応じて、必要な処理を実行
        switch (StoryProgress)
        {
            case 1:
                // ストーリー進行状況が1の時の処理
                break;
            case 2:
                // ストーリー進行状況が2の時の処理
                break;
                // その他の進行状況に対応する処理を追加
        }
    }
}
