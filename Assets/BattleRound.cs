using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRound : MonoBehaviour
{
    public InputManager inputManager;
    public string gameState = null;
    public GameObject gameGridEnemy;
    public GameObject unitManager;
    //public Animator animatorUnit;

    public void StartBattle()
    {
        HealthManager(3f);
        gameState = null;
    }

    public void HealthManager(float delayTime)
    {
        //Debug.Log("Battle Start");
        StartCoroutine(DelayAction(delayTime));
    }

    IEnumerator DelayAction(float delayTime)
    {
        while (true)
        {
            CheckingGridBattle();
            CheckingGameState();
            //Debug.Log("In while");

            PlacedObject enemy = inputManager.GetEnemyObject(0);
            PlacedObject teamObject = inputManager.GetCellObject(0);

            if (gameState == "Win" || gameState == "Lose" || gameState == "Draw")
            {
                inputManager.battleOn = false;
                inputManager.UpdateCanvas(2);
                gameState = null;
                break;
            }

            if (teamObject != null && enemy != null)
            {
                enemy.GettingDamaged(teamObject.attack);
                inputManager.UpdateFloatingText(enemy);
                //animatorUnit.SetTrigger("HitTrigger");

                teamObject.GettingDamaged(enemy.attack);
                inputManager.UpdateFloatingText(teamObject);

                if (enemy.health <= 0)
                {
                    enemy.DestroySelf();
                    inputManager.GetEnemyCell(0).GetComponent<GridCell>().UnstoreObject(enemy);

                }

                if (teamObject.health <= 0)
                {
                    //teamObject.DestroySelf();
                    teamObject.gameObject.SetActive(false);
                    inputManager.GetGridCell(0).GetComponent<GridCell>().UnstoreObject(teamObject);
                    Debug.Log(inputManager.GetGridCell(0).GetComponent<GridCell>().GetPlacedObject());
                }

                CheckingGridBattle();
                CheckingGameState();

                if (gameState == "Win")
                {
                    inputManager.battleOn = false;
                    inputManager.UpdateCanvas(2);
                    inputManager.roundCounter += 1;
                    inputManager.DestroyField();
                    gameState = null;
                    break;
                }

                else if (gameState == "Lose")
                {
                    inputManager.battleOn = false;
                    inputManager.UpdateCanvas(2);
                    inputManager.roundCounter = 1;
                    inputManager.DestroyField();
                    gameState = null;
                    break;
                }

                else if (gameState == "Draw")
                {
                    inputManager.draws -= 1;

                    if (inputManager.draws > 0)
                    {
                        inputManager.battleOn = false;
                        inputManager.UpdateCanvas(2);
                        //inputManager.roundCounter += 1;
                        inputManager.DestroyField();
                        gameState = null;
                        break;
                    }
                    else
                    {
                        inputManager.battleOn = false;
                        inputManager.UpdateCanvas(2);
                        inputManager.roundCounter = 1;
                        inputManager.DestroyField();
                        gameState = null;
                        break;
                    }
                }
            }
            yield return new WaitForSeconds(delayTime);
        }
    }

    public void UnitMove(int oldPos, int newPos, bool enemy)
    {
        if (enemy == false)
        {
            PlacedObject movingPlacedObject = inputManager.GetCellObject(oldPos);
            GameObject newGridCell = inputManager.GetGridCell(newPos);
            GameObject oldGridCell = inputManager.GetGridCell(oldPos);

            oldGridCell.GetComponent<GridCell>().UnstoreObject(movingPlacedObject);

            newGridCell.GetComponent<GridCell>().StoreObject(movingPlacedObject);
            newGridCell.GetComponent<GridCell>().SetPlacedObject(movingPlacedObject);
            movingPlacedObject.transform.position = new Vector3(newGridCell.transform.position.x, 1f, newGridCell.transform.position.z);
        }
        else
        {
            PlacedObject movingPlacedObject = inputManager.GetEnemyObject(oldPos);
            GameObject newGridCell = inputManager.GetEnemyCell(newPos);
            GameObject oldGridCell = inputManager.GetEnemyCell(oldPos);

            oldGridCell.GetComponent<GridCell>().UnstoreObject(movingPlacedObject);

            newGridCell.GetComponent<GridCell>().StoreObject(movingPlacedObject);
            newGridCell.GetComponent<GridCell>().SetPlacedObject(movingPlacedObject);
            movingPlacedObject.transform.position = new Vector3(newGridCell.transform.position.x, 1f, newGridCell.transform.position.z);
        }
    }

    public void CheckingGameState()
    {
        if (CheckObjectInGridCell(0, true) == null && CheckObjectInGridCell(1, true) == null && CheckObjectInGridCell(2, true) == null)
        {
            if (CheckObjectInGridCell(0, false) == null && CheckObjectInGridCell(1, false) == null && CheckObjectInGridCell(2, false) == null)
            {
                Debug.Log("Draw");
                gameState = "Draw";
            }

            else
            {
                Debug.Log("Win");
                gameState = "Win";
            }
        }
        else if(CheckObjectInGridCell(0, false) == null && CheckObjectInGridCell(1, false) == null && CheckObjectInGridCell(2, false) == null)
        {
            if (CheckObjectInGridCell(0, true) == null && CheckObjectInGridCell(1, true) == null && CheckObjectInGridCell(2, true) == null)
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

    public void CheckingGridBattle()
    {
        if (CheckObjectInGridCell(0, false) == null && CheckObjectInGridCell(1, false) != null)
        {
            UnitMove(1, 0, false);
        }
        
        if (CheckObjectInGridCell(1, false) == null && CheckObjectInGridCell(2, false) != null)
        {
            if (CheckObjectInGridCell(0, false) == null)
            {
                UnitMove(2, 0, false);
            }
            else
            {
                UnitMove(2, 1, false);
            }

        }

        if (CheckObjectInGridCell(0, true) == null && CheckObjectInGridCell(1, true) != null)
        {
            UnitMove(1, 0, true);
        }

        if (CheckObjectInGridCell(1, true) == null && CheckObjectInGridCell(2, true) != null)
        {
            if (CheckObjectInGridCell(0, true) == null)
            {
                UnitMove(2, 0, true);
            }
            else
            {
                UnitMove(2, 1, true);
            }
        }
    }

    public void SetUpBuyingPhase()
    {
        int childNumber = unitManager.transform.childCount;
        for (int i = 0; i < childNumber; i++)
        {
            PlacedObject placedTeam = unitManager.transform.GetChild(i).GetComponent<PlacedObject>();
            GridCell gridCell = inputManager.GetGridCell(i).GetComponent<GridCell>();

            placedTeam.gameObject.SetActive(true);
            placedTeam.SetStats();
            inputManager.UpdateFloatingText(placedTeam);
            placedTeam.transform.position = new Vector3(gridCell.transform.position.x, 1f, gridCell.transform.position.z);
            gridCell.StoreObject(placedTeam);
            gridCell.SetPlacedObject(placedTeam);

            Debug.Log(i);
        }
    }
}
