using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : PoolableMono
{
    private Rigidbody2D _rigid2D;
    private float _speed = 10f;

    private void Start()
    {
        _rigid2D = GetComponent<Rigidbody2D>();
        //_rigid2D.velocity = _speed * transform.right;
    }
    private void Update()
    {
        transform.position += transform.right * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Door"))
        {
            PoolManager.Instance.Push(this);
        }
    }

    public override void Reset()
    {

    }
}
