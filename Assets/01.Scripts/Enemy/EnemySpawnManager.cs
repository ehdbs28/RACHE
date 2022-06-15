using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance = null;

    private Boss _bossScript;

    private int _maxEnemy = 5;

    private float x;
    private float y;

    private void Start()
    {
        _bossScript = GameObject.Find("DemonBoss").GetComponent<Boss>();

        EnemySpawn(_maxEnemy);
    }

    public void EnemySpawn(int maxEnemy)
    {
        if(_bossScript.IsDeath == false)
        {
            StartCoroutine(EnemySpawnCoroutine(maxEnemy));
        }
    }

    IEnumerator EnemySpawnCoroutine(int maxEnemy)
    {
        for (int i = 0; i < maxEnemy; i++)
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
