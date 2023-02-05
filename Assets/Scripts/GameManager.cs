using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
  [Header("Timer")]
  public bool timerActive;
  public float timerStarting;
  public float timerCurrent;
  public float timerEnd;

  [Header("Game States")]
  public GameState currentGameState;
  public enum GameState { PLAYING, PAUSED, WON, LOST };

  public GameObject pauseMenu;

  // Singleton
  public static GameManager instance;

  private void Start()
  {
    // Initialize Timer
    timerCurrent = timerStarting;
    timerActive = true;

    // Initialize Game State
    currentGameState = GameState.PLAYING;

    // Remove Pause Label.
    pauseMenu.SetActive(false);

    // Singleton
    instance = this;
  }

  private void Update()
  {
    TickTimer();

    // TODO: Change this button to be ESC after testing.
    if (Input.GetKeyUp(KeyCode.P))
      SwitchPause();
  }

  /// <summary>
  /// Tick the timer if active and before its end.
  /// </summary>
  private void TickTimer()
  {
    if (timerActive && timerCurrent < timerEnd)
    {
      timerCurrent += Time.deltaTime;
    }
  }

  public void SwitchPause()
  {
    // swap current game state.
    if (currentGameState == GameState.PLAYING)
      currentGameState = GameState.PAUSED;
    else if (currentGameState == GameState.PAUSED)
      currentGameState = GameState.PLAYING;

    // Lock the look movement if the game is paused.
    FPSController.instance.canLook = currentGameState == GameState.PLAYING;

    // The player will be able to look if the current game state is playing.
    Time.timeScale = currentGameState == GameState.PLAYING ? 1f : 0f;
    Cursor.visible = currentGameState == GameState.PAUSED;
    Cursor.lockState = currentGameState == GameState.PAUSED ? CursorLockMode.None : CursorLockMode.Locked;

    // Show Paused if the game is paused.
    pauseMenu.SetActive(currentGameState == GameState.PAUSED);
  }

  public void GoToMainMenu()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene(0);
  }

  public void TriggerLoseCondition()
  {
    currentGameState = GameState.LOST;
    JournalController.instance.ShowLoseText();
  }

  public void TriggerWinCondition()
  {
    currentGameState = GameState.WON;
    JournalController.instance.ShowWinText();
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
