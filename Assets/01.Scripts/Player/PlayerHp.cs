using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHp : MonoBehaviour
{
    public static PlayerHp Instance;

    private Slider _playerHP;
    private Animator _anim;
    private float _maxHP = 100;
    private float _currentHP;

    private void Awake()
    {
        _anim = GameObject.Find("Player").GetComponent<Animator>();
        _playerHP = GameObject.Find("Canvas/PlayerHp").GetComponent<Slider>();
    }

    private void Start()
    {
        _currentHP = _maxHP;
    }

    private void Update()
    {
        _playerHP.value = _currentHP / _maxHP;
    }

    public void HPDown(float dmg)
    {
        _currentHP -= dmg;
        _playerHP.gameObject.transform.DOShakePosition(0.2f, 40, 10, 90, false, false);
        if(_currentHP <= 0)
        {
            _anim.SetBool("isDeath", true);
            StageManager.Instance.PlayerDie();
        }
    }
}
