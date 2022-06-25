using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon_Damage : MonoBehaviour
{
    private int i;

    private void Awake()
    {
        i = 0;
    }

    private void FixedUpdate()
    {
        if (isActiveAndEnabled == true)
        {
            i += 1;
            //Debug.Log("+1");

            if (i == 50)
            {
                //Debug.Log("disabled");
                i = 0;
                gameObject.SetActive(false);
            }

        }
    }
}
