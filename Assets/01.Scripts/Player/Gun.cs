using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _bulletPos;

    private float _angle;
    private Vector2 _mousePoint;
    private Action OnExplosion;

    private void Update()
    {
        FollowMouse();
        FireBullet();
    }

    private void FollowMouse()
    {
        _mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _angle = (float)Math.Atan2(_mousePoint.y - transform.position.y, _mousePoint.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
    }

    private void FireBullet()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Bullet bullet = PoolManager.Instance.Pop("PlayerBullet") as Bullet;
            bullet.transform.position = _bulletPos.position;
            bullet.transform.rotation = _bulletPos.rotation;
        }
    }
}
