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

    //�퓬���Ă��邩�ǂ������ʂ���
    public bool isBattleNow = false;

    // Start is called before the first frame update
    void Start()
    {
        //HP�̍ő�l�ݒ�
        heroHP.maxValue = 100;
        bossHP.maxValue = 200;

        //����HP���ő�l�ɍ��킹��
        heroHP.value = heroHP.maxValue;
        bossHP.value = bossHP.maxValue;

        //���������킹��
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
        //�{�^���������Ȃ�����
        foreach(Button b in buttons)
        {
            b.interactable = false;
        }

        StartCoroutine(StartAttack());
    }

    private IEnumerator StartAttack()
    {
        ae.BuildAlgo();

        //�e�L�X�g�{�b�N�X�̕\��
        textFrame.SetActive(true);
        isBattleNow = true;

        textBox.text = "�E�҂̍s��";
        Debug.Log("�ꎞ��~���s");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("�ĊJ");

        textBox.text = "";
        Debug.Log("�ꎞ��~���s");
        yield return new WaitForSeconds(0.1f);
        Debug.Log("�ĊJ");

        //�\�z�����A���S���Y�������s����
        yield return StartCoroutine(ae.ExecuteAlgo(heroHP, bossHP, nowHP, textFrame, textBox, waitTime));

        ChangeNowHP();

        if (bossHP.value <= 0)
        {
            textBox.text = "�|�����I\n(Enter�ŏI��)";

            // �G���^�[�L�[���������܂őҋ@
            yield return WaitForKeyCode(KeyCode.Return);

            //�V�[���J��(�}�b�v)
            SceneManager.sceneLoaded += WarpPlayerAfterScene;

            SceneManager.LoadScene("WorldMap");
        }
        else
        {
            yield return StartCoroutine(EnemyAttack());

            if (heroHP.value <= 0)
            {
                textBox.text = "���ꂽ�E�E�E\n(Enter�ŏI��)";

                // �G���^�[�L�[���������܂őҋ@
                yield return WaitForKeyCode(KeyCode.Return);

                //�V�[���J��(�Q�[���I�[�o�[)
                SceneManager.LoadScene("Title");
            }
            else
            {
                textFrame.SetActive(false);
                isBattleNow = false;

                cb.ReturnDeck();
                cb.DrawCard(0, 0, 3);

                //�{�^����������悤�ɂ���
                foreach (Button b in buttons)
                {
                    b.interactable = true;
                }
            }
        }
    }

    private void ChangeNowHP()
    {
        //����HP(����)�̍X�V
        nowHPValue = (int)heroHP.value;
        nowHP.text = nowHPValue.ToString();
    }

    private IEnumerator EnemyAttack()
    {
        int damage = 10;

        textBox.text = "�G�̍s��";

        Debug.Log("�ꎞ��~���s");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("�ĊJ");

        textBox.text = "";
        Debug.Log("�ꎞ��~���s");
        yield return new WaitForSeconds(0.1f);
        Debug.Log("�ĊJ");

        heroHP.value -= damage;
        ChangeNowHP();

        textBox.text = damage + "�̃_���[�W���󂯂�";
        Debug.Log("�ꎞ��~���s");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("�ĊJ");

        textBox.text = "";
        Debug.Log("�ꎞ��~���s");
        yield return new WaitForSeconds(0.1f);
        Debug.Log("�ĊJ");
    }
    private IEnumerator WaitForKeyCode(KeyCode keyCode)
    {
        // �G���^�[�L�[���������܂őҋ@
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
