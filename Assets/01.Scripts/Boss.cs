using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using DG.Tweening;

public class Boss : MonoBehaviour
{
    public enum State
    {
        HorseAttack,
        BreathAttack,
        BulletAttack,
        Death
    }

    [SerializeField] private GameObject _bossDeathEfx;

    private Animator _anim;
    StateMachine<State> fsm;

    private int _angleInterval = 5;
    private int _startAngle = 0;
    private int _endAngle = 360;

    private void Start()
    {
        fsm = new StateMachine<State>(this);

        fsm.ChangeState(State.HorseAttack);

        _anim = GetComponent<Animator>();
        BossAttackMotion("AttackCoroutine");
    }

    private void Update()
    {
        //test
        if (Input.GetKeyDown(KeyCode.M))
        {
            fsm.ChangeState(State.Death);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            fsm.ChangeState(State.BreathAttack);
        }
    }

    private void HorseAttack_Enter()
    {
        Debug.Log("HorseAttack");
    }

    private void BreathAttack_Enter()
    {
        BossAttackMotion("AttackCoroutine");
        Invoke("BossBreathAttack", 3f);
    }

    private void Death_Enter()
    {
        Sequence sq = DOTween.Sequence();

        _bossDeathEfx.SetActive(true);
        sq.Append(_bossDeathEfx.transform.DOScale(new Vector3(3f, 3f, 3f), 2.5f));
        sq.Append(_bossDeathEfx.transform.DOScale(new Vector3(0, 0, 0), 0.2f));
        sq.OnComplete(()=>
        {
            for (int fireAngle = _startAngle; fireAngle < _endAngle; fireAngle += _angleInterval)
            {
                EnemyBullet enemyBullet = PoolManager.Instance.Pop("EnemyBullet") as EnemyBullet;
                Vector2 dir = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                enemyBullet.transform.right = dir;
                enemyBullet.transform.position = transform.position;
            }
            gameObject.SetActive(false);
        });

    }

    private void BossBreathAttack()
    {
        FireAttack fireAttack = PoolManager.Instance.Pop("FireAttack") as FireAttack;
        fireAttack.transform.position = new Vector3(10, 20);

        FireAttack fireAttack2 = PoolManager.Instance.Pop("FireAttack") as FireAttack;
        fireAttack2.transform.position = new Vector3(-10, 20);
    }

    private void BossAttackMotion(string name)
    {
        StopAllCoroutines();
        StartCoroutine(name);
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
