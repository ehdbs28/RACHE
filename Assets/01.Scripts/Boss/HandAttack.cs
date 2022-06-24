using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAttack : PoolableMono
{
    private Animator _playerAnim;

    private void Start()
    {
        _playerAnim = GameObject.Find("Player").GetComponent<Animator>();
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
        _playerAnim.SetTrigger("isDamaged");
        HpManager.Instance.HPDown(8f);
        yield return new WaitForSeconds(0.1f);
    }

    public override void Reset()
    {

    }
}
