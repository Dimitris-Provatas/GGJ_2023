using UnityEngine;

public static class MouseInputWrapper
{
  private static Vector2 prevMousePos = Vector2.zero;

  public static float ScrollWheelDelta()
  {
    return Input.mouseScrollDelta.y;
  }

  public static Vector2 MousePositionDelta()
  {
    Vector2 newMousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    Vector2 result = prevMousePos - newMousePos;
    prevMousePos = newMousePos;

    return result;
  }
}