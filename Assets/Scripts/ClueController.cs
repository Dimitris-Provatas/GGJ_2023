using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(Renderer))]
public class ClueController : MonoBehaviour
{
    public Clue clueData;
    public Vector3 location;
    public MeshFilter meshFilter;
    public Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        renderer = GetComponent<Renderer>();
        meshFilter.mesh = clueData.mesh;
        renderer.material = clueData.material;
    }
}
