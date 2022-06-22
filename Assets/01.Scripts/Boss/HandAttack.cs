using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAttack : PoolableMono
{
    private void Start()
    {
        //Invoke("DestroyThis", 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Damage());
        }
    }

    /*public void DestroyThis()
    {
        PoolManager.Instance.Push(this);
    }*/

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopAllCoroutines();
    }

    IEnumerator Damage()
    {
        HpManager.Instance.HPDown(8f);
        yield return new WaitForSeconds(0.1f);
    }

    public override void Reset()
    {

    }
}
