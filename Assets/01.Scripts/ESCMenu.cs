using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ESCMenu : BtnManager
{
    [SerializeField] private TextMeshProUGUI _text;
    private void Update()
    {
        BtnMove();
        if(_currentBtnNum == 2)
        {
            _text.text = ">                <";
        }
        if(_currentBtnNum == 1)
        {
            _text.text = ">        <";
        }
        if(_currentBtnNum == 0)
        {
            _text.text = ">                                      <";
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_currentBtnNum == 2)
            {
                StageManager.Instance.IsESC = false;
            }

            if (_currentBtnNum == 1)
            {
                StageManager.Instance.IsESC = false;
                StageManager.Instance.IsSetting = true;
            }

            if(_currentBtnNum == 0)
            {
                SceneManager.LoadScene("GameTitle");
            }
        }
    }
}
