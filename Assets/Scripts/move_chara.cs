using UnityEngine;

public class move_chara : MonoBehaviour
{
    public float speed = 1.5f;
    private Animator animator;
    private Rigidbody2D rb;
    public RandomEncount randomEncount;
    public float speedThreshold = 0.5f;
    private bool canMove = true; // ���͂��󂯕t���邩�ǂ����̃t���O


    
    private Vector3 playerPosition; // ������ playerPosition ��錾
    private Vector2 lastMoveDirection = Vector2.zero;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        randomEncount = FindObjectOfType<RandomEncount>();
        playerPosition = this.transform.position; // �����ʒu��ݒ�

    }





    void FixedUpdate()
    {
        

        if (canMove)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = (x == 0) ? Input.GetAxisRaw("Vertical") : 0.0f;

            Vector2 movement = new Vector2(x, y) * speed;
            rb.MovePosition(rb.position + movement * Time.deltaTime);



        if (x != 0 || y != 0)
            {
                // �ړ��ƃA�j���[�V�����̍X�V
                transform.position += new Vector3(x, y) * Time.deltaTime * speed;
                animator.SetFloat("x", x);
                animator.SetFloat("y", y);
                
                lastMoveDirection = new Vector2(x, y);
            }
            else
            {
                animator.SetFloat("x", lastMoveDirection.x);
                animator.SetFloat("y", lastMoveDirection.y);
            }
           
            UpdateRandomEncounter();
        }
        playerPosition = this.transform.position;
        //Debug.Log(playerPosition);
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
            // ���͂��󂯕t���Ȃ��ꍇ�́A���x�ƃA�j���[�V���������Z�b�g����
            rb.velocity = Vector2.zero;

            //�A�j���[�V�����̕������Ō�̈ړ������ɌŒ�
            animator.SetFloat("x", lastMoveDirection.x);
            animator.SetFloat("y", lastMoveDirection.y);
        }
    }



    // �o�g���G���A����o���Ƃ��̏���
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Field"))
        {
            canMove = true;
        }
        else
        {
            canMove = true; // ���͂��󂯕t����悤�Ƀt���O��ݒ�
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EncounterArea") && canMove)
        {


            // �v���C���[�̓��͂��~�߂�
            canMove = false;

        }

    }
}
