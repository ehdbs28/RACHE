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

        _cmRigPerlin = _cmRigCam.GetComponent<CinemachineBasicMultiChannelPerlin>();

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
}
