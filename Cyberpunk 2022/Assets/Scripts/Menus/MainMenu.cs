using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string _levelName;

    public void NewGame()
    {
        SceneManager.LoadScene(_levelName);
    }
    private void Start()
    {
    }

    public void Quit()
    {
        Application.Quit();
    }
}
