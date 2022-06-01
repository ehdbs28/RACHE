using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffect : PoolableMono
{
    private Transform _playerTrm;
    private Vector2 _mousePos;

    private void Start()
    {
        _playerTrm = GameObject.Find("Player").GetComponent<Transform>();
        _mousePos = MouseCursor.Instance.MousePos;

        float angle = Mathf.Atan2(_mousePos.y - _playerTrm.position.y, _mousePos.x - _playerTrm.position.x) * Mathf.Rad2Deg;
        
        transform.position = _playerTrm.position;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
    }

    public override void Reset()
    {
    }
}
