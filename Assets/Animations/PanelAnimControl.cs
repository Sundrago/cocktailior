using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAnimControl : MonoBehaviour
{
    public GameObject anim_apnel;
    // Start is called before the first frame update

    public void PlayAnim(string code)
    {
        if (code == "new") anim_apnel.GetComponent<Animator>().SetTrigger("play_new");
        else if (code == "old") anim_apnel.GetComponent<Animator>().SetTrigger("play_old");
        else if (code == "left") anim_apnel.GetComponent<Animator>().SetTrigger("play_wiggle_left");
        else if (code == "right") anim_apnel.GetComponent<Animator>().SetTrigger("play_wiggle_right");
        else if (code == "scroll_left") anim_apnel.GetComponent<Animator>().SetTrigger("scroll_left");
        else if (code == "scroll_right") anim_apnel.GetComponent<Animator>().SetTrigger("scroll_right");
    }
}
