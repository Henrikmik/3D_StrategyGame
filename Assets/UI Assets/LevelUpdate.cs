using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpdate : MonoBehaviour
{
    [SerializeField] private Sprite nullLevel;
    [SerializeField] private Sprite einsLevel;
    [SerializeField] private Sprite zweiLevel;
    [SerializeField] private Sprite dreiLevel;
    [SerializeField] private Sprite vierLevel;
    [SerializeField] private Sprite fünfLevel;
    [SerializeField] private Sprite sechsLevel;
    [SerializeField] private Sprite siebenLevel;

    public InputManager inputManager;

    public void Start()
    {

    }

    public void UpdateLevel(PlacedObject placedObject)
    {
        if (placedObject.level == 1)
        {
            placedObject.transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().sprite = einsLevel;
        }
        else if (placedObject.level == 2)
        {
            placedObject.transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().sprite =zweiLevel;
        }
        else if (placedObject.level == 3)
        {
            placedObject.transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().sprite = dreiLevel;
        }
        else if (placedObject.level == 4)
        {
            placedObject.transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().sprite = vierLevel;
        }
        else if (placedObject.level == 5)
        {
            placedObject.transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().sprite = fünfLevel;
        }
        else if (placedObject.level == 6)
        {
            placedObject.transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().sprite = sechsLevel;
        }
        else if (placedObject.level >= 7)
        {
            placedObject.transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().sprite = siebenLevel;
        }
        else
        {
            placedObject.transform.GetChild(0).GetChild(2).GetComponent<SpriteRenderer>().sprite = nullLevel;
        }
    }

}
