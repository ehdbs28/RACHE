using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private Rigidbody2D _rigid;
    private Animator _anim = null;
    private SpriteRenderer _playerSprite;
    private Vector3 dir = Vector3.zero;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
    }


    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        dir = new Vector3(x, y, 0);
        dir.Normalize();

        if(x > 0)
        {
            _playerSprite.flipX = false;
        }
        else if(x < 0)
        {
            _playerSprite.flipX = true;
        }

        if(x != 0 || y != 0)
        {
            _anim.SetBool("isMove", true);
        }
        else
        {
            _anim.SetBool("isMove", false);
        }

        _rigid.MovePosition(transform.position + dir * _speed * Time.deltaTime);
    }
}
