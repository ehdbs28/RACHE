using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : PoolableMono
{
    private Transform targetTrm = null;

    private float circleScale = 2f;
    private float iteration = 0;
    private float speed = 8f;
    private Vector3 targetDir = Vector3.zero;
    private IDamaged damage;

    private void Awake()
    {
        targetDir = Vector3.zero;
        targetTrm = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        Vector2 direction = targetDir + new Vector3(Mathf.Cos(iteration * Mathf.Deg2Rad), Mathf.Sin(iteration * Mathf.Deg2Rad));
        transform.Translate(direction.normalized * speed * (circleScale * Time.deltaTime));
        iteration++;
        if (iteration > 360) iteration -= 360;

        if(Mathf.Abs(transform.position.x) > 30)
        {
            PoolManager.Instance.Push(this);
        }
    }
    public void SetTargetPosition()
    {
        targetDir = targetTrm.position - transform.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        damage = collision.GetComponent<IDamaged>();

        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Damage());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopAllCoroutines();
    }

    IEnumerator Damage()
    {
        if(damage != null)
        {
            damage.OnDamaged(8f);
        }
        yield return new WaitForSeconds(0.1f);
    }

    public override void Reset()
    {
        
    }
}
