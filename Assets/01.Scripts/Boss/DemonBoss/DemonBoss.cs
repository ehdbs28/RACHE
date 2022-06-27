using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using DG.Tweening;

public class DemonBoss : MonoBehaviour
{
    [SerializeField] private GameObject _dangerImage;
    [SerializeField] private Transform _playerTrm;

    private int _startAngle = 0;
    private int _endAngle = 360;
    private int _angleInterval = 30;

    private Vector3 _nextPos;
    private Sequence sq;

    private void Start()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        sq = DOTween.Sequence();
        while (true)
        {
            yield return new WaitForSeconds(2f);
            _nextPos = _playerTrm.position;
            GameObject danger = Instantiate(_dangerImage);
            danger.transform.position = _nextPos;
            yield return new WaitForSeconds(1f);
            Destroy(danger.gameObject);
            sq.Append(transform.DOMove(_nextPos, 1f));
            sq.OnComplete(()=>
            {
                CameraManager.Instance.ShakeCam(2, 0.4f);
                for (int fireAngle = _startAngle; fireAngle < _endAngle; fireAngle += _angleInterval)
                {
                    EnemyBullet enemyBullet = PoolManager.Instance.Pop("EnemyBullet") as EnemyBullet;
                    Vector2 dir = new Vector2(Mathf.Cos(fireAngle * Mathf.Deg2Rad), Mathf.Sin(fireAngle * Mathf.Deg2Rad));
                    enemyBullet.transform.right = dir;
                    enemyBullet.transform.position = transform.position;
                }
            });
        }
    }
}
