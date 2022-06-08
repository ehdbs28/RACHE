using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] List<PoolableMono> _poolingList;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Multiple GameManager Instance is running!");
        }
        Instance = this;

        PoolManager.Instance = new PoolManager(transform);
        foreach(PoolableMono p in _poolingList)
        {
            PoolManager.Instance.CreatePool(p, 10);
        }

        GameObject timeController = new GameObject("TimeController");
        timeController.transform.parent = transform;
        TimeController.Instance = timeController.AddComponent<TimeController>();
    }
}
