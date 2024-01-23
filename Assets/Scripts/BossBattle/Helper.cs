using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helper : MonoBehaviour
{

    public GameObject helpWindow;
    public Text helpText;

    public List<Button> buttons;

    List<Card> hand = CardBuilder.hand;
    List<ActCard> actList = CardBuilder.actList;

    private bool isOpenHelp = false;

    private BattleController bc;

    // Start is called before the first frame update
    void Start()
    {
        bc = FindObjectOfType<BattleController>();
    }

    // Update is called once per frame
    void Update()
    {
        // マウスの位置をワールド座標に変換
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // マウスの位置を起点としてレイキャストを発射
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        // 衝突がある場合
        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;

            //左クリックを押すとヘルプを表示(戦闘状態では表示しない)
            if (!bc.isBattleNow && Input.GetMouseButtonDown(1))
            {

                if (!isOpenHelp)
                {
                    isOpenHelp = true;

                    //ボタンを押せなくする
                    foreach (Button b in buttons)
                    {
                        b.interactable = false;
                    }

                    Card c = hand.Find(x => x.GetCardItem() == hitObject);
                    if (c != null)
                    {
                        helpWindow.SetActive(true);

                        switch (c.GetCardType())
                        {
                            case "act":
                                ActCard ac = actList.Find(x => x.GetCardId() == c.GetCardId());

                                switch (ac.GetActType())
                                {

                                    case "attack":
                                        helpText.text = "攻撃カード\n" +
                                                        "敵のHPを数値分減らす";
                                        break;

                                    case "heal":
                                        helpText.text = "回復カード\n" +
                                                        "自身のHPを数値分回復する";
                                        break;

                                }
                                break;

                            case "roop":
                                helpText.text = "ループカード\n" +
                                                "下に配置したカードを数値分繰り返し実行する";
                                break;

                            case "if":
                                helpText.text = "分岐カード\n" +
                                                "分岐条件に一致した場合、下に配置したカードの数値をこのカードの数値分乗算する\n" +
                                                "分岐条件に一致しなかった場合、下のカードは実行できない";
                                break;
                        }

                    }
                }
                else
                {
                    isOpenHelp = false;

                    //ボタンを押せるようにする
                    foreach (Button b in buttons)
                    {
                        b.interactable = true;
                    }

                    helpWindow.SetActive(false);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            helpWindow.SetActive(false);
        }

    }
}
