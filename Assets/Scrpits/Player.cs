using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 moveDir;
    Animator anim;

    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        moving();
        turning();
        playerAnimation();
    }

    private void moving()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        moveDir.y = Input.GetAxisRaw("Vertical") * moveSpeed;

        transform.position += new Vector3(moveDir.x, moveDir.y, 0) * Time.deltaTime;
    }

    private void turning()
    {
        if (moveDir.x < 0 && transform.localScale.x < 1)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        else if (moveDir.x > 0 && transform.localScale.x > -1)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void playerAnimation()
    {
        anim.SetInteger("isMoving", (int)moveDir.x);
        anim.SetInteger("isJump", (int)moveDir.y);
    }

}
