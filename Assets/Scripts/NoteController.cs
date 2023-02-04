using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hitData;
    
    public TextMeshProUGUI Text;
    public bool LookingAtNote = false;
    public bool HasOpenedNote = false;
    
    void Start()
    {
        
    }

    void Update()
    {
        // dray a Ray. Looking for "Note" tags with it.
        FireRay();
        
        // looking at a note is when the ray hits a GameObject with a tag of "Note".
        LookingAtNote = (hitData.collider != null && hitData.collider.CompareTag("Note"));

        // Enable/Disable the text that informs the player about being able to read a note.
        Text.enabled = LookingAtNote;

        if (Input.GetKey(KeyCode.F) && LookingAtNote && !HasOpenedNote)
        {
            Debug.Log("You Opened the Note.");
            HasOpenedNote = true;
        }
        else if (Input.GetKey(KeyCode.F) && LookingAtNote && HasOpenedNote)
        {
            Debug.Log("You Closed the Note.");
            HasOpenedNote = false;
        }
    }
    
    /// <summary>
    /// Draws a <see cref="Ray"/> relative to the player camera. Whatever it hits gets saved
    /// in the <see cref="hitData"/> variable.
    /// </summary>
    private void FireRay()
    {
        ray = Camera.main.ViewportPointToRay(new Vector3 (0.5f, 0.5f, 0));
        Physics.Raycast(ray, out hitData);
    }
}