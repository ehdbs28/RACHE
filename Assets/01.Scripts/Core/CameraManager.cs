using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance = null;

    private CinemachineVirtualCamera _cmRigCam;

    private CinemachineBasicMultiChannelPerlin _cmRigPerlin = null;

    private CamRig _camrig;

    public void Start()
    {
        _cmRigCam = GameObject.Find("MainVcam").GetComponent<CinemachineVirtualCamera>();

        _cmRigPerlin = _cmRigCam.GetComponent<CinemachineBasicMultiChannelPerlin>();

        _camrig = GameObject.Find("MainVcam").GetComponent<CamRig>();
    }
}