using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleChange : MonoBehaviour
{
    public string WorldMap;

    // �{�^���������ꂽ�ꍇ�A����Ăяo�����֐�
    public void OnClick()
    {
        Debug.Log("�����ꂽ!");  // ���O���o��
        SceneManager.LoadScene("WorldMap");
    }
}