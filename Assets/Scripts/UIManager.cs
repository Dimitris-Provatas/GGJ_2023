using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameStateText;

    [Header("Panels")]
    [SerializeField] private GameObject journalPanel;
    [SerializeField] private GameObject orbiterPanel;

    [Header("Tabs")]
    [SerializeField] private GameObject[] journalTabs;
    [SerializeField] private Button[] journalButtons;

    private GameObjectOrbiter orbiterInstance;

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
        journalPanel.SetActive(!journalPanel.activeInHierarchy);
    }

    /// <summary>
    /// Cycles through all tabs and enables only a given tab.
    /// </summary>
    /// <param name="tab">Tab to enable.</param>
    public void GoToTab(GameObject tab)
    {
        for(int i=0; i < journalTabs.Length; i++)
        {
            journalTabs[i].SetActive(journalTabs[i].Equals(tab));
            HandleButtonColorOnClick(journalButtons[i], journalTabs[i].Equals(tab));
        }
    }

    public void GoToJournal()
    {
        journalPanel.SetActive(true);
        orbiterPanel.SetActive(false);

        if(orbiterInstance != null)
        {
            Destroy(orbiterInstance);
        }
    }

    public void GoToOrbiter(GameObject go)
    {
        journalPanel.SetActive(false);
        orbiterPanel.SetActive(true);
        orbiterInstance = orbiterPanel.AddComponent<GameObjectOrbiter>();
        orbiterInstance.Init(go);
    }

    private void HandleButtonColorOnClick(Button button, bool currentlySelected)
    {
        button.GetComponent<TextMeshProUGUI>().color = currentlySelected ? new Color(1, 1, 1, 1f) : new Color(1,1,1,0.5f);
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
