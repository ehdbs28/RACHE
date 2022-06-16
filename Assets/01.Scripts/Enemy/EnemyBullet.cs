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
        if (collision.CompareTag("Player"))
        {
            TimeController.Instance.ModifyTimeScale(0.2f, 0.01f, () =>
            {
                TimeController.Instance.ModifyTimeScale(1f, 0.01f);
            });
            CameraManager.Instance.ShakeCam(2f, 0.4f);
            HpManager.Instance.HPDown(5f);
            PoolManager.Instance.Push(this);
        }
    }

    public override void Reset()
    {

    }
}
