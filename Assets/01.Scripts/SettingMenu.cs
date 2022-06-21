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

    private float _effectSound = 1;
    private float _bgmSound = 1;
    private void Update()
    {
        BtnMove();
        #region 볼륨 업
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (_currentBtnNum == 2)
            {
                _effectSound += 0.001f;
                _effectSound = Mathf.Clamp(_effectSound, 0, 1);
            }
            if (_currentBtnNum == 1)
            {
                _bgmSound += 0.001f;
                _bgmSound = Mathf.Clamp(_bgmSound, 0, 1);
            }
        }
        #endregion
        #region 볼륨 다운
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (_currentBtnNum == 2)
            {
                _effectSound -= 0.001f;
                _effectSound = Mathf.Clamp(_effectSound, 0, 1);
            }
            if (_currentBtnNum == 1)
            {
                _bgmSound -= 0.001f;
                _bgmSound = Mathf.Clamp(_bgmSound, 0, 1);
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
        if (_bgmSound == 0) { masterMixer.SetFloat("BGM", -80); }
        else { masterMixer.SetFloat("BGM", Mathf.Lerp(-40, 0, _bgmSound)); }

        if (_effectSound == 0) { masterMixer.SetFloat("Effect", -80); }
        else { masterMixer.SetFloat("Effect", Mathf.Lerp(-40, 0, _effectSound)); }

        _effectSoundTxt.text = $"효과음 : {System.Math.Round(_effectSound * 100, 1)}";
        _bgmSoundTxt.text = $"배경음 : {System.Math.Round(_bgmSound * 100, 1)}";
    }
}
