using UnityEngine;

public class SuspectController : MonoBehaviour
{
  public Suspect suspectData;
  public Vector3 location;
  public SpriteRenderer renderer;

  public bool isTarget;

  // Start is called before the first frame update
  void Start()
  {
    renderer = GetComponent<SpriteRenderer>();
    renderer.sprite = suspectData.sprite;
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
