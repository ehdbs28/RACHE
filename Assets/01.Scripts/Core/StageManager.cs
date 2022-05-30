using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject _upCloseDoor;
    [SerializeField] GameObject _upOpenDoor;
    [SerializeField] GameObject _downCloseDoor;
    [SerializeField] GameObject _downOpenDoor;

    private void Update()
    {
        //test
        if (Input.GetKeyDown(KeyCode.T))
        {
            StageClear();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            StageStart();
        }
    }

    public void StageStart()
    {
        _upCloseDoor.SetActive(true);
        _upOpenDoor.SetActive(false);

        _downCloseDoor.SetActive(true);
        _downOpenDoor.SetActive(false);
    }

    public void StageClear()
    {
        _upCloseDoor.SetActive(false);
        _upOpenDoor.SetActive(true);

        _downCloseDoor.SetActive(false);
        _downOpenDoor.SetActive(true);
    }
}
