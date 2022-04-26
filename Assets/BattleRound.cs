using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRound : MonoBehaviour
{
    public InputManager inputManager;
    public string gameState = null;

    void Start()
    {
        HealthManager(3f);
    }

    public void HealthManager(float delayTime)
    {
        StartCoroutine(DelayAction(delayTime));
        //if (inputManager.GetCellObject(0) != null)
        //{
        //    StartCoroutine(DelayAction(delayTime));
        //}
        //else
        //{
        //    if (inputManager.GetCellObject(1) != null)
        //    {
        //        // Unit move
        //        // StartCoroutine
        //    }
        //    else if(inputManager.GetCellObject(2) != null)
        //    {
        //        // Unit move
        //        // StartCoroutine
        //    }
        //}
    }

    IEnumerator DelayAction(float delayTime)
    {
        while (true)
        {
            PlacedObject enemy = inputManager.GetEnemyObject(0);
            PlacedObject teamObject = inputManager.GetCellObject(0);

            CheckingGameState();
            if (gameState == "Win" || gameState == "Lose" || gameState == "Lose")
            {
                break;
            }

            if (teamObject && enemy != null)
            {
                enemy.health -= teamObject.attack;
                inputManager.UpdateFloatingText(enemy);

                teamObject.health -= enemy.attack;
                inputManager.UpdateFloatingText(teamObject);

                if (enemy.health <= 0)
                {
                    enemy.DestroySelf();
                }

                if (teamObject.health <= 0)
                {
                    teamObject.DestroySelf();
                }
            }
            yield return new WaitForSeconds(delayTime);
        }
    }

    public void UnitMove(int oldPos, int newPos)
    {
        PlacedObject movingPlacedObject = inputManager.GetCellObject(oldPos);
        GameObject newGridCell = inputManager.GetGridCell(newPos);
        GameObject oldGridCell = inputManager.GetGridCell(oldPos);

        oldGridCell.GetComponent<GridCell>().UnstoreObject(movingPlacedObject);

        newGridCell.GetComponent<GridCell>().StoreObject(movingPlacedObject);
        movingPlacedObject.transform.position = new Vector3(newGridCell.transform.position.x, 1f, newGridCell.transform.position.z);
        //cellMouseIsOver.StoreObject(placedObject);
        //cellMouseIsOver.SetPlacedObject(placedObject);
    }

    public void CheckingGameState()
    {
        Debug.Log("CheckingState"); // Klammer drum machen
        if (CheckObjectInGridCell(0, false) && CheckObjectInGridCell(1, false) && CheckObjectInGridCell(2, false) == null)
        {
            if (CheckObjectInGridCell(0, true) && CheckObjectInGridCell(1, true) && CheckObjectInGridCell(2, true) == null)
            {
                Debug.Log("Draft");
                gameState = "Draft";
            }

            else
            {
                Debug.Log("Win");
                Debug.Log(inputManager.GetCellObject(0));
                gameState = "Win";
            }
        }
        else if(CheckObjectInGridCell(0, false) && CheckObjectInGridCell(1, false) && CheckObjectInGridCell(2, false) == null)
        {
            if (CheckObjectInGridCell(0, true) && CheckObjectInGridCell(1, true) && CheckObjectInGridCell(2, true) == null)
            {
                Debug.Log("Draft");
                gameState = "Draft";
            }

            else
            {
                Debug.Log("Lose");
                gameState = "Lose";
            }
        }
    }

    public PlacedObject CheckObjectInGridCell(int pos, bool enemy)
    {
        if (enemy == false)
        {
            PlacedObject objectInThisGridSpace = inputManager.GetGridCell(pos).GetComponent<GridCell>().objectInThisGridSpace;
            return objectInThisGridSpace;
        }
        else
        {
            PlacedObject objectInThisGridSpace = inputManager.GetEnemyCell(pos).GetComponent<GridCell>().objectInThisGridSpace;
            return objectInThisGridSpace;
        }
    }
}
