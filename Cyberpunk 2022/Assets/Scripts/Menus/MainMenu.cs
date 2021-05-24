using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string _levelName;

    public void NewGame()
    {
        // loop throug file name and reset data only for that file name bcos
        // if only some present and some not checking if all is there wont work properly
        DataInitialization.Instance.ResetAllData();
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
// Here if player is playing for the 1st time then the Reset all data is not needed bcos
// 1) since the data.json is only created after knowing the player attibutes at runtime by gameManager and created when gameManager starts
//      it will cause error
// 2) No need to reset data as initially all data is resetted
// ELSE Reset All data is needed as player already played and all the needed data.json file is created
// TWO WAYS TO SOLVE
// 1) Check if already the json file is created and then only reset the data
// 2) Create all needed json data at starting of game at mainMenu itselt(hard, since attributes are needed to be setup) - not feasible
//      as we might need to save some data only based on gameplay