using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Clue")]
public class Clue : ScriptableObject
{
  public string name;
  public string clue;
  public Mesh mesh;
  public bool isFound;
}
