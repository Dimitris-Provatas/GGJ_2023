using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class NoteController : MonoBehaviour
{
  private Ray ray;
  private RaycastHit hitData;

  public TextMeshProUGUI interactionTooltip;
  public bool lookingAtNote = false;
  public bool hasOpenedNote = false;

  void Start()
  {

  }

  void Update()
  {
    // dray a Ray. Looking for "Note" tags with it.
    ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    Physics.Raycast(ray, out hitData);

    // looking at a note is when the ray hits a GameObject with a tag of "Note".
    lookingAtNote = (hitData.collider?.CompareTag("Note")) ?? false;

    // Enable/Disable the text that informs the player about being able to read a note.
    interactionTooltip.enabled = lookingAtNote;

    // if the player presses the F Key while looking at a note...
    if (Input.GetKey(KeyCode.F) && lookingAtNote)
    {
      // if the player hasn't opened the note yet, open it.
      if (!hasOpenedNote)
      {
        Debug.Log("You Opened the Note.");
        hasOpenedNote = true;
        // TODO: Add code that disables player movement.
      }
      // otherwise, close it.
      else
      {
        Debug.Log("You Closed the Note.");
        hasOpenedNote = false;
        // TODO: Add code that re-enables player movement.
      }
    }
  }
}