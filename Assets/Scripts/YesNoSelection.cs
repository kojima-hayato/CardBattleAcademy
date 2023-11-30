using UnityEngine;
using UnityEngine.UI;

public class YesNoSelection : MonoBehaviour
{
    public Button yesButton;
    public Button noButton;
    public GameObject cursor; // カーソルを示すGameObject
    public GameObject yesNoUI; // 「はい」「いいえ」UIを表示するGameObject

    private bool isYesSelected = true; // 「はい」が選択されている状態を示すフラグ
    private bool isTouching = false; // プレイヤーが触れているかどうかのフラグ

    void Start()
    {
        HideYesNoUI(); // 最初は「はい」「いいえ」UIを非表示にする
    }

    void Update()
    {
        if (isTouching)
        {
            // 上下キーで選択を切り替える
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                isYesSelected = !isYesSelected; // 選択を切り替える
                SelectButton(isYesSelected); // 選択されたボタンを変更する
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTouching = true;
            ShowYesNoUI(); // プレイヤーが触れた時に「はい」「いいえ」UIを表示する
            SelectButton(isYesSelected); // 初期の選択状態を設定する
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTouching = false;
            HideYesNoUI(); // プレイヤーが離れた時に「はい」「いいえ」UIを非表示にする
        }
    }

    // 選択されたボタンを更新するメソッド
    void SelectButton(bool isYes)
    {
        if (isYes)
        {
            yesButton.Select();
            cursor.transform.position = yesButton.transform.position;
        }
        else
        {
            noButton.Select();
            cursor.transform.position = noButton.transform.position;
        }
    }

    // 「はい」「いいえ」UIを表示するメソッド
    void ShowYesNoUI()
    {
        yesNoUI.SetActive(true);
    }

    // 「はい」「いいえ」UIを非表示にするメソッド
    void HideYesNoUI()
    {
        yesNoUI.SetActive(false);
    }
}
