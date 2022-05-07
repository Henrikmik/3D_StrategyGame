using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRound : MonoBehaviour
{
    public InputManager inputManager;
    public string gameState = null;
    public GameObject gameGridEnemy;
    public GameObject unitManager;
    public GameObject enemyManager;
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
                EndOfRound();
                inputManager.roundCounter = 10;
                break;
            }

            if (teamObject != null && enemy != null)
            {
                enemy.GettingDamaged(teamObject.attack);
                inputManager.UpdateFloatingText(enemy);
                CheckAbilityAttack(teamObject, enemyManager);   // überprüft ability vom angreifer
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
                    //Debug.Log(inputManager.GetGridCell(0).GetComponent<GridCell>().GetPlacedObject());
                }

                CheckingGridBattle();
                CheckingGameState();

                if (gameState == "Win")
                {
                    EndOfRound();
                    inputManager.roundCounter += 1;
                    break;
                }

                else if (gameState == "Lose")
                {
                    EndOfRound();
                    inputManager.roundCounter = 1;
                    break;
                }

                else if (gameState == "Draw")
                {
                    inputManager.draws -= 1;

                    if (inputManager.draws > 0)
                    {
                        EndOfRound();
                        inputManager.roundCounter += 0;
                        break;
                    }
                    else
                    {
                        EndOfRound();
                        inputManager.roundCounter = 1;
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

            //Debug.Log(i);
        }
    }

    public void EndOfRound()
    {
        inputManager.battleOn = false;
        inputManager.UpdateCanvas(2);
        inputManager.DestroyField();
        gameState = null;
        SetUpBuyingPhase();
        inputManager.shop.ShopReroll();
    }

    public string GetAbility(PlacedObject placedObject)
    {
        return placedObject.ability;
    }

    public void CheckAbilityDefense(PlacedObject placedObject)
    {
        string ability = GetAbility(placedObject);
        Debug.Log("Check Ability");

        if (ability == "apple")
        {
            Debug.Log("Apple");

            if (placedObject.health <= 0)
            {
                GameObject nextUnit = unitManager.transform.GetChild(placedObject.transform.GetSiblingIndex() + 1).gameObject;
                nextUnit.GetComponent<PlacedObject>().baseAttack += 2;
                nextUnit.GetComponent<PlacedObject>().baseHealth += 1;
                nextUnit.GetComponent<PlacedObject>().attack += 2;
                nextUnit.GetComponent<PlacedObject>().health += 1;
                inputManager.UpdateFloatingText(nextUnit.GetComponent<PlacedObject>());
                Debug.Log(nextUnit);
            }
        }
        else if (ability == "pineapple")
        {
            // stage 3
        }
        else if (ability == "grapes")
        {
            Debug.Log("Grapes");

            if (placedObject.health <= 0)
            {
                Unit unit = inputManager.unitList[6];

                if (inputManager.GetCellObject(0) == null)
                {
                    GridCell gridCell = inputManager.GetGridCell(0).GetComponent<GridCell>();
                    Vector2Int pos2 = gridCell.GetPosition();
                    Vector3 pos3 = new Vector3(inputManager.GetGridCell(0).transform.position.x, 1f, inputManager.GetGridCell(0).transform.position.z);

                    PlacedObject placedO = PlacedObject.Create(pos3, pos2, Unit.Dir.Down, unit);
                    gridCell.SetPlacedObject(placedO);
                    gridCell.StoreObject(placedO);
                    placedO.transform.SetParent(unitManager.transform);
                    placedO.SettingStats();
                    inputManager.ShowFloatingText(placedO, pos3);
                }
                else if (inputManager.GetCellObject(1) == null)
                {
                    GridCell gridCell = inputManager.GetGridCell(1).GetComponent<GridCell>();
                    Vector2Int pos2 = gridCell.GetPosition();
                    Vector3 pos3 = new Vector3(inputManager.GetGridCell(1).transform.position.x, 1f, inputManager.GetGridCell(1).transform.position.z);

                    PlacedObject placedO = PlacedObject.Create(pos3, pos2, Unit.Dir.Down, unit);
                    gridCell.SetPlacedObject(placedO);
                    gridCell.StoreObject(placedO);
                    placedO.transform.SetParent(unitManager.transform);
                    placedO.SettingStats();
                    inputManager.ShowFloatingText(placedO, pos3);
                }
                else if(inputManager.GetCellObject(2) == null)
                {
                    GridCell gridCell = inputManager.GetGridCell(2).GetComponent<GridCell>();
                    Vector2Int pos2 = gridCell.GetPosition();
                    Vector3 pos3 = new Vector3(inputManager.GetGridCell(2).transform.position.x, 1f, inputManager.GetGridCell(2).transform.position.z);

                    PlacedObject placedO = PlacedObject.Create(pos3, pos2, Unit.Dir.Down, unit);
                    gridCell.SetPlacedObject(placedO);
                    gridCell.StoreObject(placedO);
                    placedO.transform.SetParent(unitManager.transform);
                    placedO.SettingStats();
                    inputManager.ShowFloatingText(placedO, pos3);
                }
                else
                {
                    Debug.Log("No free grid");
                }
            }
        }
        else if (ability == "coconut")
        {
            Debug.Log("Coconut");

            if (placedObject.armor == true)
            {
                placedObject.health = placedObject.baseHealth;
                placedObject.armor = false;
                inputManager.UpdateFloatingText(placedObject);
            }
        }
        else if (ability == "lemon")
        {
            Debug.Log("Lemon");

            if ((placedObject.health != placedObject.baseHealth) && (placedObject.health > 0))
            {
                int randomOption = Random.Range(0, unitManager.transform.childCount);
                
                while (randomOption == placedObject.transform.GetSiblingIndex())
                {
                    randomOption = Random.Range(0, unitManager.transform.childCount - 1);
                }

                GameObject buffedUnit = unitManager.transform.GetChild(randomOption).gameObject;
                buffedUnit.GetComponent<PlacedObject>().baseAttack += 1;
                buffedUnit.GetComponent<PlacedObject>().baseHealth += 1;
                buffedUnit.GetComponent<PlacedObject>().attack += 1;
                buffedUnit.GetComponent<PlacedObject>().health += 1;
                inputManager.UpdateFloatingText(buffedUnit.GetComponent<PlacedObject>());
                Debug.Log(buffedUnit);
            }
        }
        else
        {
            Debug.Log("No ability");
        }
    }

    public void CheckAbilityAttack(PlacedObject placedObject, GameObject manager)   // enemyManager bei cherry und unitManager bei garlic
    {
        string ability = GetAbility(placedObject);
        Debug.Log("Check Ability");

        if (ability == "cherry")
        {
            Debug.Log("Cherry");

            if (manager.transform.GetChild(1) != null)
            {
                manager.transform.GetChild(1).GetComponent<PlacedObject>().health -= 2;
                inputManager.UpdateFloatingText(manager.transform.GetChild(1).GetComponent<PlacedObject>());
            }
        }
        else
        {
            Debug.Log("No ability");
        }
    }
}
