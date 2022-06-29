using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorDisable : MonoBehaviour
{

    Animator anim;
    bool i = false;

    private void Start()
    {
        anim = this.transform.GetComponent<Animator>();
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1)
        {
            i = true;
        }

        if ((anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) && (i = true))
        {
            anim.enabled = false;
            i = false;
            //anim.gameObject.transform.position = new Vector3(2, 2, 2);
        }
    }
}
