using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject SettingsMenuUI;
    public GameObject MainMenuUI;

    public void Back()
    {
        SettingsMenuUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }
}
