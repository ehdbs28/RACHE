using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private Transform _trmParent;

    private CinemachineVirtualCamera _cmRigCam;

    private CinemachineBasicMultiChannelPerlin _cmRigPerlin = null;

    private CamRig _camrig;

    public CameraManager(Transform trmParent)
    {
        _trmParent = trmParent;
    }

    public void Init()
    {
        _cmRigCam = GameObject.Find("MainVcam").GetComponent<CinemachineVirtualCamera>();

        _cmRigPerlin = _cmRigCam.GetComponent<CinemachineBasicMultiChannelPerlin>();

        _camrig = GameObject.Find("MainVcam").GetComponent<CamRig>();
    }
}
