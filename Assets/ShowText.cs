using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowText : MonoBehaviour
{
    public InputManager inputManager;
    public TMP_Text goldText;
    public TMP_Text drawText;
    public TMP_Text roundText;

    private void Start()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }
    void Update()
    {
        goldText.text = inputManager.gold.ToString();
        drawText.text = "Draws:" + "\n" + inputManager.draws.ToString();
        roundText.text = inputManager.roundCounter.ToString();
    }
}
