using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

        _bossTrm = GameObject.Find("Player").GetComponent<Transform>();
        _playerTrm = GameObject.Find("DemonBoss").GetComponent<Transform>();
    }

    public void BossToPlayer()
    {
        StartCoroutine(BossToPlayerCoroutine());
    }

    IEnumerator BossToPlayerCoroutine()
    {
        _cmRigCam.Follow = _bossTrm;
        yield return new WaitForSeconds(5f);
        _cmRigCam.Follow = _playerTrm;
    }
}
