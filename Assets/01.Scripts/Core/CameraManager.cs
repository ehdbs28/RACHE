using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance = null;

    private CinemachineVirtualCamera _cmRigCam;

    private CinemachineBasicMultiChannelPerlin _cmRigPerlin = null;

    private Transform _bossTrm;
    private Transform _playerTrm;

    public void Start()
    {
        _cmRigCam = GameObject.Find("MainVcam").GetComponent<CinemachineVirtualCamera>();

        _cmRigPerlin = _cmRigCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        _bossTrm = GameObject.Find("DemonBoss").GetComponent<Transform>();
        _playerTrm = GameObject.Find("Player").GetComponent<Transform>();
    }

    public void BossToPlayer(Action OnComplete = null)
    {
        StartCoroutine(BossToPlayerCoroutine(OnComplete));
    }

    IEnumerator BossToPlayerCoroutine(Action OnComplete = null)
    {
        yield return new WaitForSecondsRealtime(1f);
        _cmRigCam.Follow = _bossTrm;
        yield return new WaitForSecondsRealtime(2f);
        _cmRigCam.Follow = _playerTrm;
        OnComplete?.Invoke();
    }

    public void ShakeCam(float intensity, float time)
    {
        if (_cmRigPerlin == null) return;
        StopAllCoroutines();
        StartCoroutine(ShakeCamCoroutine(intensity, time));
    }

    IEnumerator ShakeCamCoroutine(float intensity, float endTime)
    {
        _cmRigPerlin.m_AmplitudeGain = intensity;

        float currentTime = 0f;
        while (currentTime < endTime)
        {
            yield return new WaitForEndOfFrame();
            if (_cmRigPerlin == null) break;

            _cmRigPerlin.m_AmplitudeGain = Mathf.Lerp(intensity, 0, currentTime / endTime);
            currentTime += Time.deltaTime;
        }
        if (_cmRigPerlin != null)
            _cmRigPerlin.m_AmplitudeGain = 0;
    }
}
