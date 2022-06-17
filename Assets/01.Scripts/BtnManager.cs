using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void HowToPlay()
    {
        Debug.Log("HowToPlay");
    }
}
