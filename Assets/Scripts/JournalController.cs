using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class JournalController : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI gameStateText;
  [SerializeField] private GameObject clueButtonPrefab;

  [Header("Panels")]
  [SerializeField] private GameObject journalPanel;
  [SerializeField] private GameObject orbiterPanel;

  [Header("Tabs")]
  [SerializeField] private GameObject[] journalTabs;
  [SerializeField] private Button[] journalButtons;

  private GameObject cluesButtonsGrid;

  private GameObjectOrbiter orbiterInstance;

  // Singleton
  public static JournalController instance;

  private void Start()
  {
    // Singleton
    instance = this;

    gameStateText = GameObject.Find("Canvas/Game State Text").GetComponent<TextMeshProUGUI>();

    cluesButtonsGrid = GameObject.Find("Journal Inventory Tab/Viewport/Content/Grid");

    journalPanel = GameObject.Find("Canvas/Journal Panel");
    journalPanel.SetActive(false);

    orbiterPanel = GameObject.Find("Canvas/Orbiter Panel");
    orbiterPanel.SetActive(false);
  }

  public void ToggleJournalPanel()
  {
    if (orbiterPanel.activeInHierarchy) return;

    journalPanel.SetActive(!journalPanel.activeInHierarchy);

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
    ClueController clueController = clueObject.GetComponent<ClueController>();

    cluesButtonsGrid.SetActive(true);
    GameObject buttonToAdd = Instantiate(clueButtonPrefab);
    buttonToAdd.transform.SetParent(cluesButtonsGrid.transform);
    buttonToAdd.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = clueController.clueData.name;
    buttonToAdd.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = clueController.clueData.clue;
    buttonToAdd.GetComponent<Button>().onClick.AddListener(() => GoToOrbiter(clueObject));
    cluesButtonsGrid.SetActive(false);
  }

  private void HandleButtonColorOnClick(Button button, bool currentlySelected)
  {
    button.GetComponent<TextMeshProUGUI>().color = currentlySelected ? new Color(1, 1, 1, 1f) : new Color(1, 1, 1, 0.5f);
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
