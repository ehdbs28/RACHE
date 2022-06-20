using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlankBullet : PoolableMono
{
    private Transform _playerTrm;

    private void Start()
    {
        _playerTrm = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        transform.position = _playerTrm.position;
    }

    //�ִϸ��̼� �̺�Ʈ�� ���
    public void ReturnObj()
    {
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {

    }

}
