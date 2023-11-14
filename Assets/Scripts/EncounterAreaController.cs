using UnityEngine;
using UnityEngine.Playables;

public class EncounterAreaController : MonoBehaviour
{
    public PlayableDirector playableDirector;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // プレイヤーがEncounterAreaに触れた場合、Timelineを有効にする
            EnableTimeline();
        }
    }

    void EnableTimeline()
    {
        // Timelineを有効にする
        playableDirector.enabled = true;
        // または playableDirector.Play(); でも再生可能
    }
}
