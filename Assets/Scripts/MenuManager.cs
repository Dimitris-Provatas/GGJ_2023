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
  public Toggle fullScreenToggle;
  public Toggle vsyncToggle;

  public int currentRes;
  public bool fullScreen;

  private void Start()
  {
    Time.timeScale = 1;
    resolutions = Screen.resolutions;

    Cursor.lockState = CursorLockMode.None;
    Cursor.visible = true;

    GetAvailableResolutions();
  }

  public void GetAvailableResolutions()
  {
    resolutions = Screen.resolutions;

    foreach (Resolution resolution in resolutions)
    {
      resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resolution.ToString()));
    }
    
    resolutionDropdown.value = resolutionDropdown.options.Count-1;
  }

  public void BackToMenu()
  {
    mainMenu.SetActive(true);
    credits.SetActive(false);
    options.SetActive(false);
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
    UnityEngine.SceneManagement.SceneManager.LoadScene(1);
  }

  public void QuitGame()
  {
    Application.Quit();
  }

  public void ToggleFullscreen()
  {
    fullScreen = !fullScreen;

    if (fullScreen)
    {
      Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }
    else
    {
      Screen.fullScreenMode = FullScreenMode.Windowed;
    }
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
    Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height,
      Screen.fullScreen);
  }

  public void Update()
  {
    AudioListener.volume = volumeSlider.value;
  }

  public void ButtonClickSound()
  {
    SoundManager.instance.PlaySoundEffect("click");
  }

  public void ButtonHoverSound()
  {
    SoundManager.instance.PlaySoundEffect("hover");
  }
}