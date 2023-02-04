using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

  // Singleton
  public static GameManager instance;

  private void Start()
  {
    // Initialize Timer
    timerCurrent = timerStarting;
    timerActive = true;

    // Initialize Game State
    currentGameState = GameState.PLAYING;

    // Singleton
    instance = this;
  }

  private void Update()
  {
    TickTimer();
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
