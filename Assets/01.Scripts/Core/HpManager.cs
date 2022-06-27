using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HpManager : MonoBehaviour
{
    public static HpManager Instance;

    private Slider _playerHP;
    private Slider _bossHp;
    private Animator _anim;

    private float _bossMaxHP = 100;
    private float _bossCurrentHP;

    private float _maxHP = 100;
    public float MaxHP { get => _maxHP; }
    private float _currentHP;
    public float CurrentHP { get => _currentHP; }

    private void Awake()
    {
        _anim = GameObject.Find("Player").GetComponent<Animator>();
        _playerHP = GameObject.Find("Canvas/PlayerHp").GetComponent<Slider>();
        _bossHp = GameObject.Find("Canvas/BossHp").GetComponent<Slider>();
    }

    private void Start()
    {

        _currentHP = _maxHP;
        _bossCurrentHP = _bossMaxHP;
    }

    private void Update()
    {
        _bossHp.value = _bossCurrentHP / _bossMaxHP;
        _playerHP.value = _currentHP / _maxHP;
    }

    public void BossHpDown(float dmg)
    {
        if(_bossCurrentHP > 0)
        {
            _bossCurrentHP -= dmg;
            _bossHp.gameObject.transform.DOShakePosition(0.1f, 15, 10, 90, false, false);
        }
        
        if(_bossCurrentHP <= 0)
        {
            StageManager.Instance.IsDeath = true;
        }
    }

    public void HPDown(float dmg)
    {
        _currentHP -= dmg;
        _playerHP.gameObject.transform.DOShakePosition(0.1f, 15, 10, 90, false, false);
        if(_currentHP <= 0)
        {
            _anim.SetBool("isDeath", true);
            StageManager.Instance.PlayerDie();
        }
    }
}
