using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class TitleSceneStart : BtnManager
{
    [SerializeField] private Image _howToPanel;

    private bool _isHowTo = false;

    private new void Start()
    {
        _howToPanel.gameObject.SetActive(false);
        _howToPanel.transform.localScale = new Vector3(0, 0, 0);
        _howToPanel.DOFade(0, 0);
    }

    private void Update()
    {
        BtnMove();
        if (_isHowTo == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Sequence sq = DOTween.Sequence();
                sq.Append(_howToPanel.transform.DOScale(new Vector3(0, 0, 0), 0.05f));
                sq.Join(_howToPanel.DOFade(0, 0.3f));
                sq.OnComplete(() =>
                {
                    _howToPanel.gameObject.SetActive(false);
                });
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //0번이 게임 종료 1번이 게임 설명 2번이 게임스타트
            if (_currentBtnNum == 2)
            {
                SceneManager.LoadScene("GamePlay");
            }
            if(_currentBtnNum == 1)
            {
                _howToPanel.gameObject.SetActive(true);
                Sequence sq = DOTween.Sequence();
                sq.Append(_howToPanel.transform.DOScale(new Vector3(1, 1, 1), 0.1f));
                sq.Join(_howToPanel.DOFade(0.65f, 0.3f));
                sq.OnComplete(()=>
                {
                    _isHowTo = true;
                });
            }
            if (_currentBtnNum == 0)
            {
                Application.Quit();
            }
        }
    }
}
