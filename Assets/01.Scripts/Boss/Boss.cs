using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using DG.Tweening;

public class Boss : MonoBehaviour
{
    public enum State
    {
        Init,
        HorseAttack,
        BreathAttack,
        BulletAttack,
        Death
    }

    [SerializeField] private GameObject _bossDeathEfx;
    [SerializeField] private List<Vector2> _fireAttackPos = new List<Vector2>();
    [SerializeField] private GameObject boss;

    private Animator _anim;
    StateMachine<State> fsm;

    private int _angleInterval = 30;
    private int _startAngle = 0;
    private int _endAngle = 360;

    private float randomPosY;

    private bool _isDeath = false;
    public bool IsDeath { get => _isDeath; set => _isDeath = value; }

    private void Start()
    {
        fsm = new StateMachine<State>(this);

        fsm.ChangeState(State.Init);

        _anim = GetComponent<Animator>();
        BossAttackMotion("AttackCoroutine");

        StartCoroutine(ChangeState());
    }

    public void ChangeBossDeath()
    {
        fsm.ChangeState(State.Death);
    }

    IEnumerator ChangeState()
    {
        yield return new WaitForSeconds(3f);
        while (_isDeath == false)
        {
            int num = Random.Range(1, 4);
            //랜덤 값 안겹치게 수정 필요함
            Debug.Log(num);

            if(num == 1)
            {
                fsm.ChangeState(State.HorseAttack);
            }
            if(num == 2)
            {
                fsm.ChangeState(State.BreathAttack);
            }
            if(num == 3)
            {
                fsm.ChangeState(State.BulletAttack);
            }
            yield return new WaitForSeconds(3f);
        }
    }

    private void Init_Enter()
    {
        Debug.Log("Ready FSM");
    }

    private void HorseAttack_Enter()
    {
        if(_isDeath != true)
            BossAttackMotion("FireSkullAttack");
    }

    private void BreathAttack_Enter()
    {
        if(_isDeath != true)
        {
            BossAttackMotion("AttackCoroutine");
            Invoke("BossBreathAttack", 3f);
        }
    }

    private void BulletAttack_Enter()
    {
        if(_isDeath != true)
        {
            BossAttackMotion("BulletAttackCoroutine");
        }
    }

    private void Death_Enter()
    {
        _isDeath = true;

        GameObject[] allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject item in allEnemy)
        {
            PoolManager.Instance.Push(item.GetComponent<PoolableMono>());
        }

        Sequence sq = DOTween.Sequence();

        _bossDeathEfx.SetActive(true);
        sq.Append(_bossDeathEfx.transform.DOScale(new Vector3(3f, 3f, 3f), 2.5f));
        sq.Append(_bossDeathEfx.transform.DOScale(new Vector3(0, 0, 0), 0.2f));
        sq.OnComplete(()=>
        {
            for (int fireAngle = _startAngle; fireAngle < _endAngle; fireAngle += 5)
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
        for(int i = 0; i < 4; i++)
        {
            FireAttack fireAttack = PoolManager.Instance.Pop("FireAttack") as FireAttack;
            fireAttack.transform.position = _fireAttackPos[i];
        }
    }

    private void BossAttackMotion(string name)
    {
        //StopAllCoroutines();
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

    IEnumerator BulletAttackCoroutine()
    {
        EnemySpawnManager.Instance.EnemySpawn(10);

        yield return new WaitForSeconds(2f);
        for(int i = 0; i < 3; i++)
        {
            Sequence sq = DOTween.Sequence();

            sq.Append(boss.transform.DOMoveY(boss.transform.position.y + 2, 0.3f));
            sq.Append(boss.transform.DOMoveY(boss.transform.position.y - 2, 0.2f));
            sq.OnComplete(()=>
            {
                CameraManager.Instance.ShakeCam(2, 0.4f);
                for (int fireAngle = _startAngle; fireAngle < _endAngle; fireAngle += _angleInterval)
                {
                    EnemyBullet enemyBullet = PoolManager.Instance.Pop("EnemyBullet") as EnemyBullet;
                    Vector2 dir = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                    enemyBullet.transform.right = dir;
                    enemyBullet.transform.position = transform.position;
                }
                boss.transform.DOMoveY(13, 0.5f);
            });
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator FireSkullAttack()
    {
        for(int i = 0; i < 3; i++)
        {
            randomPosY = Random.Range(-1f, 9f);
            FireSkull dangerMark = PoolManager.Instance.Pop("DangerMark") as FireSkull;
            dangerMark.transform.position = new Vector3(0, randomPosY, 0);

            yield return new WaitForSeconds(1f);
            PoolManager.Instance.Push(GameObject.Find("DangerMark").GetComponent<PoolableMono>());
            FireSkull fireSkullAttack = PoolManager.Instance.Pop("Fire_Skull") as FireSkull;
            fireSkullAttack.transform.position = i % 2 == 0 ? new Vector3(-16f, randomPosY) : new Vector3(16f, randomPosY);
            fireSkullAttack.transform.localScale = fireSkullAttack.transform.position.x > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
            yield return new WaitForSeconds(1f);

        }
    }
}
