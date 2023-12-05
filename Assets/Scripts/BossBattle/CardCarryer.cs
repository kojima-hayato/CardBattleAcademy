using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCarryer : MonoBehaviour
{
    Vector3 cardPos, mousePos;
    GameObject framePrefab, nextFrame, childFrame, nowFrame;

    public static List<GameObject> frameList, childFrameList, cardItemList;

    bool isEnterAlgo, isOnFrame, isInFrameList, isInChildFrameList;

    // Start is called before the first frame update
    void Start()
    {
        //複製するプレハブ
        framePrefab = Resources.Load<GameObject>("Frame");

        //既に使用されているフレームのリスト
        frameList = new List<GameObject>();

        //if文やループ文が使用するフレームのリスト
        childFrameList = new List<GameObject>();

        //配置の順番を管理するリスト
        cardItemList = new();
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
                    if (tag == "Roop" || tag == "If")
                    {
                        //順番管理リストから削除する
                        GameObject targetObj = cardItemList.Find(x => x == gameObject);
                        //見つかれば削除
                        if (targetObj != null)
                        {
                            int index = cardItemList.IndexOf(targetObj);
                            //ループ対象を除外
                            cardItemList.RemoveAt(index + 1);
                            //本体を除外
                            cardItemList.Remove(targetObj);
                        }
                        else
                        {
                            Debug.LogError("対象が見つかりませんでした");
                        }

                        nowFrame.transform.Translate(0.0f, -1.0f, 0.0f);

                        childFrameList.Remove(childFrame);
                        Destroy(childFrame);
                    } else
                    {
                        cardItemList.Remove(gameObject);
                    }
                    Destroy(nextFrame);
                    frameList.Remove(nowFrame);

                }
                else
                {
                    frameList.Remove(nowFrame);

                    //再度仮配置を行う
                    GameObject targetObj = cardItemList.Find(x => x == gameObject);
                    //見つかれば再度仮配置
                    if (targetObj != null)
                    {
                        Debug.Log("gameObject:" + gameObject);
                        Debug.Log("targetObj:" + targetObj);

                        int index = cardItemList.IndexOf(targetObj);
                        cardItemList[index] = nowFrame;
                    } else
                    {
                        Debug.LogError("対象が見つかりませんでした");
                    }
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
                isEnterAlgo = true;

                Vector3 nextPos = nowFrame.transform.position;
                Vector3 childPos = nowFrame.transform.position;

                //ループや分岐の対象でなければ次のフレームを配置する
                if (!isInChildFrameList)
                {
                    if (tag == "Roop" || tag == "If")
                    {
                        nowFrame.transform.Translate(0.0f, 1.0f, 0.0f);
                        nextPos.x += 1.5f;

                        childPos.y -= 1.0f;

                        childFrame = Instantiate(framePrefab, childPos, Quaternion.identity);
                        childFrame.transform.SetParent(nowFrame.transform.parent);

                        childFrameList.Add(childFrame);

                        //順番管理リストに追加
                        cardItemList.Add(gameObject);
                        //対象用に仮置きする
                        cardItemList.Add(childFrame);
                    }
                    else
                    {
                        nextPos.x += 1.5f;
                        //順番管理リストに追加
                        cardItemList.Add(gameObject);
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

                        //配置したフレームと仮置きしたフレームが一致すれば取得する
                        GameObject reserveObj = cardItemList.Find(x => x == nowFrame);
                        //一致したものがあれば上書きする
                        if(reserveObj != null)
                        {
                            int index = cardItemList.IndexOf(reserveObj);
                            cardItemList[index] = gameObject;
                        }
                        else
                        {
                            Debug.LogError("対象が見つかりませんでした");
                        }
                    } else
                    {
                        Debug.Log("多重ループ・多重分岐の発生");
                    }
                }

            }
            else if (isOnFrame && !isInChildFrameList)
            {
                //カードの位置をフレームに合わせ、前に表示させる
                transform.position = nowFrame.transform.position;
                transform.Translate(0.0f, 0.0f, -1.0f);
            }
        }
    }


    //重複チェック
    private bool CheckDuplicate(List<GameObject> gameObjects, GameObject target)
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
