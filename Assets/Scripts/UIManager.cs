using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameStateText;
    [SerializeField] private GameObject panel;

    [Header("Tabs")]
    [SerializeField] private GameObject[] tabs;
    [SerializeField] private Button[] buttons;

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

    /// <summary>
    /// Cycles through all tabs and enables only a given tab.
    /// </summary>
    /// <param name="tab">Tab to enable.</param>
    public void GoToTab(GameObject tab)
    {
        for(int i=0; i < tabs.Length; i++)
        {
            tabs[i].SetActive(tabs[i].Equals(tab));
            HandleButtonColorOnClick(buttons[i], tabs[i].Equals(tab));
        }
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

    private void HandleButtonColorOnClick(Button button, bool currentlySelected)
    {
        button.GetComponent<TextMeshProUGUI>().color = currentlySelected ? new Color(1, 1, 1, 0.5f) : new Color(1,1,1,1f);
    }
}
