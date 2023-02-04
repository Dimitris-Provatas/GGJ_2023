using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
  public InventoryController instance;
  public List<GameObject> clues;

  public bool isInventoryOpen = false;

  void Start()
  {
    instance = this;

    isInventoryOpen = false;
  }

  void Update()
  {
    if (Input.GetKeyUp(KeyCode.Tab))
    {
      ToggleInventory();
    }
  }

  void ToggleInventory()
  {
    if (isInventoryOpen) { } else { }

    isInventoryOpen = !isInventoryOpen;
  }

  public void AddClueToInventory(GameObject clue)
  {
    clues.Add(clue);
  }
}
