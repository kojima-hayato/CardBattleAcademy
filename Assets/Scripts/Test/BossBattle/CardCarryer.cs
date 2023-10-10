using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCarryer : MonoBehaviour
{
    Vector3 cardPos, mousePos;
    GameObject framePrefab, nowFrame, nextFrame;
    public static List<GameObject> frameList;
    bool isEnterAlgo, isOnFrame, isInFrameList;

    // Start is called before the first frame update
    void Start()
    {
        //複製するプレハブ
        framePrefab = Resources.Load<GameObject>("Frame");

        //既に使用されているフレームのリスト
        frameList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        //フレームに重なったら使用待機状態にする
        if (collision.gameObject.tag == "Frame" && !isEnterAlgo)
        {
            nowFrame = collision.gameObject;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        //フレームに重なっていることを検知する
        if (collision.gameObject == nowFrame && isEnterAlgo)
        {
            isOnFrame = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        //待機状態のフレームの上かつアルゴリズムに組み込まれている場合、解除する
        if (collision.gameObject == nowFrame && isEnterAlgo)
        {
            if (transform.tag == "Roop")
            {
                nowFrame.transform.Translate(0.0f, -1.0f, 0.0f);
            }

            Destroy(nextFrame);
            frameList.Remove(nowFrame);

            nowFrame = null;
            isEnterAlgo = false;
            isOnFrame = false;
        }
    }


    public void OnMouseDown()
    {
        //画面の座標とunity空間の座標を連結させる
        Debug.Log("カードをつかんだ");

        cardPos = Camera.main.WorldToScreenPoint(transform.position);
        cardPos = Camera.main.ScreenToWorldPoint(cardPos);
    }

    public void OnMouseDrag()
    {
        //マウスの動きにカードを追従させる
        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = mousePos;
    }

    public void OnMouseUp()
    {

        //重複検知トークンの初期化
        isInFrameList = false;

        //同じフレームに置こうとすると配置を止める
        if (frameList != null && !isOnFrame)
        {
            foreach (GameObject f in frameList)
            {
                if (f == nowFrame)
                {
                    Debug.Log("同じframeへの配置");
                    isInFrameList = true;
                    break;
                }
            }
        }

        //次のフレームを作成し、今のフレームの位置を調整する
        if (!isInFrameList)
        {
            if (nowFrame != null && !isEnterAlgo)
            {
                isEnterAlgo = true;

                Vector3 nextPos = nowFrame.transform.position;

                if (tag == "Roop")
                {
                    nowFrame.transform.Translate(0.0f, 1.0f, 0.0f);
                    nextPos.y -= 1.0f;
                }
                else
                {
                    nextPos.x += 1.5f;
                }

                nextFrame = Instantiate(framePrefab, nextPos, Quaternion.identity);
                nextFrame.transform.SetParent(nowFrame.transform.parent);

                transform.position = nowFrame.transform.position;
                transform.Translate(0.0f, 0.0f, -1.0f);

                frameList.Add(nowFrame);

            }
            else if (isOnFrame)
            {
                transform.position = nowFrame.transform.position;
                transform.Translate(0.0f, 0.0f, -1.0f);
            }
        }
    }

}
