using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySets : MonoBehaviour
{
    public InputManager inputManager;
    private GridCell enemyGrid1;
    private GridCell enemyGrid2;
    private GridCell enemyGrid3;
    private GridCell enemyGrid4;
    private GridCell enemyGrid5;
    private GridCell enemyGrid6;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    public void CreateEnemy(int list, GridCell gridcell, int attackv, int healthv, int levelv)
    {

        Unit enemy = inputManager.unitList[list];
        Vector2Int enemyPos = gridcell.GetPosition();
        Vector3 enemyPos3 = new Vector3(gridcell.transform.position.x, 0f, gridcell.transform.position.z);

        PlacedObject placedObject = PlacedObject.Create(enemyPos3, enemyPos, Unit.Dir.Down, enemy);
        gridcell.SetPlacedObject(placedObject);
        gridcell.StoreObject(placedObject);
        placedObject.transform.SetParent(inputManager.enemyManager.transform);
        placedObject.SettingStatsEnemy(attackv, healthv, levelv);
        inputManager.ShowFloatingText(placedObject, enemyPos3, false);
        
        Transform visualT = placedObject.transform.GetChild(1).GetChild(0);
        Transform statsT = placedObject.transform.GetChild(0);
        //visualT.eulerAngles = new Vector3(0f, 0f, 0f);
        statsT.transform.localPosition = new Vector3 (0.7f, 0.15f, - 0.6f);
        statsT.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
        statsT.GetChild(2).position = new Vector3 (placedObject.transform.GetChild(0).GetChild(2).position.x, 2f, placedObject.transform.GetChild(0).GetChild(2).position.z);

        inputManager.levelUpdate.UpdateLevel(placedObject);
    }

    public void EnemySet()
    {
        if (inputManager.roundCounter < 3)
        {
            enemyGrid1 = inputManager.gameGridEnemyS.transform.GetChild(0).GetComponent<GridCell>();
            enemyGrid2 = inputManager.gameGridEnemyS.transform.GetChild(1).GetComponent<GridCell>();
            enemyGrid3 = inputManager.gameGridEnemyS.transform.GetChild(2).GetComponent<GridCell>();
        }
        else
        {
            enemyGrid1 = inputManager.gameGridEnemyS.transform.GetChild(0).GetComponent<GridCell>();
            enemyGrid2 = inputManager.gameGridEnemyS.transform.GetChild(1).GetComponent<GridCell>();
            enemyGrid3 = inputManager.gameGridEnemyS.transform.GetChild(2).GetComponent<GridCell>();
            enemyGrid4 = inputManager.gameGridEnemyS.transform.GetChild(3).GetComponent<GridCell>();
            enemyGrid5 = inputManager.gameGridEnemyS.transform.GetChild(4).GetComponent<GridCell>();
            enemyGrid6 = inputManager.gameGridEnemyS.transform.GetChild(5).GetComponent<GridCell>();
        }

        int i;
        if (inputManager.roundCounter == 1)
        {
            i = Random.Range(1, 6);
        }
        else
        {
            i = Random.Range(1, 4);
        }

        if (inputManager.roundCounter == 1) // stage 1
        {
            if (i <= 1)         // set 1
            {
                CreateEnemy(7, enemyGrid1, 2, 1, 1);    // Tomato
                CreateEnemy(12, enemyGrid2, 1, 2, 1);   // Broccoli
                CreateEnemy(11, enemyGrid3, 1, 2, 1);   // Pumpkin
            }
            else if (i == 2)    // set 2
            {
                CreateEnemy(8, enemyGrid1, 1, 1, 1);    // Garlic
                CreateEnemy(7, enemyGrid2, 2, 1, 1);
                CreateEnemy(12, enemyGrid3, 1, 2, 1);
            }
            else if (i == 3)     // set 3
            {
                CreateEnemy(11, enemyGrid1, 1, 2, 1);
                CreateEnemy(7, enemyGrid2, 2, 1, 1);
                CreateEnemy(10, enemyGrid3, 2, 2, 1);   // Corn
            }
            else if (i == 4)     // set 3
            {
                CreateEnemy(10, enemyGrid1, 2, 2, 1);   // Corn
                CreateEnemy(11, enemyGrid2, 1, 2, 1);
                CreateEnemy(9, enemyGrid3, 2, 1, 1);    // Aubergine
            }
            else if (i >= 5)     // set 3
            {
                CreateEnemy(9, enemyGrid1, 2, 1, 1);    // Aubergine
                CreateEnemy(7, enemyGrid2, 2, 1, 1);
                CreateEnemy(8, enemyGrid3, 1, 1, 1);   // Garlic
            }
            else
            {
                Debug.Log("i out of range");
            }
        }
        else if (inputManager.roundCounter == 2)    // stage 2
        {
            if (i <= 1)         // set 1
            {
                CreateEnemy(8, enemyGrid1, 2, 2, 1);
                CreateEnemy(11, enemyGrid2, 2, 5, 1);
                CreateEnemy(10, enemyGrid3, 4, 4, 1);
            }
            else if (i == 2)    // set 2
            {
                CreateEnemy(7, enemyGrid1, 4, 2, 1);
                CreateEnemy(11, enemyGrid2, 2, 5, 1);
                CreateEnemy(12, enemyGrid3, 2, 4, 1);
            }
            else if (i >= 3)    // set 3
            {
                CreateEnemy(11, enemyGrid1, 2, 5, 1);
                CreateEnemy(10, enemyGrid2, 4, 4, 1);
                CreateEnemy(7, enemyGrid3, 4, 2, 1);
            }
            else
            {
                Debug.Log("i out of range");
            }
        }
        else if (inputManager.roundCounter == 3)    // stage 3
        {
            if (i <= 1)         // set 1
            {
                CreateEnemy(12, enemyGrid1, 6, 4, 1);
                CreateEnemy(10, enemyGrid2, 6, 6, 4);

                CreateEnemy(12, enemyGrid4, 4, 6, 1);
                CreateEnemy(7, enemyGrid5, 6, 4, 1);
                CreateEnemy(11, enemyGrid6, 3, 7, 1);
            }
            else if (i == 2)    // set 2
            {
                CreateEnemy(8, enemyGrid1, 5, 5, 4);    // Garlic
                CreateEnemy(7, enemyGrid2, 6, 4, 1);    // Tomato

                CreateEnemy(12, enemyGrid4, 4, 6, 1);
                CreateEnemy(7, enemyGrid5, 6, 4, 1);
                CreateEnemy(8, enemyGrid6, 5, 5, 1);
            }
            else if (i >= 3)    // set 3
            {
                CreateEnemy(11, enemyGrid1, 3, 7, 1);    // Pumpkin
                CreateEnemy(12, enemyGrid2, 4, 6, 1);    // Broccoli
                CreateEnemy(12, enemyGrid3, 4, 6, 1);

                CreateEnemy(8, enemyGrid4, 5, 5, 1);
                CreateEnemy(7, enemyGrid5, 6, 4, 1);
                CreateEnemy(11, enemyGrid6, 3, 7, 1);
            }
            else
            {
                Debug.Log("i out of range");
            }
        }
        else if (inputManager.roundCounter == 4)    // stage 4
        {
            if (i <= 1)         // set 1
            {
                CreateEnemy(11, enemyGrid1, 5, 9, 1);    // Pumpkin
                CreateEnemy(9, enemyGrid2, 9, 5, 4);    // Eggplant -> stats nachfragen
                CreateEnemy(7, enemyGrid3, 8, 6, 1);

                CreateEnemy(9, enemyGrid4, 9, 5, 1);
                CreateEnemy(10, enemyGrid5, 8, 8, 1);   // Corn
                CreateEnemy(7, enemyGrid6, 8, 6, 1);    // Tomato
            }
            else if (i == 2)    // set 2
            {
                CreateEnemy(12, enemyGrid1, 6, 8, 1);    // Broccoli
                CreateEnemy(7, enemyGrid2, 8, 6, 1);
                CreateEnemy(9, enemyGrid3, 9, 5, 4);

                CreateEnemy(10, enemyGrid4, 8, 8, 1);
                CreateEnemy(8, enemyGrid5, 7, 7, 1);
                CreateEnemy(12, enemyGrid6, 6, 8, 1);
            }
            else if (i >= 3)    // set 3
            {
                CreateEnemy(10, enemyGrid1, 8, 8, 1);
                CreateEnemy(7, enemyGrid2, 8, 6, 1);
                CreateEnemy(8, enemyGrid3, 7, 7, 1);

                CreateEnemy(12, enemyGrid4, 6, 8, 1);
                CreateEnemy(9, enemyGrid5, 9, 5, 1);
                CreateEnemy(10, enemyGrid6, 8, 8, 1);
            }
            else
            {
                Debug.Log("i out of range");
            }
        }
        else if (inputManager.roundCounter == 5)    // stage 5
        {
            if (i <= 1)         // set 1
            {
                CreateEnemy(10, enemyGrid1, 10, 10, 4); // Corn
                CreateEnemy(9, enemyGrid2, 11, 7, 4);   // Eggplant

                CreateEnemy(11, enemyGrid4, 7, 11, 1);  // Pumpkin
                CreateEnemy(10, enemyGrid5, 10, 10, 4); // Corn
                CreateEnemy(7, enemyGrid6, 10, 8, 1);   // Tomato
            }
            else if (i == 2)    // set 2
            {
                CreateEnemy(11, enemyGrid1, 7, 11, 4);
                CreateEnemy(8, enemyGrid2, 9, 9, 4);    // Garlic

                CreateEnemy(12, enemyGrid4, 8, 10, 1);
                CreateEnemy(11, enemyGrid5, 7, 12, 4);
                CreateEnemy(9, enemyGrid6, 11, 7, 1);
            }
            else if (i >= 3)    // set 3
            {
                CreateEnemy(9, enemyGrid1, 11, 7, 7);
                CreateEnemy(7, enemyGrid2, 10, 8, 1);

                CreateEnemy(7, enemyGrid4, 10, 8, 1);
                CreateEnemy(8, enemyGrid5, 11, 11, 4);
                CreateEnemy(12, enemyGrid6, 8, 10, 1);
            }
            else
            {
                Debug.Log("i out of range");
            }
        }
        else if (inputManager.roundCounter == 6)    // stage 6
        {
            if (i <= 1)         // set 1
            {
                CreateEnemy(11, enemyGrid1, 9, 13, 7);
                CreateEnemy(7, enemyGrid2, 12, 10, 1);
                CreateEnemy(12, enemyGrid3, 10, 12, 1);

                CreateEnemy(8, enemyGrid4, 11, 11, 4);
                CreateEnemy(9, enemyGrid5, 13, 9, 4);
                CreateEnemy(7, enemyGrid6, 12, 10, 1);
            }
            else if (i == 2)    // set 2
            {
                CreateEnemy(7, enemyGrid1, 12, 10, 1);
                CreateEnemy(10, enemyGrid2, 12, 12, 7);

                CreateEnemy(7, enemyGrid4, 12, 10, 1);
                CreateEnemy(9, enemyGrid5, 13, 9, 4);
                CreateEnemy(11, enemyGrid6, 9, 13, 4);
            }
            else if (i >= 3)    // set 3
            {
                CreateEnemy(9, enemyGrid1, 15, 11, 7);
                CreateEnemy(12, enemyGrid2, 10, 12, 1);

                CreateEnemy(12, enemyGrid4, 10, 12, 1);
                CreateEnemy(10, enemyGrid5, 12, 12, 4);
                CreateEnemy(9, enemyGrid6, 13, 9, 4);
            }
            else
            {
                Debug.Log("i out of range");
            }
        }
        else if (inputManager.roundCounter == 7)    // stage 7
        {
            if (i <= 1)         // set 1
            {
                CreateEnemy(9, enemyGrid1, 15, 11, 7);
                CreateEnemy(10, enemyGrid2, 14, 14, 7);
                CreateEnemy(11, enemyGrid3, 11, 15, 7);

                CreateEnemy(7, enemyGrid4, 14, 12, 1);
                CreateEnemy(10, enemyGrid5, 14, 14, 7);
                CreateEnemy(9, enemyGrid6, 15, 11, 7);
            }
            else if (i == 2)    // set 2
            {
                CreateEnemy(8, enemyGrid1, 13, 13, 7);
                CreateEnemy(11, enemyGrid2, 11, 15, 7);
                CreateEnemy(10, enemyGrid3, 14, 14, 7);

                CreateEnemy(12, enemyGrid4, 12, 14, 1);
                CreateEnemy(9, enemyGrid5, 15, 11, 7);
                CreateEnemy(11, enemyGrid6, 11, 15, 7);
            }
            else if (i >= 3)    // set 3
            {
                CreateEnemy(11, enemyGrid1, 11, 15, 7);
                CreateEnemy(9, enemyGrid2, 15, 11, 7);
                CreateEnemy(8, enemyGrid3, 13, 13, 7);

                CreateEnemy(7, enemyGrid4, 14, 12, 1);
                CreateEnemy(11, enemyGrid5, 11, 15, 7);
                CreateEnemy(10, enemyGrid6, 14, 14, 7);
            }
            else
            {
                Debug.Log("i out of range");
            }
        }
        else
        {
            Debug.Log("Roundcounter too high");
        }
    }
}
