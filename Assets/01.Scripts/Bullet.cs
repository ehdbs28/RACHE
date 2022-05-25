using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolableMono
{
    [SerializeField] private float _bulletSpeed = 5f;

    private Rigidbody2D _bulletRigid;
    private Vector3 _mousePos;
    private float _bulletAngle;

    public override void Reset()
    {

    }

    private void Start()
    {
        _bulletRigid = GetComponent<Rigidbody2D>();
        _mousePos = Input.mousePosition;
        _bulletAngle = (float)Mathf.Atan2(_mousePos.y - transform.position.y, _mousePos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(_bulletAngle, Vector3.forward);
        _bulletRigid.AddForce(_mousePos * _bulletSpeed, ForceMode2D.Impulse);
    }
}
