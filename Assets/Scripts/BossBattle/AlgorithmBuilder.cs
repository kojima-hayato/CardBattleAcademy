using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgorithmBuilder : MonoBehaviour
{
    List<GameObject> algoList = new();

    public void AddToAlgo(GameObject g)
    {
        int index = algoList.Count;

        //リストが空なら追加、そうでなければ配置位置に基づいて挿入
        if (index == 0)
        {
            algoList.Add(g);
        } else
        {

        }
    }

    public void RemoveFromAlgo(GameObject g)
    {
        algoList.Remove(g);
    }
}
