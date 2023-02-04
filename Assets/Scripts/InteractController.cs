using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class InteractController : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hitData;

    public GameObject noteInteractionTooltip;
    public GameObject clueInteractionTooltip;

    public TextMeshProUGUI pickedUpClueTooltip;
    public Animator pickedupClueTooltipAnimator;
    private bool lookingAtNote = false;
    private bool lookingAtClue = false;
    private bool hasOpenedNote = false;

    void Update()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Physics.Raycast(ray, out hitData, 0.75f);

        lookingAtNote = (hitData.collider?.CompareTag("Note")) ?? false;
        lookingAtClue = (hitData.collider?.CompareTag("Clue")) ?? false;

        noteInteractionTooltip.SetActive(lookingAtNote);
        clueInteractionTooltip.SetActive(lookingAtClue);

        if (Input.GetKeyUp(KeyCode.F))
        {
            if (lookingAtNote)
            {
                if (!hasOpenedNote)
                {
                    Debug.Log("You Opened the Note.");
                    hasOpenedNote = true;
                    // TODO: Add code that disables player movement.
                }
                else
                {
                    Debug.Log("You Closed the Note.");
                    hasOpenedNote = false;
                    // TODO: Add code that re-enables player movement.
                }
            }
            else if (lookingAtClue)
            {
                JournalController.instance.ClueWasFound(hitData.collider.gameObject);
                pickedupClueTooltipAnimator.SetTrigger("ClueTrigger");
            }
        }
    }
}