using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneStart : BtnManager
{
    private void Update()
    {
        BtnMove();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //0번이 게임 종료 1번이 게임스타트
            if (_currentBtnNum == 1)
            {
                SceneManager.LoadScene("GamePlay");
            }
            if (_currentBtnNum == 0)
            {
                Application.Quit();
            }
        }
    }
}
