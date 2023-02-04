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
    float scrollDelta = MouseInputWrapper.ScrollWheelDelta();
    if (scrollDelta != 0f)
    {
      distance = Mathf.Clamp(distance + scrollDelta, minDistance, maxDistance);
      localObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, distance));
    }

    if (Input.GetMouseButton(0))
    {
      Vector2 mousePosDelta = MouseInputWrapper.MousePositionDelta();

      if (mousePosDelta == Vector2.zero) return;

      localObject.transform.Rotate(new Vector3(-mousePosDelta.y, mousePosDelta.x, 0f), Space.World);
    }
  }
}
