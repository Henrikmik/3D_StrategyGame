using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levelmanager : MonoBehaviour
{

    InputManager inputManager;
    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;
    public GameObject level5;
    public GameObject level6;
    public GameObject level7;

    private void Start()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    private void Update()
    {
        if (inputManager.roundCounter == 1)
        {
            level1.SetActive(true);
            level2.SetActive(false);
            level3.SetActive(false);
            level4.SetActive(false);
            level5.SetActive(false);
            level6.SetActive(false);
            level7.SetActive(false);
        }
        else if(inputManager.roundCounter == 2)
        {
            level1.SetActive(false);
            level2.SetActive(true);
            level3.SetActive(false);
            level4.SetActive(false);
            level5.SetActive(false);
            level6.SetActive(false);
            level7.SetActive(false);
        }
        else if (inputManager.roundCounter == 3)
        {
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(true);
            level4.SetActive(false);
            level5.SetActive(false);
            level6.SetActive(false);
            level7.SetActive(false);
        }
        else if (inputManager.roundCounter == 4)
        {
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(false);
            level4.SetActive(true);
            level5.SetActive(false);
            level6.SetActive(false);
            level7.SetActive(false);
        }
        else if (inputManager.roundCounter == 5)
        {
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(false);
            level4.SetActive(false);
            level5.SetActive(true);
            level6.SetActive(false);
            level7.SetActive(false);
        }
        else if (inputManager.roundCounter == 6)
        {
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(false);
            level4.SetActive(false);
            level5.SetActive(false);
            level6.SetActive(true);
            level7.SetActive(false);
        }
        else if (inputManager.roundCounter == 7)
        {
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(false);
            level4.SetActive(false);
            level5.SetActive(false);
            level6.SetActive(false);
            level7.SetActive(true);
        }
        else if (inputManager.roundCounter == 8)
        {
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(false);
            level4.SetActive(false);
            level5.SetActive(false);
            level6.SetActive(false);
            level7.SetActive(false);
        }
        else
        {
            Debug.Log("Kein Level");
        }
    }

}
