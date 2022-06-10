using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : PoolableMono
{
    [SerializeField] private float _enemySpeed = 2f;

    private SpriteRenderer _enemySprite;
    private bool isActive = false;
    private Transform _playerTrm;
    private Animator _anim = null;
    private float _distance;
    private Vector3 dir;
    private Sequence sq;
    private bool isExplosion = false;

    private int _angleInterval = 30;
    private int _startAngle = 0;
    private int _endAngle = 360;

    private void OnEnable()
    {
        isActive = true;
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
        _distance = Vector3.Distance(_playerTrm.position, transform.position);

        if (_distance <= 3)
        {
            dir = new Vector3(0, 0, 0);

            if (!isExplosion)
            {
                EnemyExplosion();
            }
        }
        else
        {
            dir = _playerTrm.position - transform.position;
        }

        transform.position += dir.normalized * _enemySpeed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8.8f, 8.8f),
                                                    Mathf.Clamp(transform.position.y, -4.5f, 16.5f));
    }

    private void EnemyExplosion()
    {
        sq = DOTween.Sequence();
        isExplosion = true;

        sq.Append(transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1f));
        sq.Append(transform.DOScale(new Vector3(0, 0, 0), 0.2f));
        sq.OnComplete(() =>
        {
            StartCoroutine(MakeBullet());
            PoolManager.Instance.Push(this);
        });
    }

    IEnumerator MakeBullet()
    {
        for(int fireAngle = _startAngle; fireAngle < _endAngle; fireAngle += _angleInterval)
        {
            EnemyBullet enemyBullet = PoolManager.Instance.Pop("EnemyBullet") as EnemyBullet;
            Vector2 dir = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
            enemyBullet.transform.right = dir;
            enemyBullet.transform.position = transform.position;
        }
        yield return new WaitForSeconds(0.1f);
    }

    public override void Reset()
    {

    }
}
