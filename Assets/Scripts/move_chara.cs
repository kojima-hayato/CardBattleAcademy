using UnityEngine;

public class move_chara : MonoBehaviour
{
    private float speed = 0.011f;
    private Animator animator;

    public RandomEncount randomEncount;

    private void Start()
    {
        animator = GetComponent<Animator>();
        randomEncount = GetComponent<RandomEncount>();
    }

    void Update()
    {
        Vector2 pos = transform.position;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            pos.x += speed;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            pos.x -= speed;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            pos.y += speed;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            pos.y -= speed;
        }

        transform.position = pos;

        if (randomEncount != null)
        {
            randomEncount.playerMovement = this;
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
}