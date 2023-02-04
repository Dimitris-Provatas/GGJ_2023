using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("Suspects & Clues")]
    public Clue[] inventory;
    public Suspect suspectCorrect;
    public Suspect suspectSelected;

    [Header("Cameras")]
    public Camera fpsCamera;
    public Camera treeCamera;

    // Singleton
    public static GameManager instance;

    public void ChangeCameras()
    {
        fpsCamera.enabled = !treeCamera.enabled;
    }

    public bool AllCluesAreCorrect()
    {
        return inventory.Equals(suspectSelected.clues) && suspectCorrect.Equals(suspectSelected);
    }

    public void SelectSuspect(Suspect sus)
    {
        if (inventory.Length < 4)
        {
            Debug.Log("Can't choose a suspect just yet. You need to find more clues.");
        }
        else
        {
            sus = suspectSelected;
            Debug.Log("You have chosen " + sus.name + ". Are you sure?");
        }
    }

    public void ConfirmSelection()
    {
        if (AllCluesAreCorrect())
            GameManager.instance.TriggerWinCondition();
        else
            GameManager.instance.TriggerLoseCondition();
    }
}
