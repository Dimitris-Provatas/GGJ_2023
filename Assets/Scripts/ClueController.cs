using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(Renderer))]
public class ClueController : MonoBehaviour
{
    public Clue clueData;
    public Vector3 location;
    public MeshFilter meshFilter;
    public MeshRenderer renderer;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        renderer = GetComponent<MeshRenderer>();
        meshFilter.mesh = clueData.mesh;
        renderer.material = clueData.material;
    }
}
