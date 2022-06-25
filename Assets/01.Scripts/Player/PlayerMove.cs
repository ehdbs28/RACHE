using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerMove : MonoBehaviour, IDamaged
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Slider _playerDashLimit;
    [SerializeField] private List<GameObject> _BlankBulletLimit = new List<GameObject>();

    private float _defaultSpeed;

    private Rigidbody2D _rigid;
    private AudioSource _playerAudio;
    private Animator _anim = null;
    private SpriteRenderer _playerSprite;
    private Vector3 dir = Vector3.zero;

    private bool _isDash = false;

    private void Start()
    {
        _defaultSpeed = _moveSpeed;

        _rigid = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponent<SpriteRenderer>();
        _playerAudio = GetComponent<AudioSource>();
        _anim = GetComponent<Animator>();

        _playerDashLimit.value = 1;
    }

    private void Update()
    {
        if(StageManager.Instance.IsGameStart == true && StageManager.Instance.IsESC == false && StageManager.Instance.IsSetting == false)
        {
            Move();
            Flash();
            BlankBullet();
        }
    }

    private void Flash()
    {
        if (Input.GetMouseButtonDown(1))
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
            //dir.Normalize();

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

            _rigid.position += (Vector2)dir.normalized * _defaultSpeed * Time.deltaTime;
        }
    }

    private void BlankBullet()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(_BlankBulletLimit.Count > 0)
            {
                _BlankBulletLimit[0].SetActive(false);
                _BlankBulletLimit.RemoveAt(0);
                CameraManager.Instance.ShakeCam(5, 0.2f);
                BlankBullet blankBullet = PoolManager.Instance.Pop("BlankBullet") as BlankBullet;
            }
            else
            {
                CameraManager.Instance.ShakeCam(3, 0.2f);
            }
        }
    }

    public void OnDamaged(float damage)
    {
        _anim.SetTrigger("isDamaged");
        _playerAudio.Play();
        HpManager.Instance.HPDown(damage);
    }
}
