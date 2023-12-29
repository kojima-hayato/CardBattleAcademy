using UnityEngine;

public class SimpleAudioPlayer : MonoBehaviour
{
    public AudioClip audioClip; // Inspectorで設定するためのオーディオクリップ
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // このスクリプトがアタッチされているGameObjectのAudioSourceを取得

        if (audioClip != null && audioSource != null)
        {
            // AudioSourceに再生するオーディオクリップを設定して再生する
            audioSource.clip = audioClip;
            audioSource.Play();

            // AudioSourceが再生されているかをデバッグログで確認
            Debug.Log("AudioSourceが再生されました。");
        }
        else
        {
            Debug.LogWarning("オーディオクリップかAudioSourceが設定されていません。");
        }
    }
}
