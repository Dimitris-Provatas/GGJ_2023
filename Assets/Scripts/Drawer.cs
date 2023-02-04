using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public bool opened = false;
    
    public void Open()
    {
        opened = true;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -0.8f);
    }

    public void Close()
    {
        opened = false;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -0.45f);
    }
}
