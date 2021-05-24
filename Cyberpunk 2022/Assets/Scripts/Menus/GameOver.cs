using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private string _mainMenu;
    //[SerializeField] private string _thisLevelName;

    public void Retry()
    {
        // for temp reset all data, and not resetting to data before this level
        DataInitialization.Instance.ResetAllData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
