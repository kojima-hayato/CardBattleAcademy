using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationScript : MonoBehaviour
{
    public Canvas conversationCanvas;
    public Text conversationText;
    public string[] conversationLines;
    private int currentLine = 0;

    void Start()
    {
        // 最初はCanvasを非表示にする
        conversationCanvas.enabled = false;

        if (conversationText != null && conversationLines.Length > 0)
        {
            // 初期の会話文を表示
            conversationText.text = conversationLines[currentLine];
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // プレイヤーが触れたらCanvasを表示
            conversationCanvas.enabled = true;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // クリックしたら次の会話文へ
            ShowNextLine();
        }
    }

    void ShowNextLine()
    {
        if (currentLine < conversationLines.Length - 1)
        {
            currentLine++;
            conversationText.text = conversationLines[currentLine];
        }
        else
        {
            // 会話が終了した場合、Canvasを非表示にする
            conversationCanvas.enabled = false;
            Debug.Log("会話終了");
        }
    }
}
