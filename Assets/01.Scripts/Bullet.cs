using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    [SerializeField] private float _bulletForce = 5f;
    
    private Rigidbody2D _bulletRigid;

    public override void Reset()
    {

    }

    private void OnEnable()
    {
        _bulletRigid = GetComponent<Rigidbody2D>();

        //_bulletRigid.AddForce(Vector2.right * _bulletForce, ForceMode2D.Impulse);
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
    }
}