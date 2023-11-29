using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCarryer : MonoBehaviour
{
    Vector3 cardPos, mousePos;
    GameObject framePrefab, nowFrame, nextFrame, childFrame, trueFrame, falseFrame;

    public static List<GameObject> frameList, childFrameList;

    bool isEnterAlgo, isOnFrame, isInFrameList, isInChildFrameList;

    public static AlgorithmBuilder ab = new();

    // Start is called before the first frame update
    void Start()
    {
        //複製するプレハブ
        framePrefab = Resources.Load<GameObject>("Frame");

        //既に使用されているフレームのリスト
        frameList = new List<GameObject>();

        //if文やループ文に直結するフレームのリスト
        childFrameList = new List<GameObject>();
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
            Debug.Log("フレームに触れた");
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
        if (collision.gameObject == nowFrame)
        {
            Debug.Log("フレームから離れた");
            if (isEnterAlgo)
            {
                if (!isInChildFrameList)
                {
                    ab.RemoveFromAlgo(gameObject);

                    if (tag == "Roop")
                    {
                        nowFrame.transform.Translate(0.0f, -1.0f, 0.0f);
                        
                        childFrameList.Remove(childFrame);
                        Destroy(childFrame);
                    }
                    else if (tag == "If")
                    {
                        childFrameList.Remove(trueFrame);
                        Destroy(trueFrame);

                        childFrameList.Remove(falseFrame);
                        Destroy(falseFrame);
                    }
                    Destroy(nextFrame);

                    frameList.Remove(nowFrame);
                } else
                {
                    frameList.Remove(nowFrame);
                }
            }

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
        Debug.Log("カードを離した");

        //同じフレームに置こうとすると配置を止める
        if (frameList != null && !isOnFrame)
        {
            isInFrameList = CheckDuplicate(frameList, nowFrame);
        }

        //多重ループを止める
        if (childFrameList != null && !isOnFrame)
        {
            isInChildFrameList = CheckDuplicate(childFrameList, nowFrame);
        }

        //次のフレームを作成し、今のフレームの位置を調整する
        if (!isInFrameList)
        {
            if (nowFrame != null && !isEnterAlgo)
            {
                Debug.Log("AddToAlgo実行");
                ab.AddToAlgo(gameObject, isInChildFrameList);

                isEnterAlgo = true;

                Vector3 nextPos = nowFrame.transform.position;
                Vector3 childPos = nowFrame.transform.position;
                Vector3 truePos = nowFrame.transform.position;
                Vector3 falsePos = nowFrame.transform.position;

                //ループや分岐の対象でなければ次のフレームを配置する
                if (!isInChildFrameList)
                {
                    if (tag == "Roop")
                    {
                        nowFrame.transform.Translate(0.0f, 1.0f, 0.0f);
                        nextPos.x += 1.5f;

                        childPos.y -= 1.0f;

                        childFrame = Instantiate(framePrefab, childPos, Quaternion.identity);
                        childFrame.transform.SetParent(nowFrame.transform.parent);

                        childFrameList.Add(childFrame);

                    }
                    else if (tag == "If")
                    {
                        nextPos.x += 3.0f;

                        //true分岐のフレーム
                        truePos.x += 1.5f;
                        truePos.y += 1.0f;

                        trueFrame = Instantiate(framePrefab, truePos, Quaternion.identity);
                        trueFrame.transform.SetParent(nowFrame.transform.parent);

                        //false分岐のフレーム
                        falsePos.x += 1.5f;
                        falsePos.y -= 1.0f;

                        falseFrame = Instantiate(framePrefab, falsePos, Quaternion.identity);
                        falseFrame.transform.SetParent(nowFrame.transform.parent);

                        childFrameList.Add(trueFrame);
                        childFrameList.Add(falseFrame);
                    }
                    else
                    {
                        nextPos.x += 1.5f;
                    }

                    nextFrame = Instantiate(framePrefab, nextPos, Quaternion.identity, nowFrame.transform.parent);

                    //カードの位置をフレームに合わせ、前に表示させる
                    transform.position = nowFrame.transform.position;
                    transform.Translate(0.0f, 0.0f, -1.0f);

                    frameList.Add(nowFrame);
                }
                else
                {
                    if(!(tag == "Roop" || tag == "If"))
                    {
                        frameList.Add(nowFrame);

                        //カードの位置をフレームに合わせ、前に表示させる
                        transform.position = nowFrame.transform.position;
                        transform.Translate(0.0f, 0.0f, -1.0f);
                    } else
                    {
                        Debug.Log("多重ループ・多重分岐の発生");
                    }
                }

            }
            else if (isOnFrame)
            {
                //カードの位置をフレームに合わせ、前に表示させる
                transform.position = nowFrame.transform.position;
                transform.Translate(0.0f, 0.0f, -1.0f);
            }
        }
    }

    private bool CheckDuplicate(List<GameObject> gameObjects, GameObject target)    //重複チェック
    {
        bool result = false;

        foreach(GameObject g in gameObjects)
        {
            if(g == target)
            {
                result = true;
                break;
            }
        }
        return result;
    }
}
