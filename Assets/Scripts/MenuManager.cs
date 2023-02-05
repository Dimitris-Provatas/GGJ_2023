using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject credits;
    public GameObject options;
    public Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    public Slider volumeSlider;

    public int currentRes;
    public bool fullScreen;

    private void Start()
    {
        Time.timeScale = 1;
        resolutions = Screen.resolutions;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        AudioListener.volume = 1;

        GetAvailableResolutions();
    }

    public void GetAvailableResolutions()
    {
        resolutions = Screen.resolutions;

        foreach (Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resolution.ToString()));
        }
    }

    public void BackToMenu()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);
        options.SetActive(false);
    }

    public void BackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Credits()
    {
        mainMenu.SetActive(false);
        credits.SetActive(true);
        options.SetActive(false);
    }

    public void Options()
    {
        mainMenu.SetActive(false);
        credits.SetActive(false);
        options.SetActive(true);
    }

    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(4);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void ToggleVSync()
    {
        if (QualitySettings.vSyncCount == 0)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;
    }

    public void ApplySettings()
    {
        Screen.SetResolution(resolutions[currentRes].width, resolutions[currentRes].height, fullScreen);
    }

    public void Update()
    {
        AudioListener.volume = volumeSlider.value;
    }
}