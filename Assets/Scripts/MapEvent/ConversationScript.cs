using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

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
        // Canvasを最初に非表示に設定
        conversationCanvas.enabled = false;

        // 特定のシーンでのみ会話ログを読み込む
        if (SceneManager.GetActiveScene().name == "BlueCastle")
        {
            LoadDialogueFromJSON();
        }
        else if(SceneManager.GetActiveScene().name == "WorldMap")
        {
            LoadDialogueFromJSON();
        }
        else if (SceneManager.GetActiveScene().name == "Village")
        {
            LoadDialogueFromJSON();
        }
        else {

        // 特定のシーンでない場合は何もしない

        }
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

        FindObjectOfType<move_chara>().SetCanMove(false);


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

        FindObjectOfType<move_chara>().SetCanMove(true);
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
