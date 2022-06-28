using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeIcon : MonoBehaviour
{
    public InputManager inputManager;
    public Sprite shieldNormal;
    public Sprite shieldArmor;

    private void Start()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    void Update()
    {
        if (this.GetComponentInParent<PlacedObject>().armor == true)
        {
            this.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = shieldArmor;
        }
        else
        {
            this.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = shieldNormal;
        }
    }
}
