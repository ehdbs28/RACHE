using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PoolableMono
{
    [SerializeField] private float _enemySpeed = 5f;

    private SpriteRenderer _enemySprite;

    private void OnEnable()
    {
        _enemySprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _enemySprite.flipX = transform.position.x > 0 ? true : false;
    }

    public override void Reset()
    {

    }
}
