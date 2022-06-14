using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private Slider _playerHP;

    [SerializeField] private float _maxHP = 100;
    [SerializeField] private float _currentHP;

    private void Start()
    {
        _currentHP = _maxHP;
    }

    private void Update()
    {
        _playerHP.value = _currentHP / _maxHP;
    }
}
