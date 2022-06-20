using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    [SerializeField] protected List<Transform> _btnTrms = new List<Transform>();

    protected int _currentBtnNum = 0;

    protected void Start()
    {
        transform.position = _btnTrms[_currentBtnNum].position;
    }

    protected void BtnMove()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _currentBtnNum--;
            _currentBtnNum = Mathf.Clamp(_currentBtnNum, 0, _btnTrms.Count - 1);
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _currentBtnNum++;
            _currentBtnNum = Mathf.Clamp(_currentBtnNum, 0, _btnTrms.Count - 1);
        }
        transform.position = _btnTrms[_currentBtnNum].position;
    }
}
