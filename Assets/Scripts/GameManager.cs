using System.Collections;
using System.Collections.Generic;
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

  public TextMeshProUGUI pauseMenu;

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
    pauseMenu.enabled = false;

    // Singleton
    instance = this;
  }

  private void Update()
  {
    TickTimer();

    if (Input.GetKeyUp(KeyCode.P))
    {
      SwitchPause();
      Debug.Log("Esc pressed");
    }
      
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

  private void SwitchPause()
  {
    // swap current game state.
    if (currentGameState == GameState.PLAYING)
      currentGameState = GameState.PAUSED;
    else if (currentGameState == GameState.PAUSED)
      currentGameState = GameState.PLAYING;
    
    // The player will be able to look if the current game state is playing.
    Time.timeScale = currentGameState == GameState.PLAYING ? 1f : 0f;
    Cursor.visible = currentGameState == GameState.PAUSED;

    // Lock the look movement if the game is paused.
    FPSController.instance.canLook = currentGameState == GameState.PLAYING;

    // Show Paused if the game is paused.
    pauseMenu.enabled = currentGameState == GameState.PAUSED;
  }

  private void TriggerLoseCondition()
  {
    currentGameState = GameState.LOST;
    JournalController.instance.ShowLoseText();
  }

  private void TriggerWinCondition()
  {
    currentGameState = GameState.WON;
    JournalController.instance.ShowWinText();
  }
}
