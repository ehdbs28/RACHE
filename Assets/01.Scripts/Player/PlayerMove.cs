using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Slider _playerDashLimit;
    private float _defaultSpeed;

    private Rigidbody2D _rigid;
    private Animator _anim = null;
    private SpriteRenderer _playerSprite;
    private Vector3 dir = Vector3.zero;

    private bool _isDash = false;

    private void Start()
    {
        _defaultSpeed = _moveSpeed;

        _rigid = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();

        _playerDashLimit.value = 1;
    }

    private void Update()
    {
        if(StageManager.Instance.IsGameStart == true)
        {
            Move();
            Flash();
        }
    }

    private void Flash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(_playerDashLimit.value == 1)
            {
                DashEffect dashEffect = PoolManager.Instance.Pop("PlayerDash") as DashEffect;
                _isDash = true;
                _playerDashLimit.DOValue(0, 0.1f);
            }
        }
        if (_playerDashLimit.value == 0)
        {
            _playerDashLimit.DOValue(1, 1f);
        }
        _isDash = false;
    }

    private void Move()
    {
        if (!_isDash)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            dir = new Vector3(x, y, 0);
            dir.Normalize();

            if (x > 0)
            {
                _playerSprite.flipX = false;
            }
            else if (x < 0)
            {
                _playerSprite.flipX = true;
            }

            if (x != 0 || y != 0)
            {
                _anim.SetBool("isMove", true);
            }
            else
            {
                _anim.SetBool("isMove", false);
            }

            _rigid.MovePosition(transform.position + dir * _defaultSpeed * Time.deltaTime);
        }
    }
}
