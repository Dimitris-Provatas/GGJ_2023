using UnityEngine;

public class ClueController : MonoBehaviour
{
    public Clue clueData;
    public Vector3 location;
    public MeshFilter meshFilter;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = clueData.mesh;
    }

    void OnTriggerEnter()
    {
        InventoryController.instance.AddClueToInventory(gameObject);
        Destroy(gameObject);
    }
}
