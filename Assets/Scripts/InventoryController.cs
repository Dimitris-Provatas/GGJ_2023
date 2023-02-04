using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
  public List<GameObject> clues;

  public bool isInventoryOpen = false;

  // Singleton
  public static InventoryController instance;

  void Start()
  {
    // Singleton
    instance = this;
    isInventoryOpen = false;
  }

  void Update()
  {
    if (Input.GetKeyUp(KeyCode.Tab))
    {
      JournalController.instance.ToggleJournalPanel();
      isInventoryOpen = !isInventoryOpen;
    }
  }

  public void AddClueToInventory(GameObject clue)
  {
    clues.Add(clue);
    JournalController.instance.ClueWasFound(clue);
  }
}
