using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class ConversationScript : MonoBehaviour
{
    public Canvas conversationCanvas;
    public Text conversationText;
    private int currentLine = 0;
    public string[] conversationLines;
    private move_chara playerController;

    //キャラクターごとの会話データを指定するための変数
    public string dialogueFileName;

    // DialogueDataクラス定義が必要です
    [System.Serializable]
    public class DialogueData
    {
        public string[] lines;
    }

   private void Start()
    {
       
        playerController = FindObjectOfType<move_chara>();

        // 最初はCanvasを非表示に
        conversationCanvas.enabled = false;

        // JSONから会話データを読み込む
        LoadDialogueFromJSON(dialogueFileName);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // プレイヤーが触れたら会話を開始
            StartCoroutine(StartConversation());
        }
    }

    
    public IEnumerator StartConversation()
    {
        // 最初はCanvasを表示
        conversationCanvas.enabled = true;

        // 会話開始時にプレイヤーの動きをF止める
        if (playerController != null)
        {
            playerController.SetCanMove(false);
            
        }

        while (currentLine < conversationLines.Length)
        {
            //次の会話文を表示
            conversationText.text = conversationLines[currentLine];
            
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); // クリックを待つ
            yield return new WaitForSeconds(0.1f); // 小さな遅延を挿入してダブルクリックを防ぐ
            currentLine++;
        }

        // 会話終了時にプレイヤーの動きを再開する
        if (playerController != null)
        {
            playerController.SetCanMove(true);
        }




        // 会話が終了したらCanvasを非表示にする
        conversationCanvas.enabled = false;
        currentLine = 0; // 会話をリセット
    }

    //会話ログが終了しているか判定
    public bool IsConversationFinished()
    {
        return currentLine >= conversationLines.Length;

    }

    // JSONファイルからデータを読み込むメソッド
    public void LoadDialogueFromJSON(string fileName)
    {
        TextAsset fileData = Resources.Load<TextAsset>(fileName);
        if (fileData == null)
        {
            Debug.LogError("ファイルが見つからない: " + fileName);
            return;
        }

        DialogueData dialogueData = JsonUtility.FromJson<DialogueData>(fileData.text);
        conversationLines = dialogueData.lines;
        currentLine = 0; // 現在のラインをリセット
    }

    // その他のメソッド
}
