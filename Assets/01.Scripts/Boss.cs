using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        BossAttackMotion();
    }

    private void BossAttackMotion()
    {
        StopAllCoroutines();
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSecondsRealtime(1f);
        _anim.SetBool("isAttack", true);
        yield return new WaitForSecondsRealtime(2f);
        _anim.SetBool("isAttack", false);
    }
}
