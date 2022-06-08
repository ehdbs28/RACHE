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
    private float _halfHeight;

    private void Start()
    {
        Calc();
    }

    private void Calc()
    {
        _boundMax = _confiner.bounds.max;
        _boundMin = _confiner.bounds.min;

        float otho = _cmRigCam.m_Lens.OrthographicSize;

        //_halfWidth = otho * 16 / 9;
        _halfHeight = otho;
    }

    public void HandleMove(float y)
    {
        //float minX = _boundMin.x + _halfWidth;
        //float maxX = _boundMax.x - _halfWidth;

        float minY = _boundMin.y + _halfHeight;
        float maxY = _boundMax.y - _halfHeight;

        Vector3 pos = new Vector3(0, y, 0);

        transform.position +=  pos * _moveSpeed * Time.deltaTime;
        /*transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY));*/
        transform.position = new Vector3(0, Mathf.Clamp(transform.position.y, minY, maxY));
    }
}

