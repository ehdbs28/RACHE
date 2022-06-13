using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FireSkull : PoolableMono
{
    [SerializeField] private float speed = 10f;

    private float positionY;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (gameObject.CompareTag("DangerMark"))
        {
            //여기 더 해야함
        }
    }

    IEnumerator MoveCoroutine()
    {
        positionY = Random.Range(-1f, 9f);

        transform.localScale = transform.position.x > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);

        FireSkull DangerMark = PoolManager.Instance.Pop("DangerMark") as FireSkull;
        yield return new WaitForSeconds(2f);
    }

    public override void Reset()
    {
        
    }
}
