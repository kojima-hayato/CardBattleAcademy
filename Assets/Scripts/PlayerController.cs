using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private float moveSpeed = 0.01f;

    private Vector3 lastMoveDirection = Vector3.zero; // 新しい変数を導入

    public SymbolEncounter symbolEncounter;

    private bool canMove = true;
    private Vector3 encounterDirection;

    public static Vector3 playerPosition;

    private void Start()
    {
        this.transform.position = playerPosition;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        playerPosition = this.transform.position;
        if (canMove)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = (x == 0) ? Input.GetAxisRaw("Vertical") : 0.0f;

            if (x != 0 || y != 0)
            {
                animator.SetFloat("x", x);
                animator.SetFloat("y", y);

                // 動く
                transform.position += new Vector3(x, y) * Time.deltaTime * moveSpeed;

                // 最後に入力していた方向を更新
                lastMoveDirection = new Vector3(x, y).normalized;
            }
            else
            {
                animator.SetFloat("x", lastMoveDirection.x);
                animator.SetFloat("y", lastMoveDirection.y);
            }
        }
    }

    // プレイヤーが特定のエリアに入ったときに呼ばれるメソッド
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EncounterArea") && canMove)
        {


            // プレイヤーの入力を止める
            canMove = false;

        }

    }

}
