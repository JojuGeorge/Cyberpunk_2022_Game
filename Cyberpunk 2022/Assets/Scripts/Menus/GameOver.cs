using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private string _mainMenu;
    [SerializeField] private string _thisLevelName;

    public void Retry()
    {
        SceneManager.LoadScene(_thisLevelName);
        this.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(_mainMenu);
        this.gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
