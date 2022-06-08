using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PoolableMono
{
    [SerializeField] private float _enemySpeed = 2f;

    private SpriteRenderer _enemySprite;
    private bool isActive = false;
    private Transform _playerTrm;
    private Animator _anim = null;
    private float _distance;

    private void OnEnable()
    {
        isActive = true;
        _distance = Vector3.Distance(_playerTrm.position, transform.position);
        _enemySprite = GetComponent<SpriteRenderer>();
        _playerTrm = GameObject.Find("Player").GetComponent<Transform>();
        _anim = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        isActive = false;
    }

    private void Update()
    {
        _enemySprite.flipX = transform.position.x > 0 ? true : false;

        if(isActive)
        {
            EnemyMove();
        }
    }

    private void EnemyMove()
    {
        _anim.SetBool("isMove", true);

        transform.position += (_playerTrm.position - transform.position).normalized * _enemySpeed * Time.deltaTime;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8.8f, 8.8f),
                                                    Mathf.Clamp(transform.position.y, -4.5f, 16.5f));
    }

    public override void Reset()
    {

    }
}
