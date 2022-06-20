using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;

public class SettingMenu : BtnManager
{
    [SerializeField] TextMeshProUGUI _effectSoundTxt;
    [SerializeField] TextMeshProUGUI _bgmSoundTxt;
    [SerializeField] AudioMixer masterMixer;

    private int _effectSound = 100;
    private int _bgmSound = 100;
    private void Update()
    {
        BtnMove();
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(_currentBtnNum == 0)
            {
                StageManager.Instance.IsSetting = false;
                StageManager.Instance.IsESC = true;
            }
        }
        masterMixer.SetFloat("BGM", Mathf.Lerp(-40, 0, _bgmSound));

        _effectSoundTxt.text = $"효과음 : {_effectSound}";
        _bgmSoundTxt.text = $"배경음 : {_bgmSound}";
    }

    private void LateUpdate()
    {
        #region 볼륨 업
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (_currentBtnNum == 2)
            {
                _effectSound++;
                _effectSound = Mathf.Clamp(_effectSound, 0, 100);
            }
            if (_currentBtnNum == 1)
            {
                _bgmSound++;
                _bgmSound = Mathf.Clamp(_bgmSound, 0, 100);
            }
        }
        #endregion
        #region 볼륨 다운
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
    }
}
