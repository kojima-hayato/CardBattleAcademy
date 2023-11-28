using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private float moveSpeed = 0.01f;

    private Vector3 lastMoveDirection = Vector3.zero; // �V�����ϐ��𓱓�

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

                // ����
                transform.position += new Vector3(x, y) * Time.deltaTime * moveSpeed;

                // �Ō�ɓ��͂��Ă����������X�V
                lastMoveDirection = new Vector3(x, y).normalized;
            }
            else
            {
                animator.SetFloat("x", lastMoveDirection.x);
                animator.SetFloat("y", lastMoveDirection.y);
            }
        }
    }

    // �v���C���[������̃G���A�ɓ������Ƃ��ɌĂ΂�郁�\�b�h
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EncounterArea") && canMove)
        {


            // �v���C���[�̓��͂��~�߂�
            canMove = false;

        }

    }

}
