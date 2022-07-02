using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRotation : MonoBehaviour
{

    Transform t;
    public float fixedRotation;

    private void Start()
    {
        t = transform;

        PlacedObject placedObject = this.transform.parent.GetComponent<PlacedObject>();

        if (placedObject.nameA == "Apple")
        {
            fixedRotation = 180;
        }
        else if (placedObject.nameA == "Coconut")
        {
            fixedRotation = 170;
        }
        else if (placedObject.nameA == "Grapes")
        {
            fixedRotation = 180;
        }
        else if (placedObject.nameA == "Lemon")
        {
            fixedRotation = 170;
        }
        else if (placedObject.nameA == "Mini Grape")
        {
            fixedRotation = 0;
        }
        else if (placedObject.nameA == "Cherry")
        {
            fixedRotation = -180;
        }
        else
        {
            fixedRotation = 170;
        }
    }

    private void Update()
    {
        t.eulerAngles = new Vector3(t.eulerAngles.x, fixedRotation, t.eulerAngles.z);
    }

}
