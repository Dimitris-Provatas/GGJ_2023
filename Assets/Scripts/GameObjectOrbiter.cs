using UnityEngine;

public class GameObjectOrbiter : MonoBehaviour
{
  public GameObject targetObject;

  const float minDistance = 1.5f;
  const float maxDistance = 5f;

  [Range(minDistance, maxDistance)]
  public float distance = 4f;

  private GameObject localObject;
  private Vector2 prevMousePos;

  // Start is called before the first frame update
  void Start()
  {
    localObject = Instantiate(targetObject, Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, distance)), Quaternion.identity);
  }

  // Update is called once per frame
  void Update()
  {
    distance = Mathf.Clamp(distance + Input.mouseScrollDelta.y, minDistance, maxDistance);
    localObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, distance));

    if (Input.GetMouseButtonDown(0))
    {
      prevMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

      return;
    }
    if (Input.GetMouseButton(0))
    {
      Vector2 newMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

      if (newMousePos == prevMousePos)
      {
        return;
      }

      Vector2 mousePosDelta = prevMousePos - newMousePos;
      localObject.transform.Rotate(new Vector3(-mousePosDelta.y, mousePosDelta.x, 0f), Space.World);

      prevMousePos = newMousePos;
    }
  }
}
