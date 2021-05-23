using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private string _mainMenu;
    [SerializeField] private string _levelName;             // Automatically get the current level for Retry


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Toggle();
        }
    }

    private void Toggle()
    {
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);

        if (_pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void PauseButton()
    {
        Toggle();
    }

    public void Continue()
    {
        Toggle();
    }

    public void Retry()
    {
        Toggle();
        SceneManager.LoadScene(_levelName);
    }

    public void MainMenu()
    {
        Toggle();
        SceneManager.LoadScene(_mainMenu);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
