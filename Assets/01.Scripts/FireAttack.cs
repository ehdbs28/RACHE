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
        //버그 픽스 하여야댐
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

    public override void Reset()
    {
        
    }
}
