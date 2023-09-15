using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの座標を取得する
        Vector3 vector = player.transform.position;

        //Z軸以外同じ座標にする
        this.transform.position = new Vector3(vector.x, vector.y, -1);
    }
}
