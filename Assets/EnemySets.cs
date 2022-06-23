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
        Vector3 enemyPos3 = new Vector3(gridcell.transform.position.x, 1f, gridcell.transform.position.z);

        PlacedObject placedObject = PlacedObject.Create(enemyPos3, enemyPos, Unit.Dir.Down, enemy);
        gridcell.SetPlacedObject(placedObject);
        gridcell.StoreObject(placedObject);
        placedObject.transform.SetParent(inputManager.enemyManager.transform);
        placedObject.SettingStatsEnemy(attackv, healthv, levelv);
        inputManager.ShowFloatingText(placedObject, enemyPos3, false);
        placedObject.transform.GetChild(0).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        placedObject.transform.GetChild(0).GetChild(2).position = new Vector3(placedObject.transform.GetChild(0).GetChild(2).position.x, 2f, placedObject.transform.GetChild(0).GetChild(2).position.z);
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

        int i = Random.Range(1, 4);

        if (inputManager.roundCounter == 1) // stage 1
        {
            if (i <= 1)         // set 1
            {
                CreateEnemy(7, enemyGrid1, 2, 1, 1);    // Tomato
                CreateEnemy(12, enemyGrid2, 1, 2, 1);   // Broccoli
                CreateEnemy(11, enemyGrid3, 1, 3, 1);   // Pumpkin
            }
            else if (i == 2)    // set 2
            {
                CreateEnemy(8, enemyGrid1, 1, 1, 1);    // Garlic
                CreateEnemy(7, enemyGrid2, 2, 1, 1);
                CreateEnemy(12, enemyGrid3, 1, 2, 1);
            }
            else if (i >= 3)     // set 3
            {
                CreateEnemy(11, enemyGrid1, 1, 3, 1);
                CreateEnemy(7, enemyGrid2, 2, 1, 1);
                CreateEnemy(10, enemyGrid3, 2, 2, 1);   // Corn
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
                CreateEnemy(11, enemyGrid2, 2, 6, 1);
                CreateEnemy(10, enemyGrid3, 4, 4, 1);
            }
            else if (i == 2)    // set 2
            {
                CreateEnemy(7, enemyGrid1, 4, 2, 1);
                CreateEnemy(11, enemyGrid2, 2, 6, 1);
                CreateEnemy(12, enemyGrid3, 2, 4, 1);
            }
            else if (i >= 3)    // set 3
            {
                CreateEnemy(11, enemyGrid1, 6, 4, 1);
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
                CreateEnemy(10, enemyGrid2, 6, 6, 2);

                CreateEnemy(12, enemyGrid4, 4, 6, 1);
                CreateEnemy(7, enemyGrid5, 6, 4, 1);
                CreateEnemy(11, enemyGrid6, 3, 8, 1);
            }
            else if (i == 2)    // set 2
            {
                CreateEnemy(8, enemyGrid1, 5, 5, 2);    // Garlic
                CreateEnemy(7, enemyGrid2, 6, 4, 1);    // Tomato

                CreateEnemy(12, enemyGrid4, 4, 6, 1);
                CreateEnemy(7, enemyGrid5, 6, 4, 1);
                CreateEnemy(8, enemyGrid6, 5, 5, 1);
            }
            else if (i >= 3)    // set 3
            {
                CreateEnemy(11, enemyGrid1, 3, 8, 1);    // Pumpkin
                CreateEnemy(12, enemyGrid2, 4, 6, 1);    // Broccoli
                CreateEnemy(12, enemyGrid3, 4, 6, 1);

                CreateEnemy(8, enemyGrid4, 5, 5, 1);
                CreateEnemy(7, enemyGrid5, 6, 4, 1);
                CreateEnemy(11, enemyGrid6, 3, 8, 1);
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
                CreateEnemy(11, enemyGrid1, 5, 10, 1);    // Pumpkin
                CreateEnemy(9, enemyGrid2, 10, 5, 2);    // Eggplant -> stats nachfragen
                CreateEnemy(7, enemyGrid3, 8, 6, 1);

                CreateEnemy(9, enemyGrid4, 10, 5, 1);
                CreateEnemy(10, enemyGrid5, 8, 8, 1);   // Corn
                CreateEnemy(7, enemyGrid6, 8, 6, 1);    // Tomato
            }
            else if (i == 2)    // set 2
            {
                CreateEnemy(12, enemyGrid1, 6, 8, 1);    // Broccoli
                CreateEnemy(7, enemyGrid2, 8, 6, 1);
                CreateEnemy(9, enemyGrid3, 10, 5, 2);

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
                CreateEnemy(9, enemyGrid5, 10, 5, 1);
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
                CreateEnemy(10, enemyGrid1, 10, 10, 2); // Corn
                CreateEnemy(9, enemyGrid2, 12, 7, 2);   // Eggplant

                CreateEnemy(11, enemyGrid4, 7, 12, 1);  // Pumpkin
                CreateEnemy(10, enemyGrid5, 10, 10, 2); // Corn
                CreateEnemy(7, enemyGrid6, 10, 8, 1);   // Tomato
            }
            else if (i == 2)    // set 2
            {
                CreateEnemy(11, enemyGrid1, 7, 12, 2);
                CreateEnemy(8, enemyGrid2, 9, 9, 2);    // Garlic

                CreateEnemy(12, enemyGrid4, 8, 10, 1);
                CreateEnemy(11, enemyGrid5, 7, 12, 2);
                CreateEnemy(9, enemyGrid6, 12, 7, 1);
            }
            else if (i >= 3)    // set 3
            {
                CreateEnemy(9, enemyGrid1, 12, 7, 3);
                CreateEnemy(7, enemyGrid2, 10, 8, 1);

                CreateEnemy(7, enemyGrid4, 10, 8, 1);
                CreateEnemy(8, enemyGrid5, 11, 11, 2);
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
                CreateEnemy(11, enemyGrid1, 9, 14, 3);
                CreateEnemy(7, enemyGrid2, 12, 10, 1);
                CreateEnemy(12, enemyGrid3, 10, 12, 1);

                CreateEnemy(8, enemyGrid4, 11, 11, 2);
                CreateEnemy(9, enemyGrid5, 14, 9, 2);
                CreateEnemy(7, enemyGrid6, 12, 10, 1);
            }
            else if (i == 2)    // set 2
            {
                CreateEnemy(7, enemyGrid1, 12, 10, 1);
                CreateEnemy(10, enemyGrid2, 12, 12, 3);

                CreateEnemy(7, enemyGrid4, 12, 10, 1);
                CreateEnemy(9, enemyGrid5, 14, 9, 2);
                CreateEnemy(11, enemyGrid6, 9, 14, 2);
            }
            else if (i >= 3)    // set 3
            {
                CreateEnemy(9, enemyGrid1, 16, 11, 3);
                CreateEnemy(12, enemyGrid2, 10, 12, 1);

                CreateEnemy(12, enemyGrid4, 10, 12, 1);
                CreateEnemy(10, enemyGrid5, 12, 12, 2);
                CreateEnemy(9, enemyGrid6, 14, 9, 2);
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
                CreateEnemy(9, enemyGrid1, 16, 11, 3);
                CreateEnemy(10, enemyGrid2, 14, 14, 3);
                CreateEnemy(11, enemyGrid3, 11, 16, 3);

                CreateEnemy(7, enemyGrid4, 14, 12, 1);
                CreateEnemy(10, enemyGrid5, 14, 14, 3);
                CreateEnemy(9, enemyGrid6, 16, 11, 3);
            }
            else if (i == 2)    // set 2
            {
                CreateEnemy(8, enemyGrid1, 13, 13, 3);
                CreateEnemy(11, enemyGrid2, 11, 16, 3);
                CreateEnemy(10, enemyGrid3, 14, 14, 3);

                CreateEnemy(12, enemyGrid4, 12, 14, 1);
                CreateEnemy(9, enemyGrid5, 16, 11, 3);
                CreateEnemy(11, enemyGrid6, 11, 16, 3);
            }
            else if (i >= 3)    // set 3
            {
                CreateEnemy(11, enemyGrid1, 11, 16, 3);
                CreateEnemy(9, enemyGrid2, 16, 11, 3);
                CreateEnemy(8, enemyGrid3, 13, 13, 3);

                CreateEnemy(7, enemyGrid4, 14, 12, 1);
                CreateEnemy(11, enemyGrid5, 11, 16, 3);
                CreateEnemy(10, enemyGrid6, 14, 14, 3);
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
