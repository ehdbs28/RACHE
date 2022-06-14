using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireSkull : PoolableMono
{
    [SerializeField] private float speed = 10f;

    private SpriteRenderer sr;
    private Vector2 dir;

    private void Start()
    {
        if (gameObject.CompareTag("DangerMark"))
        {
            sr = GetComponent<SpriteRenderer>();

            DangerMarkFade();
        }
        dir = transform.position.x > 0 ? Vector3.right * -1 : Vector3.right;
    }

    private void Update()
    {
        if (gameObject.CompareTag("FireAttack"))
        {
            Invoke("AttackMove", 0.5f);

            if(Mathf.Abs(transform.position.x) > 20f)
            {
                PoolManager.Instance.Push(this);
            }
        }
    }

    private void AttackMove()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    private void DangerMarkFade()
    {
        Sequence sq = DOTween.Sequence();

        sq.Append(sr.DOFade(0.3f, 1f));
        sq.Append(sr.DOFade(0.8f, 1f));
        sq.AppendCallback(() =>
        {
            DangerMarkFade();
        });
    }

    public override void Reset()
    {
        
    }
}