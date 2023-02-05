using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class InteractController : MonoBehaviour
{
  private Ray ray;
  private RaycastHit hitData;

  public GameObject noteInteractionTooltip;
  public GameObject clueInteractionTooltip;
  public GameObject drawerInteractionTooltip;
  public GameObject decisionInteractionTooltip;

  public TextMeshProUGUI pickedUpClueTooltip;
  public Animator pickedupClueTooltipAnimator;
  /* private bool lookingAtNote = false; */
  private bool lookingAtClue = false;
  private bool lookingAtDrawer = false;
  private bool lookingAtDecision = false;
  private bool hasOpenedNote = false;

  private void Update()
  {
    ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
    Physics.Raycast(ray, out hitData, 0.75f);

    Debug.DrawRay(ray.origin, ray.direction * 10);

    /* lookingAtNote = (hitData.collider?.CompareTag("Note")) ?? false; */
    lookingAtClue = (hitData.collider?.CompareTag("Clue")) ?? false;
    lookingAtDrawer = (hitData.collider?.CompareTag("Drawer")) ?? false;
    lookingAtDecision = (hitData.collider?.CompareTag("Decision")) ?? false;

    /* noteInteractionTooltip.SetActive(lookingAtNote); */
    clueInteractionTooltip.SetActive(lookingAtClue);
    drawerInteractionTooltip.SetActive(lookingAtDrawer);
    decisionInteractionTooltip.SetActive(lookingAtDecision);

    if (Input.GetKeyUp(KeyCode.F))
    {
      /* // is the player looking at a note? */
      /* if (lookingAtNote) */
      /* { */
      /*     // handle open/closing note */
      /*     if (!hasOpenedNote) */
      /*     { */
      /*         Debug.Log("You Opened the Note."); */
      /*         hasOpenedNote = true; */
      /*         // TODO: Add code that disables player movement. */
      /*     } */
      /*     else */
      /*     { */
      /*         Debug.Log("You Closed the Note."); */
      /*         hasOpenedNote = false; */
      /*         // TODO: Add code that re-enables player movement. */
      /*     } */
      /* } */

      // is the player looking at a clue?
      if (lookingAtClue)
      {
        JournalController.instance.ClueWasFound(hitData.collider.gameObject);
        PuzzleManager.instance.inventory.Add(hitData.collider.gameObject.GetComponent<ClueController>().clueData);
        pickedupClueTooltipAnimator.SetTrigger("ClueTrigger");
        SoundManager.instance.PlaySoundEffect("pickUp");
      }

      // is the player looking at a drawer?
      else if (lookingAtDrawer)
      {
        Debug.Log("Looking at drawer");

        Drawer d = hitData.collider.gameObject.GetComponent<Drawer>();

        if (d.opened)
          d.Close();
        else
          d.Open();
      }

      else if (lookingAtDecision)
      {
        Debug.Log("Looking at decision");
        PuzzleManager.instance.ToggleInteract();
      }
    }
  }
}