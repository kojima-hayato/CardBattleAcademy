using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WarpScript : MonoBehaviour
{
    public Vector3 warpDestination;
    public CinemachineVirtualCamera virtualCamera;

    private bool hasPlayerTouched = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") && !hasPlayerTouched)
        {
            hasPlayerTouched = true;

            // CinemachineVirtualCameraを一時的に無効にする
            virtualCamera.enabled = false;

            // プレイヤーの位置を変更
            other.gameObject.transform.position = warpDestination;

            // カメラの位置も変更
            if (virtualCamera != null)
            {
                virtualCamera.transform.position = new Vector3(warpDestination.x, warpDestination.y, virtualCamera.transform.position.z);
            }

            // カメラとプレイヤーの移動が完了した後、数秒待ってからCinemachineVirtualCameraを再度有効にする
            StartCoroutine(EnableCameraAfterDelay(0.01f));
        }
    }

    private IEnumerator EnableCameraAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // CinemachineVirtualCameraを再度有効にする
        virtualCamera.enabled = true;
    }
}
