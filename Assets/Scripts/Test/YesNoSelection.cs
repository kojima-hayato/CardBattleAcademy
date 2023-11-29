using UnityEngine;
using UnityEngine.UI;

public class YesNoSelection : MonoBehaviour
{
    public Button yesButton;
    public Button noButton;

    private bool isYesSelected = true; // 「はい」が選択されている状態を示すフラグ

    void Start()
    {
        // 最初に「はい」が選択されていることを示す
        yesButton.Select();
    }

    void Update()
    {
        // 上下キーで選択を切り替える
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            isYesSelected = !isYesSelected; // 選択を切り替える

            // 選択されたボタンを変更する
            if (isYesSelected)
            {
                yesButton.Select();
            }
            else
            {
                noButton.Select();
            }
        }

        // Enterキーで選択を確定する（仮定のため、適宜変更可能）
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isYesSelected)
            {
                // 「はい」が選択された場合の処理
                Debug.Log("はいが選択されました");
                // ここに「はい」が選択された時の処理を記述
            }
            else
            {
                // 「いいえ」が選択された場合の処理
                Debug.Log("いいえが選択されました");
                // ここに「いいえ」が選択された時の処理を記述
            }
        }
    }
}
