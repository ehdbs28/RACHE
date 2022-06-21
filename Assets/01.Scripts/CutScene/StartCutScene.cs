using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class StartCutScene : MonoBehaviour
{
    [SerializeField] private RectTransform _blackPanelUp;
    [SerializeField] private RectTransform _blackPanelDown;
    [SerializeField] private Text _skipTxt;
    [SerializeField] private Text _talkTxt;

    [SerializeField] private GameObject _blackHole;

    private Sequence sq;

    private void Start()
    {
        _talkTxt.text = "";

        sq = DOTween.Sequence();

        sq.Append(_blackPanelUp.DOAnchorPos(new Vector2(0, 980), 3f));
        sq.Join(_blackPanelDown.DOAnchorPos(new Vector2(0, -980), 3f));
        sq.OnComplete(()=>
        {
            CutSceneText();
            SkipText();
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneChange();
        }
    }

    private void SceneChange()
    {
        _talkTxt.text = "";

        sq = DOTween.Sequence();

        sq.Append(_blackPanelUp.DOAnchorPos(new Vector2(0, 540), 2f));
        sq.Join(_blackPanelDown.DOAnchorPos(new Vector2(0, -540), 2f));
        sq.OnComplete(()=>
        {
            sq.Kill();

            SceneManager.LoadScene("GameTitle");
        }); 
    }

    private void SkipText()
    {
        sq = DOTween.Sequence();

        sq.Append(_skipTxt.DOFade(0, 1f));
        sq.Append(_skipTxt.DOFade(1, 1f));
        sq.AppendCallback(()=>
        {
            SkipText();
        });
    }

    private void CutSceneText()
    {
        sq = DOTween.Sequence();

        sq.Append(_talkTxt.DOText("(´ëÃæ ¿ë»ç°¡ ¸¶¿ÕÀ» ¹«Âî¸£°í ±Õ¿­ÀÌ ±úÁü)", 2f));
        sq.Append(_talkTxt.DOText("(¸Þµ¥Å¸½Ã~¸Þµ¥Å¸½Ã~)", 0.5f));
        sq.Append(_blackHole.transform.DORotate(new Vector3(0, 0, 720f), 2f, RotateMode.FastBeyond360));
        sq.Append(_blackHole.transform.DOScale(new Vector3(0, 0, 0), 1f));

        sq.OnComplete(()=>
        {
            SceneChange();
        });
    }
}
