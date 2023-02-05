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
        

        if(gameObject.name.Contains("drawer"))
            SoundManager.instance.PlaySoundEffect("drawOpen");
        else 
            SoundManager.instance.PlaySoundEffect("openDrawer");

    }

    public void Close()
    {
        opened = false;
        anim.SetTrigger("Close");

        if (gameObject.name.Contains("drawer"))
            SoundManager.instance.PlaySoundEffect("drawClose");
        else

        SoundManager.instance.PlaySoundEffect("closeDrawer");
        //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -0.45f);
    }
}
