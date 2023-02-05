using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class JournalController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI gameStateText;
    [SerializeField] private GameObject clueButtonPrefab;
    [SerializeField] private GameObject blackPanel;

    [Header("Panels")]
    public GameObject journalPanel;
    [SerializeField] private GameObject orbiterPanel;

    [Header("Tabs")]
    [SerializeField] private GameObject[] journalTabs;
    [SerializeField] private Button[] journalButtons;

    [SerializeField] private GameObject cluesButtonsGrid;

    private GameObjectOrbiter orbiterInstance;

    private List<GameObject> cluesList = new List<GameObject>();

    // Singleton
    public static JournalController instance;

    private void Start()
    {
        // Singleton
        instance = this;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab) && !PuzzleManager.instance.panel.activeInHierarchy)
        {
            ToggleJournalPanel();
        }

    }

    public void ToggleJournalPanel()
    {
        if (orbiterPanel.activeInHierarchy) return;

        journalPanel.SetActive(!journalPanel.activeInHierarchy);
        SoundManager.instance.ToggleThemePlaying();
        SoundManager.instance.PlaySoundEffect(journalPanel.activeInHierarchy ? "pageOpen" : "pageClose");

        FPSController.instance.canLook = !journalPanel.activeInHierarchy;
        FPSController.instance.lockMovement = journalPanel.activeInHierarchy;
        Cursor.visible = journalPanel.activeInHierarchy;
        Cursor.lockState = journalPanel.activeInHierarchy ? CursorLockMode.None : CursorLockMode.Locked;
    }

    /// <summary>
    /// Cycles through all tabs and enables only a given tab
    /// </summary>
    /// <param name="tab">Tab to enable.</param>
    public void GoToTab(GameObject tab)
    {
        for (int i = 0; i < journalTabs.Length; i++)
        {
            journalTabs[i].SetActive(journalTabs[i].Equals(tab));
            HandleButtonColorOnClick(journalButtons[i], journalTabs[i].Equals(tab));
            SoundManager.instance.PlaySoundEffect("pageOpen");
        }
    }

    public void GoToJournal()
    {
        journalPanel.SetActive(true);
        orbiterPanel.SetActive(false);

        if (orbiterInstance != null)
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

    public void ClueWasFound(GameObject clueObject)
    {
        cluesList.Add(clueObject);
        int lastIndex = cluesList.Count - 1;

        ClueController clueController = clueObject.GetComponent<ClueController>();
        // Destroy(clueObject);
        clueObject.SetActive(false);

        GameObject buttonToAdd = Instantiate(clueButtonPrefab);
        buttonToAdd.transform.SetParent(cluesButtonsGrid.transform);
        buttonToAdd.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = clueController.clueData.name;
        buttonToAdd.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = clueController.clueData.clue;

        buttonToAdd.GetComponent<Button>().onClick.AddListener(() => GoToOrbiter(cluesList[lastIndex]));
    }

    private void HandleButtonColorOnClick(Button button, bool currentlySelected)
    {
        button.GetComponent<TextMeshProUGUI>().color = currentlySelected ? new Color(1, 1, 1, 1f) : new Color(1, 1, 1, 0.5f);
    }

    public void ShowLoseText()
    {
        blackPanel.SetActive(true);
        SoundManager.instance.PlayLoseSound();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        gameStateText.enabled = true;
        gameStateText.text = "As you step outside the old house, you can't help but feel a sense of defeat. Despite your best efforts, the mystery of your unknown father remains unsolved. You didn't find the answers you sought, but the journey has changed you nonetheless. The cryptic clues, the forgotten memories, and the unknown twists and turns have left their mark, and you know that the search for answers will continue, even if the truth remains elusive for now.";
    }

    public void ShowWinText()
    {
        blackPanel.SetActive(true);
        gameStateText.enabled = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SoundManager.instance.PlayWinSound();
        gameStateText.text = "All the pieces have fallen into place, and the mystery that once seemed unsolvable has been solved. The journey was long and challenging, but the reward is worth it all.You stand in awe, surrounded by the memories of your father, feeling a sense of closure and belonging.You now know who you truly are and where you come from, and the world will never seem quite the same again.";
    }
}
