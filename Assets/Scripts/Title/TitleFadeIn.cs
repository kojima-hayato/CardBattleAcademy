using UnityEngine;

public class TitleFadeIn : MonoBehaviour
{
    public float fallSpeed = 2.0f; // 降下速度
    public Vector3 startPosition; // 画像の開始位置
    public Vector3 endPosition; // 画像の終了位置

    void Start()
    {
        // 画像の開始位置を画面上部の中央に設定する
        startPosition = new Vector3(Screen.width / 2, Screen.height, 0);

        Debug.Log("Start Position: " + startPosition); // 開始位置をデバッグログで表示

        // 画像の終了位置を画面中央より少し上の位置に設定する（例として、画面の高さの3/4に設定）
        endPosition = new Vector3(Screen.width / 2, Screen.height * 3 / 5, 0);

        Debug.Log("End Position: " + endPosition); // 終了位置をデバッグログで表示
        transform.position = startPosition;
    }

    void Update()
    {
        // 画像を上から下に移動させる
        transform.position = Vector3.Lerp(transform.position, endPosition, fallSpeed * Time.deltaTime);
    }
}
