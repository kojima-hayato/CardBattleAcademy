using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_change : MonoBehaviour
{

    public string SceneName;

    private void OnCollisionEnter2D(Collision2D other)
    {

        string Scene = SceneName;

        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(Scene);//移動先のシーンの名前を必ず遷移先のシーン名にする

        }
    }

}