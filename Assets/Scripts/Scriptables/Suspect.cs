using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objects/Suspect")]
public class Suspect : ScriptableObject
{
  public string name;
  public Sprite sprite;
  public Clue[] clues;
}
