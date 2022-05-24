using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private Animator anim = null;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void LateUpdate()
    {
        Move();
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(x, y, 0);
        dir.Normalize();

        if(x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if(x < 0)
        {
            spriteRenderer.flipX = true;
        }

        if(x != 0 || y != 0)
        {
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }

        //transform.position += dir * _speed * Time.deltaTime;
        rigid.MovePosition(transform.position + dir * _speed * Time.deltaTime);
    }
}
