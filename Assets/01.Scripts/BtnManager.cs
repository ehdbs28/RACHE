using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    [SerializeField] private List<Transform> _btnTrms = new List<Transform>();

    private int _currentBtnNum = 0;

    private void Start()
    {
        transform.position = _btnTrms[_currentBtnNum].position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            _currentBtnNum--;
            _currentBtnNum = Mathf.Clamp(_currentBtnNum, 0, 1);
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _currentBtnNum++;
            _currentBtnNum = Mathf.Clamp(_currentBtnNum, 0, 1);
        }
        transform.position = _btnTrms[_currentBtnNum].position;

        if(Input.GetKeyDown(KeyCode.Return))
        {
            //0���� ���� ���� 1���� ���ӽ�ŸƮ
            if(_currentBtnNum == 1)
            {
                SceneManager.LoadScene("GamePlay");
            }
            if(_currentBtnNum == 0)
            {
                Application.Quit();
            }
        }
    }
}
