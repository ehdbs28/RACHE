using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _bulletPos;

    private float _angle;
    private Action OnExplosion;

    private void Update()
    {
        if (StageManager.Instance.IsGameStart == true)
        {
            FollowMouse();
            FireBullet();
        }
    }

    private void FollowMouse()
    {
        _angle = (float)Math.Atan2(MouseCursor.Instance.MousePos.y - transform.position.y, MouseCursor.Instance.MousePos.x - transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(_angle, Vector3.forward);
    }

    private void FireBullet()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            TimeController.Instance.ModifyTimeScale(0.3f, 0.01f, () =>
            {
                TimeController.Instance.ModifyTimeScale(1f, 0.01f);
            });
            CameraManager.Instance.ShakeCam(1f, 0.4f);
            Bullet bullet = PoolManager.Instance.Pop("PlayerBullet") as Bullet;
            bullet.transform.position = _bulletPos.position;
            bullet.transform.rotation = _bulletPos.rotation;
        }
    }
}
