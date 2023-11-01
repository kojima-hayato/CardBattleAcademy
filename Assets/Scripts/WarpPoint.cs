using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WarpPoint : MonoBehaviour
{
    public Vector3 pos;
    public CinemachineVirtualCamera virtualCamera;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Cinemachineカメラを一時的に無効にする
            virtualCamera.enabled = false;

            // プレイヤーの位置を変更
            other.gameObject.transform.position = new Vector3(pos.x, pos.y, pos.z);

            // カメラの位置も変更
            Camera mainCamera = Camera.main;
            if (mainCamera != null)
            {
                mainCamera.transform.position = new Vector3(pos.x, pos.y, mainCamera.transform.position.z);
            }

            // 移動が完了したらCinemachineカメラを再度有効にする
            virtualCamera.enabled = true;
        }
    }
}
