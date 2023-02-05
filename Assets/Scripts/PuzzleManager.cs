using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    [Header("Suspects & Clues")]
    public Clue[] inventory;
    public Suspect suspectCorrect;
    public Suspect suspectSelected;

    [Header("Cameras")]
    public Camera fpsCamera;
    public Camera treeCamera;

    [Header("UI")]
    public GameObject panel;
    public GameObject buttons;
    public TextMeshProUGUI statusText;

    // Singleton
    public static PuzzleManager instance;
    private void Start()
    {
        instance = this;
    }

    public void ChangeCameras()
    {
        //fpsCamera.enabled = !treeCamera.enabled;
    }

    public void ToggleInteract()
    {
        panel.SetActive(!panel.activeInHierarchy);

        Cursor.visible = panel.activeInHierarchy;
        Cursor.lockState = panel.activeInHierarchy ? CursorLockMode.None : CursorLockMode.Locked;

        if (panel.activeInHierarchy)
            FPSController.instance.FreezePlayer();
        else
            FPSController.instance.UnfreezePlayer();

        if(inventory.Length < 4)
            statusText.text = "Can't choose a suspect just yet. I need to find more clues.";
        else
            statusText.text = "Who can it be now?";
    }

    public bool AllCluesAreCorrect()
    {
        //return inventory.Equals(suspectSelected.clues) && suspectCorrect.Equals(suspectSelected);
        return suspectCorrect.Equals(suspectSelected);
    }

    public void SelectSuspect(Suspect sus)
    {
        if (inventory.Length < 4)
        {
            statusText.text = "Can't choose a suspect just yet. I need to find more clues.";
        }
        else
        {
            suspectSelected = sus;
            statusText.text = "I'm thinking of " + sus.name + ". But am I sure?";
            // show buttons
            buttons.SetActive(true);
        }
    }

    public void CancelSuspect()
    {
        statusText.text = "Who can it be now?";
        suspectSelected = null;
        buttons.SetActive(false);
    }

    public void ConfirmSelection()
    {
        ToggleInteract();

        if (AllCluesAreCorrect())
            GameManager.instance.TriggerWinCondition();
        else
            GameManager.instance.TriggerLoseCondition();
    }
}
