using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDisable : MonoBehaviour
{

    Animator anim;

    private void Start()
    {
        anim = this.transform.GetComponent<Animator>();
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            this.gameObject.SetActive(false);
        }
    }
}
