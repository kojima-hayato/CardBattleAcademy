using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConversationScript : MonoBehaviour
{
    public Canvas conversationCanvas;
    public Text conversationText;
    private int currentLine = 0;
    private string[] conversationLines;
    public string fileNameBase; // 会話ファイルのベース名

    [System.Serializable]
    public class DialogueData
    {
        public string[] lines;
    }

    private void Start()
    {
        // StoryManagerが存在するか確認
        if (StoryManager.Instance != null)
        {
            LoadDialogueFromJSON();
        }
        else
        {
            Debug.LogError("StoryManagerが見つかりません。");
        }

        

        conversationCanvas.enabled = false;
        LoadDialogueFromJSON();
        StartCoroutine(WaitForStoryManagerInitialization());
    }

    private void LoadDialogueFromJSON()
    {
        // 現在のストーリー進行状況に応じたファイル名を生成
        Debug.Log("LoadDialogueFromJSONが呼ばれました。現在のストーリー進行状況: " + StoryManager.Instance.StoryProgress);
        string fileName = fileNameBase + StoryManager.Instance.StoryProgress;
        Debug.Log("fileNameBase: " + fileNameBase);

        TextAsset fileData = Resources.Load<TextAsset>(fileName);
        if (fileData == null)
        {
            Debug.LogError("ファイルが見つからない: " + fileName);
            return;
        }

        DialogueData dialogueData = JsonUtility.FromJson<DialogueData>(fileData.text);
        conversationLines = dialogueData.lines;
        currentLine = 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(StartConversation());
        }
    }

    public IEnumerator StartConversation()
    {
        conversationCanvas.enabled = true;
        bool waitingForInput = true;

        while (currentLine < conversationLines.Length)
        {
           // Debug.Log("現在のライン: " + currentLine);   会話ログが飛んだ時用の確認デバッグ

            conversationText.text = conversationLines[currentLine];

            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            // ダブルクリック防止のための短い遅延
            yield return new WaitForSeconds(0.2f);

            currentLine++;
            waitingForInput = false;
        }

        conversationCanvas.enabled = false;
        currentLine = 0;
    }

    private IEnumerator WaitForStoryManagerInitialization()
    {
        // StoryManagerの初期化が完了するのを待つ
        yield return new WaitUntil(() => StoryManager.Instance != null);

        // 初期化が完了した後、会話データをロード
        LoadDialogueFromJSON();

        // その他の初期化処理があればここに記述
    }


    // 会話内容を更新するためのメソッド
    public void UpdateDialogue()
    {
        LoadDialogueFromJSON();
    }
}
