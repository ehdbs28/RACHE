using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffect : PoolableMono
{
    private Vector3 _limitPos = new Vector3(8.5f, 2.5f, 0);
    private Transform _playerTrm;
    private Vector2 _mousePos;
    private Transform _afterDashPos;
    private SpriteRenderer _spriteRenederer;

    private void OnEnable()
    {
        StartCoroutine(PushCoroutine());
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

        _playerTrm.position = new Vector3(Mathf.Clamp(_afterDashPos.position.x, -13f, 13f),
                                                    Mathf.Clamp(_afterDashPos.position.y, -4.5f, 4.8f));
        //_playerTrm.position = _afterDashPos.position;
    }

    IEnumerator PushCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {

    }
}
