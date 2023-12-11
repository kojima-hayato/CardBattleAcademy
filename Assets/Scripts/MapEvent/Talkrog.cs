using UnityEngine;
using UnityEngine.UI;

public class Talkrog : MonoBehaviour
{
    public GameObject dialogueBox; // 会話UIのパネルなどを参照するためのオブジェクト
    public Text dialogueText; // 会話文を表示するUIテキスト
    public string[] conversation; // 会話の内容を格納する配列
    private int index; // 会話のインデックスを管理するための変数
    private bool isConversationActive = false; // 会話中かどうかを判定するフラグ

    void Start()
    {
        index = 0; // 最初の会話から開始するため、インデックスを0に設定する
        dialogueBox.SetActive(false); // 最初は会話UIを非表示にする
    }

    void Update()
    {
        if (isConversationActive && Input.GetMouseButtonDown(0))
        {
            DisplayNextSentence(); // マウスクリックで次の会話を表示する
        }
    }

    public void StartConversation()

    {
        Debug.Log("会話が開始されました");
        isConversationActive = true; // 会話中フラグを立てる
        dialogueBox.SetActive(true); // 会話UIを表示する
        DisplayNextSentence(); // 最初の会話を表示する
    }

    public void DisplayNextSentence()
    {
        // インデックスを増やして次の会話を表示する
        if (index < conversation.Length)
        {
            dialogueText.text = conversation[index]; // 会話文を表示する
            index++;
        }
        else
        {
            EndConversation(); // 会話が終わったら終了処理を行う
        }
    }

    public void EndConversation()
    {
        isConversationActive = false; // 会話終了を示すフラグを下げる
        dialogueBox.SetActive(false); // 会話UIを非表示にする
        index = 0; // インデックスをリセットする
    }
}
