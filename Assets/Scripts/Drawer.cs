using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public bool opened = false;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();   
    }

    public void Open()
    {
        opened = true;
        anim.SetTrigger("Open");
        //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -0.8f);
    }

    public void Close()
    {
        opened = false;
        anim.SetTrigger("Close");
        //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -0.45f);
    }
}
