using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hitData;
    
    void Start()
    {
        
    }

    void Update()
    {
        FireRay();
        
        if (hitData.collider != null && hitData.collider.name.StartsWith("Note"))
        {
            Debug.Log("Found a note!");
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