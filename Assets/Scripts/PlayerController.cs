using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    float moveSpeed = 0.01f;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = (x == 0) ? Input.GetAxisRaw("Vertical") : 0.0f;

        if (x != 0 || y != 0)
        {
            animator.SetFloat("x", x);
            animator.SetFloat("y", y);
        }
        else
        {
            animator.SetFloat("x", 0);
            animator.SetFloat("y", 0);

        }


        //“®‚­
        transform.position += new Vector3(x, y) * Time.deltaTime * moveSpeed;
    }
}