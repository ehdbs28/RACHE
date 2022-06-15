using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public static SceneChange Instance;

    public void SceneChanging(string name)
    {
        SceneManager.LoadScene(name);
    }
}
