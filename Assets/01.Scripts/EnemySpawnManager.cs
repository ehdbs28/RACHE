using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    private int _maxEnemy = 5;

    private float x;
    private float y;

    private void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        for (int i = 0; i < _maxEnemy; i++)
        {
                yield return new WaitForSeconds(0.5f);
                x = Random.Range(-9f, 9f);
                y = Random.Range(0, 13f);  //포지션 수정
                Enemy enemy = PoolManager.Instance.Pop("Enemy") as Enemy;
                enemy.transform.position = new Vector3(x, y);
                enemy.transform.rotation = Quaternion.identity;
        }
    }
}
