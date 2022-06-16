using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScene : MonoBehaviour
{
    [SerializeField] private List<GameObject> LightList = new List<GameObject>();
    [SerializeField] private Image _reStart;
    [SerializeField] private Button _reStartBtn;

    private void Start()
    {
        _reStartBtn.interactable = false;
        _reStart.CrossFadeAlpha(0, 0f, true);
        StartCoroutine(BtnFade());
        StartCoroutine(LightCoroutine());
    }

    IEnumerator BtnFade()
    {
        yield return new WaitForSeconds(2.5f);
        _reStartBtn.interactable = true;
        while (true)
        {
            _reStart.CrossFadeAlpha(1, 1f, true);
            yield return new WaitForSeconds(0.5f);
            _reStart.CrossFadeAlpha(0, 1f, true);
            yield return new WaitForSeconds(0.5f);
        }
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
