using UnityEngine;

public class move_chara : MonoBehaviour
{
    public float speed = 1.5f;
    private Animator animator;
    private Rigidbody2D rb;
    public RandomEncount randomEncount;
    public float speedThreshold = 0.5f;
    private bool canMove = true; // 入力を受け付けるかどうかのフラグ



    private Vector3 playerPosition; // ここで playerPosition を宣言
    private Vector2 lastMoveDirection = Vector2.zero;

    private void SavePlayerPosition()
    {
        PlayerPrefs.SetFloat("PlayerPositionX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerPositionY", transform.position.y);
        PlayerPrefs.Save();
    }

    private void LoadPlayerPosition()
    {
        float x = PlayerPrefs.GetFloat("PlayerPositionX", 0);
        float y = PlayerPrefs.GetFloat("PlayerPositionY", 0);
        transform.position = new Vector2(x, y);
    }






    private void Start()
    { 
        LoadPlayerPosition();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        randomEncount = FindObjectOfType<RandomEncount>();
        //playerPosition = this.transform.position; // 初期位置を設定

       

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
                transform.position += new Vector3(x, y) * Time.deltaTime * speed;
                animator.SetFloat("x", x);
                animator.SetFloat("y", y);
                lastMoveDirection = new Vector3(x, y).normalized;
            }
            else
            {
                animator.SetFloat("x", lastMoveDirection.x);
                animator.SetFloat("y", lastMoveDirection.y);
            }

            UpdateRandomEncounter();

        }
    }


    void FixedUpdate()
    {
        if (canMove)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = (x == 0) ? Input.GetAxisRaw("Vertical") : 0.0f;
            Vector2 movement = new Vector2(x, y) * speed;
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime);
        }
    }

    public bool IsMoving()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void UpdateRandomEncounter()
    {
        float playerSpeed = rb.velocity.magnitude;

        if (playerSpeed > speedThreshold && randomEncount != null)
        {
            randomEncount.PlayerIsMoving(true);
        }
        else if (randomEncount != null)
        {
            randomEncount.PlayerIsMoving(false);
        }
    }




    public void SetCanMove(bool value)
    {
        canMove = value;
        if (!value)
        {
            // 入力を受け付けない場合は、速度とアニメーションもリセットする
            rb.velocity = Vector2.zero;

            //アニメーションの方向を最後の移動方向に固定
            animator.SetFloat("x", lastMoveDirection.x);
            animator.SetFloat("y", lastMoveDirection.y);
        }
    }



    // バトルエリアから出たときの処理
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Field"))
        {
            canMove = true;
        }
        else
        {
            canMove = true; // 入力を受け付けるようにフラグを設定
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EncounterArea") && canMove)
        {


            // プレイヤーの入力を止める
            canMove = false;

        }

    }
}
