using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : PoolableMono
{
    private Transform targetTrm = null;

    private float circleScale = 2f;
    private float iteration = 0;
    private float speed = 10f;
    private Vector3 targetDir = Vector3.zero;

    private void Start()
    {
        targetTrm = GameObject.Find("Player").GetComponent<Transform>();
        targetDir = targetTrm.position - transform.position;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        HpManager.Instance.HPDown(3f);
        yield return new WaitForSeconds(0.1f);
    }

    public override void Reset()
    {
        
    }
}
