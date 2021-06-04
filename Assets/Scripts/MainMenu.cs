using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject SettingsMenuUI;
    public GameObject MainMenuUI;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Settings()
    {
        MainMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
