using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAttack : PoolableMono
{
    private Animator _playerAnim;

    private void Start()
    {
        _playerAnim = GameObject.Find("Player").GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerAnim.SetTrigger("isDamaged");
            HpManager.Instance.HPDown(7f);
        }    
    }

    public override void Reset()
    {

    }
}
