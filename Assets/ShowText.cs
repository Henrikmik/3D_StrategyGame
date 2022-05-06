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
        goldText.text = "Gold: " + inputManager.gold.ToString();
        drawText.text = "Draws left: " + inputManager.draws.ToString();
        roundText.text = "Round: " + inputManager.roundCounter.ToString();
    }
}
