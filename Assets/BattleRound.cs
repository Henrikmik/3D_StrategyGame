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
                yield return new WaitForSeconds(1f);

                // Unit greift Enemy an
                enemy.GettingDamaged(teamObject.attack);
                inputManager.UpdateFloatingText(enemy);
                CheckAbilityAttack(teamObject, enemyManager, true);   // überprüft ability vom Angreifer
                CheckAbilityDefense(enemy, enemyManager, true);

                //yield return new WaitForSeconds(1f);  // für tests

                // Enemy greift Unit an
                teamObject.GettingDamaged(enemy.attack);
                inputManager.UpdateFloatingText(teamObject);
                CheckAbilityAttack(enemy, unitManager, false);   // überprüft ability vom Angreifer
                CheckAbilityDefense(teamObject, unitManager, false);

                //yield return new WaitForSeconds(10f);

                if ((inputManager.GetEnemyObject(0) != null) && (inputManager.GetEnemyObject(0).health <= 0))
                {
                    inputManager.GetEnemyObject(0).DestroySelf();
                    inputManager.GetEnemyCell(0).GetComponent<GridCell>().UnstoreObject(inputManager.GetEnemyObject(0));
                }

                if ((inputManager.GetEnemyObject(1) != null) && (inputManager.GetEnemyObject(1).health <= 0))
                {
                    inputManager.GetEnemyObject(1).DestroySelf();
                    inputManager.GetEnemyCell(1).GetComponent<GridCell>().UnstoreObject(inputManager.GetEnemyObject(1));
                }

                if ((inputManager.GetEnemyObject(2) != null) && (inputManager.GetEnemyObject(2).health <= 0))
                {
                    inputManager.GetEnemyObject(2).DestroySelf();
                    inputManager.GetEnemyCell(2).GetComponent<GridCell>().UnstoreObject(inputManager.GetEnemyObject(2));
                }

                if ((inputManager.GetCellObject(0) != null) && (inputManager.GetCellObject(0).health <= 0))
                {
                    inputManager.GetCellObject(0).gameObject.SetActive(false);
                    inputManager.GetGridCell(0).GetComponent<GridCell>().UnstoreObject(inputManager.GetCellObject(0));
                }

                if ((inputManager.GetCellObject(1) != null) && (inputManager.GetCellObject(1).health <= 0))
                {
                    inputManager.GetCellObject(1).gameObject.SetActive(false);
                    inputManager.GetGridCell(1).GetComponent<GridCell>().UnstoreObject(inputManager.GetCellObject(1));
                }

                if ((inputManager.GetCellObject(2) != null) && (inputManager.GetCellObject(2).health <= 0))
                {
                    inputManager.GetCellObject(2).gameObject.SetActive(false);
                    inputManager.GetGridCell(2).GetComponent<GridCell>().UnstoreObject(inputManager.GetCellObject(2));
                }

                CheckingGridBattle();
                yield return new WaitForSeconds(1f);
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
            yield return new WaitForSeconds(delayTime); // letzter change
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
                Debug.Log("Draw");
                gameState = "Draw";
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
        int childNumb = unitManager.transform.childCount;

        for (int i = 0; i < childNumb; i++)
        {
            if (unitManager.transform.GetChild(i).name == "Mini Grape(Clone)")
            {
                Destroy(unitManager.transform.GetChild(i).gameObject);
                childNumb -= 1;
            }
            else
            {

            }
        }

        Debug.Log("Test");
        Debug.Log(childNumb);
        for (int i = 0; i < childNumb; i++)
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

    public void CheckAbilityDefense(PlacedObject placedObject, GameObject manager, bool enemy)  // unitManager/enemyManager : wenn enemy, dann true, wenn unit, dann false
    {
        string ability = GetAbility(placedObject);
        //Debug.Log("Check Ability");

        if (ability == "apple")
        {
            //Debug.Log("Apple");

            if (placedObject.health <= 0)
            {
                int index = placedObject.transform.GetSiblingIndex() + 1;
                Debug.Log(index);

                if (inputManager.GetCellObject(index) == null)
                {
                    //Debug.Log("No unit to buff");
                }
                else
                {
                    inputManager.GetCellObject(index).baseAttack += 2;
                    inputManager.GetCellObject(index).baseHealth += 1;
                    inputManager.GetCellObject(index).attack += 2;
                    inputManager.GetCellObject(index).health += 1;
                    inputManager.UpdateFloatingText(inputManager.GetCellObject(index));
                    Debug.Log("apple: " + inputManager.GetCellObject(index));
                }
            }
        }
        else if (ability == "pineapple")
        {
            // stage 3
        }
        else if (ability == "grapes")
        {
            //Debug.Log("Grapes");

            if (placedObject.health <= 0)
            {
                if (manager == unitManager) // unit grape
                {
                    Unit unit = inputManager.unitList[6];
                    int childNumber = unitManager.transform.childCount;

                    for (int i = placedObject.transform.GetSiblingIndex(); i < childNumber; i++)    //auf position auf dem feld zugreifen
                    {
                        if (inputManager.GetCellObject(i) == placedObject)
                        {
                            GridCell gridCell = inputManager.GetGridCell(i).GetComponent<GridCell>();
                            Vector2Int pos2 = gridCell.GetPosition();
                            Vector3 pos3 = new Vector3(inputManager.GetGridCell(i).transform.position.x, 1f, inputManager.GetGridCell(i).transform.position.z);

                            // deactivate grape
                            placedObject.gameObject.SetActive(false);
                            inputManager.GetGridCell(i).GetComponent<GridCell>().UnstoreObject(placedObject);

                            // spawn mini grape
                            PlacedObject placedO = PlacedObject.Create(pos3, pos2, Unit.Dir.Down, unit);
                            gridCell.SetPlacedObject(placedO);
                            gridCell.StoreObject(placedO);
                            placedO.transform.SetParent(manager.transform);
                            placedO.SettingStats();
                            inputManager.ShowFloatingText(placedO, pos3);
                            Debug.Log("spawned mini Grape");
                            childNumber = 10;
                        }
                        else
                        {
                            Debug.Log("No free Grid");
                        }
                    }
                }
                
                else if (manager == enemyManager)   // enemy garlic
                {
                    Unit unit = inputManager.unitList[6];   // GetCellObject zu GetEnemyObject  GetGridCell zu GetEnemyCell
                    int childNumber = enemyManager.transform.childCount;

                    for (int i = 0; i < childNumber; i++)
                    {
                        if (inputManager.GetEnemyObject(i) == placedObject)
                        {
                            GridCell gridCell = inputManager.GetEnemyCell(i).GetComponent<GridCell>();
                            Vector2Int pos2 = gridCell.GetPosition();
                            Vector3 pos3 = new Vector3(inputManager.GetEnemyCell(i).transform.position.x, 1f, inputManager.GetEnemyCell(i).transform.position.z);

                            // deactivate unit
                            Destroy(placedObject.gameObject);

                            // spawn mini grape
                            PlacedObject placedO = PlacedObject.Create(pos3, pos2, Unit.Dir.Down, unit);
                            gridCell.SetPlacedObject(placedO);
                            gridCell.StoreObject(placedO);
                            placedO.transform.SetParent(manager.transform);
                            placedO.SettingStats();
                            inputManager.ShowFloatingText(placedO, pos3);
                            Debug.Log("spawned mini");
                        }
                        else
                        {
                            Debug.Log("No free grid");
                        }
                    }
                }

                else
                {
                    Debug.Log("No Manager found");
                }
            }
        }
        else if (ability == "mini grapes")
        {
            if (placedObject.health <= 0)
            {
                Destroy(placedObject.gameObject);
                Debug.Log("Mini grape");
            }
        }
        else if (ability == "coconut")
        {
            //Debug.Log("Coconut");

            if (placedObject.armor == true)
            {
                placedObject.health = placedObject.baseHealth;
                placedObject.armor = false;
                inputManager.UpdateFloatingText(placedObject);
                Debug.Log("coconut armor");
            }
        }
        else if (ability == "lemon")
        {
            //Debug.Log("Lemon");

            if ((placedObject.health != placedObject.baseHealth) && (placedObject.health > 0))
            {
                int randomOption = Random.Range(0, unitManager.transform.childCount);   // 0 dabei, max childcount ( in dem fall 3) nicht dabei
                GameObject buffedUnit = unitManager.transform.GetChild(randomOption).gameObject;
                int numberOfUnitsOnField = NumberOfUnitsOnField(enemy);

                Debug.Log(placedObject.transform.GetSiblingIndex());

                if (numberOfUnitsOnField <= 1)
                {
                    //Debug.Log("No unit to buff");
                }
                else
                {
                    while ((randomOption == placedObject.transform.GetSiblingIndex()) || (buffedUnit.activeInHierarchy == false))
                    {
                        randomOption = Random.Range(0, unitManager.transform.childCount);
                        buffedUnit = unitManager.transform.GetChild(randomOption).gameObject;
                    }

                    buffedUnit.GetComponent<PlacedObject>().baseAttack += 1;
                    buffedUnit.GetComponent<PlacedObject>().baseHealth += 1;
                    buffedUnit.GetComponent<PlacedObject>().attack += 1;
                    buffedUnit.GetComponent<PlacedObject>().health += 1;
                    inputManager.UpdateFloatingText(buffedUnit.GetComponent<PlacedObject>());
                    Debug.Log("lemon ability: " + buffedUnit);
                }
            }
        }
        else
        {
            //Debug.Log("No ability");
        }
    }

    public void CheckAbilityAttack(PlacedObject placedObject, GameObject manager, bool enemy)   // enemyManager bei cherry und unitManager bei garlic : enemy bei cherry true und bei garlic false
    {
        string ability = GetAbility(placedObject);
        //Debug.Log("Check Ability");

        if (ability == "cherry")
        {
            //Debug.Log("Cherry");
            int numberOfUnitsOnField = NumberOfUnitsOnField(enemy);
            //Debug.Log(numberOfUnitsOnField);
            //if (manager.transform.GetChild(1) != null)
            if (numberOfUnitsOnField > 1)
            {
                PlacedObject affectedUnit = null;

                if (manager == unitManager)
                {
                    affectedUnit = inputManager.GetCellObject(1).GetComponent<PlacedObject>();
                }
                else if (manager == enemyManager)
                {
                    affectedUnit = inputManager.GetEnemyObject(1).GetComponent<PlacedObject>();
                }
                affectedUnit.health -= 2;
                inputManager.UpdateFloatingText(affectedUnit);
                CheckAbilityDefense(affectedUnit, manager, enemy);
                Debug.Log("Cherry eingesetzt");
            }
            else
            {
                //Debug.Log("No Target");
            }
        }
        else
        {
            //Debug.Log("No ability");
        }
    }

    public int NumberOfUnitsOnField(bool enemy)
    {
        int numberOfUnitsOnField = 0;

        if ((CheckObjectInGridCell(0, enemy) != null) && (CheckObjectInGridCell(0, enemy).gameObject.activeInHierarchy == true))
        {
            numberOfUnitsOnField += 1;
        }
        if ((CheckObjectInGridCell(1, enemy) != null) && (CheckObjectInGridCell(1, enemy).gameObject.activeInHierarchy == true))
        {
            numberOfUnitsOnField += 1;
        }
        if ((CheckObjectInGridCell(2, enemy) != null) && (CheckObjectInGridCell(1, enemy).gameObject.activeInHierarchy == true))
        {
            numberOfUnitsOnField += 1;
        }
        return numberOfUnitsOnField;
    }
}
