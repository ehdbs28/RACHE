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
            //0���� ���� ���� 1���� ���ӽ�ŸƮ
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
