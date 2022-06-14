using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverScene : MonoBehaviour
{
    [SerializeField] private List<GameObject> LightList = new List<GameObject>();
    [SerializeField] private Button _reStartBtn;

    private void Start()
    {
        StartCoroutine(LightCoroutine());
    }

    IEnumerator LightCoroutine()
    {
        for(int i = 0; i < LightList.Count; i++)
        {
            yield return new WaitForSeconds(0.3f);
            LightList[i].SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
