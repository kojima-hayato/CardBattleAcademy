using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetCard : MonoBehaviour
{
    public GameObject cardsParent;
    LayoutGroup layoutGroup;

    // Start is called before the first frame update
    void Start()
    {
        layoutGroup = cardsParent.GetComponent<LayoutGroup>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick()
    {
        //レイアウトグループの再更新を行う
        layoutGroup.CalculateLayoutInputHorizontal();
        layoutGroup.CalculateLayoutInputVertical();
        layoutGroup.SetLayoutHorizontal();
        layoutGroup.SetLayoutVertical();
    }
}
