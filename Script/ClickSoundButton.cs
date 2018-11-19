using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickSoundButton : MonoBehaviour
{

    public AudioSource source;
    public AudioClip hover;
    public AudioClip click;
    

    public void Hoversound()
    {

        source.PlayOneShot(hover);

    }

    public void Clicksound()
    {
        source.PlayOneShot(click);
    }
}
