using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;

public class Boss : MonoBehaviour
{
    public enum State
    {
        Init,
        HorseAttack,
        BreathAttack,
        BulletAttack
    }

    private Animator _anim;
    StateMachine<State> fsm;

    private void Start()
    {
        fsm = new StateMachine<State>(this);

        fsm.ChangeState(State.Init);

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
        yield return new WaitForSecondsRealtime(0.8f);
        BossBreath bossBreath = PoolManager.Instance.Pop("BossBreath") as BossBreath;
        bossBreath.transform.position = new Vector2(-3.85f, 9.73f);
        yield return new WaitForSecondsRealtime(1.2f);
        _anim.SetBool("isAttack", false);
        PoolManager.Instance.Push(GameObject.Find("Manager/BossBreath").GetComponent<PoolableMono>());
    }
}
