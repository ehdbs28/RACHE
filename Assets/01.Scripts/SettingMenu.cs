using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingMenu : BtnManager
{
    [SerializeField] TextMeshProUGUI _effectSoundTxt;
    [SerializeField] TextMeshProUGUI _bgmSoundTxt;

    private int _effectSound = 0;
    private int _bgmSound = 0;
    private void Update()
    {
        BtnMove();
        #region ���� ��
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if(_currentBtnNum == 2)
            {
                _effectSound++;
                _effectSound = Mathf.Clamp(_effectSound, 0, 100);
            }
            if(_currentBtnNum == 1)
            {
                _bgmSound++;
                _bgmSound = Mathf.Clamp(_bgmSound, 0, 100);
            }
        }
        #endregion
        #region ���� �ٿ�
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (_currentBtnNum == 2)
            {
                _effectSound--;
                _effectSound = Mathf.Clamp(_effectSound, 0, 100);
            }
            if (_currentBtnNum == 1)
            {
                _bgmSound--;
                _bgmSound = Mathf.Clamp(_bgmSound, 0, 100);
            }
        }
        #endregion
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(_currentBtnNum == 0)
            {
                StageManager.Instance.IsSetting = false;
                StageManager.Instance.IsESC = true;
            }
        }
        _effectSoundTxt.text = $"ȿ���� : {_effectSound}";
        _bgmSoundTxt.text = $"����� : {_bgmSound}";
    }
}
