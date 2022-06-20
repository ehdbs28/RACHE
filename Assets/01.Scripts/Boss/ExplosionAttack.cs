using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAttack : PoolableMono
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HpManager.Instance.HPDown(7f);
        }    
    }

    public override void Reset()
    {

    }
}
