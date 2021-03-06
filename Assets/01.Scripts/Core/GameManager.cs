using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] List<PoolableMono> _poolingList;

    private void Awake()
    {
        #region 인스턴스 생성
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManager Instance is running!");
        }
        Instance = this;

        PoolManager.Instance = new PoolManager(transform);
        foreach(PoolableMono p in _poolingList)
        {
            PoolManager.Instance.CreatePool(p, 10);
        }

        TimeController.Instance = gameObject.AddComponent<TimeController>();

        CameraManager.Instance = gameObject.AddComponent<CameraManager>();

        EnemySpawnManager.Instance = gameObject.AddComponent<EnemySpawnManager>();

        HpManager.Instance = gameObject.AddComponent<HpManager>();

        ScoreManager.Instance = gameObject.AddComponent<ScoreManager>();
        #endregion
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("GamePlay");
        }
    }
}
