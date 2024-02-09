using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public Slider heroHP;
    public Slider bossHP;

    public TextMeshProUGUI maxHP;
    public TextMeshProUGUI nowHP;

    public GameObject textFrame;
    public Text textBox;

    public List<Button> buttons = new();

    public string sceneName;
    public Vector3 playerPosition;

    private int maxHPValue, nowHPValue;

    private readonly float waitTime = 1.0f;

    private CardBuilder cb;

    private readonly AlgorithmExecuter ae = new();

    //戦闘しているかどうか判別する
    public bool isBattleNow = false;

    // Start is called before the first frame update
    void Start()
    {
        //HPの最大値設定
        heroHP.maxValue = 100;
        bossHP.maxValue = 200;

        //現在HPを最大値に合わせる
        heroHP.value = heroHP.maxValue;
        bossHP.value = bossHP.maxValue;

        //数字を合わせる
        maxHPValue = (int)heroHP.maxValue;
        maxHP.text = maxHPValue.ToString();
        ChangeNowHP();

        cb = FindObjectOfType<CardBuilder>();

        textFrame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick()
    {
        //ボタンを押せなくする
        foreach(Button b in buttons)
        {
            b.interactable = false;
        }

        StartCoroutine(StartAttack());
    }

    private IEnumerator StartAttack()
    {
        ae.BuildAlgo();

        //テキストボックスの表示
        textFrame.SetActive(true);
        isBattleNow = true;

        textBox.text = "勇者の行動";
        Debug.Log("一時停止実行");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("再開");

        textBox.text = "";
        Debug.Log("一時停止実行");
        yield return new WaitForSeconds(0.1f);
        Debug.Log("再開");

        //構築したアルゴリズムを実行する
        yield return StartCoroutine(ae.ExecuteAlgo(heroHP, bossHP, nowHP, textFrame, textBox, waitTime));

        ChangeNowHP();

        if (bossHP.value <= 0)
        {
            textBox.text = "倒した！\n(Enterで終了)";

            // エンターキーが押されるまで待機
            yield return WaitForKeyCode(KeyCode.Return);

            //シーン遷移(マップ)
            SceneManager.sceneLoaded += WarpPlayerAfterScene;

            SceneManager.LoadScene("WorldMap");
        }
        else
        {
            yield return StartCoroutine(EnemyAttack());

            if (heroHP.value <= 0)
            {
                textBox.text = "やられた・・・\n(Enterで終了)";

                // エンターキーが押されるまで待機
                yield return WaitForKeyCode(KeyCode.Return);

                //シーン遷移(ゲームオーバー)
                SceneManager.LoadScene("Title");
            }
            else
            {
                textFrame.SetActive(false);
                isBattleNow = false;

                cb.ReturnDeck();
                cb.DrawCard(0, 0, 3);

                //ボタンを押せるようにする
                foreach (Button b in buttons)
                {
                    b.interactable = true;
                }
            }
        }
    }

    private void ChangeNowHP()
    {
        //現在HP(数字)の更新
        nowHPValue = (int)heroHP.value;
        nowHP.text = nowHPValue.ToString();
    }

    private IEnumerator EnemyAttack()
    {
        int damage = 10;

        textBox.text = "敵の行動";

        Debug.Log("一時停止実行");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("再開");

        textBox.text = "";
        Debug.Log("一時停止実行");
        yield return new WaitForSeconds(0.1f);
        Debug.Log("再開");

        heroHP.value -= damage;
        ChangeNowHP();

        textBox.text = damage + "のダメージを受けた";
        Debug.Log("一時停止実行");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("再開");

        textBox.text = "";
        Debug.Log("一時停止実行");
        yield return new WaitForSeconds(0.1f);
        Debug.Log("再開");
    }
    private IEnumerator WaitForKeyCode(KeyCode keyCode)
    {
        // エンターキーが押されるまで待機
        while (!Input.GetKeyUp(keyCode))
        {
            yield return null;
        }
    }

    void WarpPlayerAfterScene(Scene scene, LoadSceneMode mode)
    {
        foreach(Card c in CardBuilder.hand)
        {
            CardBuilder.deck.Add(c);
            Destroy(c.GetCardItem());
        }
        CardBuilder.hand.Clear();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = playerPosition;
        }

        SceneManager.sceneLoaded -= WarpPlayerAfterScene;
    }
}
