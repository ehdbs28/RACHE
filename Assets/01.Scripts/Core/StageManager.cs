using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject _upCloseDoor;
    [SerializeField] GameObject _upOpenDoor;
    [SerializeField] GameObject _downCloseDoor;
    [SerializeField] GameObject _downOpenDoor;

    private Vector3 _initPos = new Vector3(0, -4.5f, 0);
    private RectTransform _blackImage;
    private Transform _playerTrm;

    private void Start()
    {
        _playerTrm = GameObject.Find("Player").GetComponent<Transform>();
        _blackImage = GameObject.Find("Canvas/StageSkipPanel").GetComponent<RectTransform>();

        StageStart();
    }

    private void Update()
    {
        if(_playerTrm.position.y >= 3.5f)
        {
            StageStart();
        }

        //test
        if (Input.GetKeyDown(KeyCode.T))
        {
            StageClear();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            StageStart();
        }
    }

    public void BlackScreen()
    {
        Image img = _blackImage.GetComponent<Image>();

        Sequence seq = DOTween.Sequence();
        seq.Append(img.DOFade(1, 0.5f));
        seq.Append(img.DOFade(0, 0.5f));
    }

    public void StageStart()
    {
        _upCloseDoor.SetActive(true);
        _upOpenDoor.SetActive(false);

        _downCloseDoor.SetActive(true);
        _downOpenDoor.SetActive(false);
        BlackScreen();

        _playerTrm.position = _initPos;
    }

    public void StageClear()
    {
        _upCloseDoor.SetActive(false);
        _upOpenDoor.SetActive(true);

        _downCloseDoor.SetActive(false);
        _downOpenDoor.SetActive(true);
    }
}
