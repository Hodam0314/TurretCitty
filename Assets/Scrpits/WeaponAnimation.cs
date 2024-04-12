using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    Animator anim;
    Vector3 moveDir;
    private float moveSpeed = 3f;


    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        checkMoving();
        checkAnimation();
    }

    private void checkMoving()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        moveDir.y = Input.GetAxisRaw("Vertical") * moveSpeed;
    }

    private void checkAnimation()
    {
        anim.SetInteger("isMoving", (int)moveDir.x);
        anim.SetInteger("isJump", (int)moveDir.y);
    }

}
