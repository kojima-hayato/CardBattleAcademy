using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Warp : MonoBehaviour
{
    public GameObject player;   //プレイヤーオブジェクトを取得
    public GameObject warpPoint;    //対照になるワープポイントの取得
    private Vector3 wp; //ワープ先座標の取得

    public static bool isAfterWarp = false; //ワープ直後か検知(連続ワープの防止)
    public static bool isStay = false;

    // Start is called before the first frame update
    void Start()
    {
        //ワープ先座標を対照となるワープポイントから取得する
        wp = warpPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //プレイヤーが接触すると
        if (collision.gameObject == player)
        {
            //ワープ後でなく、上に乗り続けていない場合
            if (!isAfterWarp && !isStay)
            {
                //ワープする
                player.transform.position = wp;

                isAfterWarp = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //ワープ直後なら
        if(isAfterWarp)
        {
            //プレイヤーが乗り続けた後なら
            if(collision.gameObject == player && isStay)
            {
                isAfterWarp = false;
                isStay = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            isStay = true;
        }
    }
}