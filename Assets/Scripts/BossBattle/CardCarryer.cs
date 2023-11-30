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
        //��������v���n�u
        framePrefab = Resources.Load<GameObject>("Frame");

        //���Ɏg�p����Ă���t���[���̃��X�g
        frameList = new List<GameObject>();

        //if���⃋�[�v���ɒ�������t���[���̃��X�g
        childFrameList = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        //�t���[���ɏd�Ȃ�����g�p�ҋ@��Ԃɂ���
        if (collision.gameObject.tag == "Frame" && !isEnterAlgo)
        {
            Debug.Log("�t���[���ɐG�ꂽ");
            nowFrame = collision.gameObject;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        //�t���[���ɏd�Ȃ��Ă��邱�Ƃ����m����
        if (collision.gameObject == nowFrame && isEnterAlgo)
        {
            isOnFrame = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        //�ҋ@��Ԃ̃t���[���̏ォ�A���S���Y���ɑg�ݍ��܂�Ă���ꍇ�A��������
        if (collision.gameObject == nowFrame)
        {
            Debug.Log("�t���[�����痣�ꂽ");
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
        //��ʂ̍��W��unity��Ԃ̍��W��A��������
        Debug.Log("�J�[�h������");

        cardPos = Camera.main.WorldToScreenPoint(transform.position);
        cardPos = Camera.main.ScreenToWorldPoint(cardPos);
    }

    public void OnMouseDrag()
    {
        //�}�E�X�̓����ɃJ�[�h��Ǐ]������
        mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = mousePos;
    }

    public void OnMouseUp()
    {
        Debug.Log("�J�[�h�𗣂���");

        //�����t���[���ɒu�����Ƃ���Ɣz�u���~�߂�
        if (frameList != null && !isOnFrame)
        {
            isInFrameList = CheckDuplicate(frameList, nowFrame);
        }

        //���d���[�v���~�߂�
        if (childFrameList != null && !isOnFrame)
        {
            isInChildFrameList = CheckDuplicate(childFrameList, nowFrame);
        }

        //���̃t���[�����쐬���A���̃t���[���̈ʒu�𒲐�����
        if (!isInFrameList)
        {
            if (nowFrame != null && !isEnterAlgo)
            {
                Debug.Log("AddToAlgo���s");
                ab.AddToAlgo(gameObject, isInChildFrameList);

                isEnterAlgo = true;

                Vector3 nextPos = nowFrame.transform.position;
                Vector3 childPos = nowFrame.transform.position;
                Vector3 truePos = nowFrame.transform.position;
                Vector3 falsePos = nowFrame.transform.position;

                //���[�v�╪��̑ΏۂłȂ���Ύ��̃t���[����z�u����
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

                        //true����̃t���[��
                        truePos.x += 1.5f;
                        truePos.y += 1.0f;

                        trueFrame = Instantiate(framePrefab, truePos, Quaternion.identity);
                        trueFrame.transform.SetParent(nowFrame.transform.parent);

                        //false����̃t���[��
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

                    //�J�[�h�̈ʒu���t���[���ɍ��킹�A�O�ɕ\��������
                    transform.position = nowFrame.transform.position;
                    transform.Translate(0.0f, 0.0f, -1.0f);

                    frameList.Add(nowFrame);
                }
                else
                {
                    if(!(tag == "Roop" || tag == "If"))
                    {
                        frameList.Add(nowFrame);

                        //�J�[�h�̈ʒu���t���[���ɍ��킹�A�O�ɕ\��������
                        transform.position = nowFrame.transform.position;
                        transform.Translate(0.0f, 0.0f, -1.0f);
                    } else
                    {
                        Debug.Log("���d���[�v�E���d����̔���");
                    }
                }

            }
            else if (isOnFrame)
            {
                //�J�[�h�̈ʒu���t���[���ɍ��킹�A�O�ɕ\��������
                transform.position = nowFrame.transform.position;
                transform.Translate(0.0f, 0.0f, -1.0f);
            }
        }
    }

    private bool CheckDuplicate(List<GameObject> gameObjects, GameObject target)    //�d���`�F�b�N
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
