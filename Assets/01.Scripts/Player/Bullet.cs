using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    [SerializeField] private float _bulletForce = 5f;
    [SerializeField] private float _bulletDamage = 50f;

    public override void Reset()
    {

    }

    private void Update()
    {
        transform.Translate(Vector3.right * _bulletForce * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall") || collision.CompareTag("Door"))
        {
            PoolManager.Instance.Push(this);
        }

        if (collision.CompareTag("Enemy"))
        {
            ScoreManager.Instance.CurrentScore += 50;

            /*TimeController.Instance.ModifyTimeScale(0.3f, 0.01f, () =>
            {
                TimeController.Instance.ModifyTimeScale(1f, 0.01f);
            });
            CameraManager.Instance.ShakeCam(1f, 0.4f);*/
            PoolManager.Instance.Push(this);
            //PoolManager.Instance.Push(collision.GetComponent<Enemy>());
        }

        if (collision.CompareTag("Boss"))
        {
            ScoreManager.Instance.CurrentScore += 300;
            /*TimeController.Instance.ModifyTimeScale(0.3f, 0.01f, () =>
            {
                TimeController.Instance.ModifyTimeScale(1f, 0.01f);
            });
            CameraManager.Instance.ShakeCam(1f, 0.4f);*/
            HpManager.Instance.BossHpDown(_bulletDamage);
            PoolManager.Instance.Push(this);
        }
    }
}
