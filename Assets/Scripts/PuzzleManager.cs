using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PuzzleManager : MonoBehaviour
{
    [Header("Suspects & Clues")]
    public List<Clue> inventory;
    public Suspect[] allSuspects;
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
        suspectCorrect = allSuspects[Random.Range(0, allSuspects.Length)];
    }

    public void ToggleInteract()
    {
        panel.SetActive(!panel.activeInHierarchy);

        Cursor.visible = panel.activeInHierarchy;
        Cursor.lockState = panel.activeInHierarchy ? CursorLockMode.None : CursorLockMode.Locked;

        if (panel.activeInHierarchy)
        {
            FPSController.instance.FreezePlayer();
            SoundManager.instance.PlaySoundEffect("pageOpen");
        }
        else
        {
            FPSController.instance.UnfreezePlayer();
            SoundManager.instance.PlaySoundEffect("pageClose");
        }


        if(inventory.Count < 4)
            statusText.text = "Can't choose a suspect just yet. I need to find more clues.";
        else
            statusText.text = "Who can it be now?";

        buttons.SetActive(false);
    }

    public bool AllCluesAreCorrect()
    {
        return suspectCorrect.Equals(suspectSelected);
    }

    public void SelectSuspect(Suspect sus)
    {
        if (inventory.Count < 4)
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
