using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private Animator anim = null;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();

        if(DateTime.Now.Hour == 24)
        {
            Debug.LogError("»¡·¡");
        }
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

        transform.position += dir * _speed * Time.deltaTime;
    }
}
