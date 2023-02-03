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

  // Update is called once per frame
  void Update()
  {

  }

  void OnCollisionEnter(Collision other)
  {
    Debug.Log("AAAAAAAAAAAAAAAAAAAAA");
  }
}
