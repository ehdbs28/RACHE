using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamRig : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private PolygonCollider2D _confiner;
    [SerializeField] private CinemachineVirtualCamera _cmRigCam;

    private Vector3 _boundMax;
    private Vector3 _boundMin;
    private float _halfWidth;

    public PolygonCollider2D Confiner
    {
        get => _confiner;
        set
        {
            _confiner = value;
            Calc();
        }
    }

    private void Calc()
    {
        _boundMax = _confiner.bounds.max;
        _boundMin = _confiner.bounds.min;

        float otho = _cmRigCam.m_Lens.OrthographicSize;

        _halfWidth = otho * 16 / 9;
    }

    private void Update()
    {
        HandleMove();
    }

    private void HandleMove()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 pos = transform.position;
        float minX = _boundMin.x + _halfWidth;
        float maxX = _boundMax.x - _halfWidth;

        float minY = _boundMin.y + _halfWidth;
        float maxY = _boundMax.y - _halfWidth;

        pos.x = Mathf.Clamp(pos.x + _moveSpeed * x * Time.deltaTime, minX, maxX);
        pos.y = Mathf.Clamp(pos.y + _moveSpeed * y * Time.deltaTime, minY, maxY);

        transform.position = pos;
    }
}

