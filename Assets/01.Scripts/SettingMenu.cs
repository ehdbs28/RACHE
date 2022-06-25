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

    private float _effectSound = 10;
    private float _bgmSound = 10;
    private void Update()
    {
        BtnMove();
        #region 볼륨 업
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (_currentBtnNum == 2)
            {
                _effectSound++;
                _effectSound = Mathf.Clamp(_effectSound, 0, 10);
            }
            if (_currentBtnNum == 1)
            {
                _bgmSound++;
                _bgmSound = Mathf.Clamp(_bgmSound, 0, 10);
            }
        }
        #endregion
        #region 볼륨 다운
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (_currentBtnNum == 2)
            {
                _effectSound--;
                _effectSound = Mathf.Clamp(_effectSound, 0, 10);
            }
            if (_currentBtnNum == 1)
            {
                _bgmSound--;
                _bgmSound = Mathf.Clamp(_bgmSound, 0, 10);
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
        else { masterMixer.SetFloat("BGM", Mathf.Lerp(-40, 0, _bgmSound / 10)); }

        if (_effectSound == 0) { masterMixer.SetFloat("Effect", -80); }
        else { masterMixer.SetFloat("Effect", Mathf.Lerp(-40, 0, _effectSound / 10)); }

        _effectSoundTxt.text = $"효과음 : {_effectSound}";
        _bgmSoundTxt.text = $"배경음 : {_bgmSound}";
    }
}
