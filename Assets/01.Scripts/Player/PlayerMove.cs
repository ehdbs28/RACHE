using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    private float _defaultSpeed;

    private Rigidbody2D _rigid;
    private Animator _anim = null;
    private SpriteRenderer _playerSprite;
    private Vector3 dir = Vector3.zero;

    #region 플레이어 점멸 관련 코드
    private Vector2 _mousePos;
    private bool _isDash = false;
    [SerializeField] private float _dashSpeed;
    [SerializeField] private float _defaultTime;
    private float _dashTime;

    private float _rotate;
    private Vector2 _movement;
    #endregion

    private void Start()
    {
        _defaultSpeed = _moveSpeed;

        _rigid = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        Flash();
    }

    private void Flash()
    {
        _mousePos = MouseCursor.Instance.MousePos;
        _movement = new Vector2(_mousePos.x - transform.position.x, _mousePos.y - transform.position.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DashEffect dashEffect = PoolManager.Instance.Pop("PlayerDash") as DashEffect;
            _isDash = true;
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
