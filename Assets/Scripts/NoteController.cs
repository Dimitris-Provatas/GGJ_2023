using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hitData;
    
    public TextMeshPro Text;
    public bool LookingAtNote = false;
    
    void Start()
    {
        
    }

    void Update()
    {
        FireRay();
        
        if (hitData.collider != null && hitData.collider.CompareTag("Note"))
        {
            Debug.Log("Found a note!");
            LookingAtNote = true;
        }
        else
        {
            LookingAtNote = false;
        }

        if (LookingAtNote)
        {
            
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