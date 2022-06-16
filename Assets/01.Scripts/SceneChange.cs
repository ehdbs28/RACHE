using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneChange : MonoBehaviour
{
    public UnityEvent _event;

    public void SceneChanging(string name)
    {
        _event.Invoke();
        SceneManager.LoadScene(name);
    }
}
