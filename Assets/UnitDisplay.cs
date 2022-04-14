using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDisplay : MonoBehaviour
{
    public Unit apple;

    public Text nameText;
    public Text descriptionText;

    public Text attackText;
    public Text healthText;
    public Text levelText;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(apple.description);
    }
}
