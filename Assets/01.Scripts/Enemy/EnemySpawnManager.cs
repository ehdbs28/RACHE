using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance = null;


    private int _maxEnemy = 3;

    private float x;
    private float y;

    private void Start()
    {
        EnemySpawn(3);    
    }

    public void EnemySpawn(int maxEnemy)
    {
        if(StageManager.Instance.IsDeath == false)
        {
            StartCoroutine(EnemySpawnCoroutine(maxEnemy));
        }
    }

    IEnumerator EnemySpawnCoroutine(int maxEnemy)
    {
        while(StageManager.Instance.IsDeath != true)
        {
            for (int i = 0; i < maxEnemy; i++)
            {
                if (StageManager.Instance.IsDeath == true) { break; }
                yield return new WaitForSeconds(0.5f);
                x = Random.Range(-9f, 9f);
                y = Random.Range(0, 13f);  //포지션 수정
                Enemy enemy = PoolManager.Instance.Pop("Enemy") as Enemy;
                enemy.transform.position = new Vector3(x, y);
                enemy.transform.rotation = Quaternion.identity;
            }
            yield return new WaitForSeconds(5f);
        } 
    }
}
