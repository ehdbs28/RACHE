using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffect : PoolableMono
{
    private Transform _playerTrm;
    private Vector2 _mousePos;
    private Transform _afterDashPos;

    private SpriteRenderer _spriteRenederer;

    private void OnEnable()
    {
        _spriteRenederer = GetComponent<SpriteRenderer>();
        _playerTrm = GameObject.Find("Player").GetComponent<Transform>();
        _mousePos = MouseCursor.Instance.MousePos;
        _afterDashPos = transform.Find("AfterDashPos").GetComponent<Transform>();

        float angle = Mathf.Atan2(_mousePos.y - _playerTrm.position.y, _mousePos.x - _playerTrm.position.x) * Mathf.Rad2Deg;
        
        if(angle >= 90 || angle <= -90)
        {
            _spriteRenederer.flipY = true;
        }
        else
        {
            _spriteRenederer.flipY = false;
        }

        transform.position = _playerTrm.position;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _playerTrm.position = _afterDashPos.position; //수정필요
    }

    public override void Reset()
    {

    }
}
