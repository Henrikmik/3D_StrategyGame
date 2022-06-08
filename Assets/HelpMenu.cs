using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{

    public static bool GameIsHelped = false;

    public GameObject helpMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (GameIsHelped)
            {
                HelpResume();
            }else
            {
                HelpPause();
            }
        }
    }

    public void HelpResume()
    {
        helpMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsHelped = false;
    }

    public void HelpPause()
    {
        helpMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsHelped = true;
    }
}
