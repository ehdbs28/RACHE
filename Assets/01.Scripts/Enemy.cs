using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PoolableMono
{
    [SerializeField] private float _enemySpeed = 5f;

    private SpriteRenderer _enemySprite;
    private bool isActive = false;

    private void OnEnable()
    {
        isActive = true;
        _enemySprite = GetComponent<SpriteRenderer>();
        
    }

    private void OnDisable()
    {
        isActive = false;
    }

    private void Update()
    {
        _enemySprite.flipX = transform.position.x > 0 ? true : false;

        if(isActive)
        {
            Debug.Log("isActive == true");
        }
    }

    public override void Reset()
    {

    }
}
