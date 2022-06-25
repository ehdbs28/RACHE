using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAttack : PoolableMono
{
    private IDamaged damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        damage = collision.GetComponent<IDamaged>();
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Damage());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        damage = collision.GetComponent<IDamaged>();

        StopAllCoroutines();
    }

    IEnumerator Damage()
    {
        if(damage != null)
        {
            damage.OnDamaged(8f);
        }
        yield return new WaitForSeconds(0.1f);
    }

    public override void Reset()
    {

    }
}
