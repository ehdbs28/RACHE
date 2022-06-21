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
        FireAttack,
        Death
    }

    [SerializeField] private GameObject _bossDeathEfx;
    [SerializeField] private GameObject _bossFireEffect;
    [SerializeField] private List<Vector2> _fireAttackPos = new List<Vector2>();
    [SerializeField] private GameObject boss;
    [SerializeField] private AudioSource _bossAudio;

    [Header("보스 오디오 클립")]
    [SerializeField] private AudioClip _breathClip;
    [SerializeField] private AudioClip _clearExplosionClip;
    [SerializeField] private AudioClip _flameClip;
    [SerializeField] private AudioClip _bulletClip;

    private Sequence sq;

    private Animator _anim;
    StateMachine<State> fsm;

    private int _angleInterval = 30;
    private int _startAngle = 0;
    private int _endAngle = 360;

    private float randomPosY;

    private bool _isDeath = false;
    public bool IsDeath { get => _isDeath; set => _isDeath = value; }

    private float _stateChangeDelay = 3f;
    public float StageteChangeDelay { get => _stateChangeDelay; set => _stateChangeDelay = value; }

    private void Start()
    {
        fsm = new StateMachine<State>(this);

        fsm.ChangeState(State.Init);

        _anim = GetComponent<Animator>();
        BossAttackMotion("AttackCoroutine");

        StartCoroutine(ChangeState());
    }

    public void KillSequence()
    {
        sq.Kill();
    }

    private void Update()
    {
        if (_isDeath)
        {
            GameObject[] allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject item in allEnemy)
            {
                PoolManager.Instance.Push(item.GetComponent<PoolableMono>());
            }
        }
    }

    public void ChangeBossDeath()
    {
        fsm.ChangeState(State.Death);
    }

    IEnumerator ChangeState()
    {
        Dictionary<string, int> diction = new Dictionary<string, int>();
        int num, randomNum;
        yield return new WaitUntil(()=> StageManager.Instance.IsGameStart == true);
        while (!_isDeath && StageManager.Instance.IsGameStart == true)
        {
            num = Random.Range(1, 5);

            if (diction.ContainsKey("1") && diction.ContainsKey("2") && diction.ContainsKey("3") && diction.ContainsKey("4"))
            {
                diction.Clear();
            }

            if (!diction.ContainsKey(num.ToString()))
            {
                randomNum = num;
                diction.Add(num.ToString(), num);
            }
            else
            {
                continue;
            }

            Debug.Log($"{randomNum} 번 패턴");

            if(randomNum == 1)
            {
                fsm.ChangeState(State.HorseAttack);
            }
            if(randomNum == 2)
            {
                fsm.ChangeState(State.BreathAttack);
            }
            if(randomNum == 3)
            {
                fsm.ChangeState(State.BulletAttack);
            }
            if(randomNum == 4)
            {
                fsm.ChangeState(State.FireAttack);
            }
            yield return new WaitForSeconds(_stateChangeDelay);
        }
    }

    private void Init_Enter()
    {
        Debug.Log("Ready FSM");
    }

    private void HorseAttack_Enter()
    {
        if (_isDeath != true)
            _stateChangeDelay = 4f;
            BossAttackMotion("FireSkullAttack");
    }

    private void BreathAttack_Enter()
    {
        if(_isDeath != true)
        {
            _stateChangeDelay = 4f;
            BossAttackMotion("AttackCoroutine");
            Invoke("BossBreathAttack", 3f);
        }
    }

    private void BulletAttack_Enter()
    {
        if(_isDeath != true)
        {
            _stateChangeDelay = 5f;
            BossAttackMotion("BulletAttackCoroutine");
        }
    }

    private void FireAttack_Enter()
    {
        if(_isDeath != true)
        {
            _stateChangeDelay = 5f;
            BossAttackMotion("FireAttackCoroutine");
        }
    }

    private void Death_Enter()
    {
        _isDeath = true;

        CameraManager.Instance.ShakeCam(40f, 0.5f);

        /*GameObject[] allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject item in allEnemy)
        {
            PoolManager.Instance.Push(item.GetComponent<PoolableMono>());
        }*/

        sq = DOTween.Sequence();

        _bossDeathEfx.SetActive(true);
        sq.Append(_bossDeathEfx.transform.DOScale(new Vector3(3f, 3f, 3f), 2.5f));
        _bossAudio.clip = _clearExplosionClip;
        _bossAudio.Play();
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
            StageManager.Instance.StageClear();
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
        StartCoroutine(name);
    }

    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSecondsRealtime(1f);
        _anim.SetBool("isAttack", true);
        _bossAudio.clip = _breathClip;
        _bossAudio.Play();
        yield return new WaitForSecondsRealtime(2f);
        _anim.SetBool("isAttack", false);
    }

    IEnumerator FireAttackCoroutine()
    {
        _bossFireEffect.SetActive(true);
        _bossAudio.clip = _flameClip;
        _bossAudio.Play();
        yield return new WaitUntil(() => _bossFireEffect.activeSelf == false);
        for (int i = 17; i >= -3; i--)
        {
            ExplosionAttack explosionAtk = PoolManager.Instance.Pop("ExplosionAttack") as ExplosionAttack;
            explosionAtk.transform.position = new Vector3(0, i);
            yield return new WaitForSeconds(0.3f);
            PoolManager.Instance.Push(FindObjectOfType<ExplosionAttack>());
        }
    }

    IEnumerator BulletAttackCoroutine()
    {
        yield return new WaitForSeconds(2f);
        for(int i = 0; i < 3; i++)
        {
            _bossAudio.clip = _bulletClip;
            _bossAudio.Play();

            sq = DOTween.Sequence();

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
