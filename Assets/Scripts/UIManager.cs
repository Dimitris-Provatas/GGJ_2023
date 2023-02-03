using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameStateText;
    [SerializeField] private GameObject panel;

    // Singleton
    public static UIManager instance;

    private void Start()
    {
        // Singleton
        instance = this;
    }

    private void Update()
    {
        // TODO just for testing, move this to interaction manager/character controller
        if (Input.GetKeyDown(KeyCode.J))
            ToggleJournalPanel();
    }

    public void ToggleJournalPanel()
    {
        panel.SetActive(!panel.activeInHierarchy);
    }

    public void ShowLoseText()
    {
        gameStateText.enabled = true;
        gameStateText.text = "INCORRECT";
    }

    public void ShowWinText()
    {
        gameStateText.enabled = true;
        gameStateText.text = "CORRECT";
    }
}
