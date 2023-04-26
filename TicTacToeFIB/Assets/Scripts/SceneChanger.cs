using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField]
    [Scene]
    private string _mainMenu;

    [SerializeField]
    [Scene]
    private string _twoPlayer;

    [SerializeField]
    [Scene]
    private string _vsComputer;

    private void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void MainMenu()
    {
        ChangeScene(_mainMenu);
    }

    public void TwoPlayer()
    {
        ChangeScene(_twoPlayer);
    }

    public void VsComputer()
    {
        ChangeScene(_vsComputer);
    }
}
