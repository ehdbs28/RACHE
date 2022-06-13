using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAttack : PoolableMono
{
    private Transform targetTrm;

    private float circleScale = 2f;
    private float iteration = 0;
    private float speed = 15f;
    private Vector3 targetDir;

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
    }

    public override void Reset()
    {
        
    }
}
