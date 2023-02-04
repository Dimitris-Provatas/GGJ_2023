using UnityEngine;

public class ClueController : MonoBehaviour
{
  public Clue clueData;
  public Vector3 location;
  public MeshFilter meshFilter;

  public InventoryController inventoryController;

  // Start is called before the first frame update
  void Start()
  {
    meshFilter = GetComponent<MeshFilter>();
    meshFilter.mesh = clueData.mesh;

    inventoryController = GameObject.Find("Inventory Controller").GetComponent<InventoryController>();
  }

  void OnTriggerEnter()
  {
    inventoryController.AddClueToInventory(gameObject);
    Destroy(gameObject);
  }
}
