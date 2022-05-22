using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRound : MonoBehaviour
{
    public InputManager inputManager;
    public string gameState = null;
    public string gameState2 = null;
    public bool round2 = false;
    public GameObject gameGridEnemy;
    public GameObject unitManager;
    public GameObject enemyManager;
    //public Animator animatorUnit;

    public void StartBattle()
    {
        //Debug.Log("test");
        HealthManager(3f, 1);
        gameState = null;
        gameState2 = null;
    }

    private void Update()
    {
        if ((inputManager.roundCounter >= 3) && (round2 == true))
        {
            round2 = false;
            Debug.Log("2222222222222");
            HealthManager(3f, 2);
        }
    }

    public void HealthManager(float delayTime, int lane)
    {
        //Debug.Log("Battle Start");
        //Debug.Log("test 2");
        StartCoroutine(DelayAction(delayTime, lane));
    }

    IEnumerator DelayAction(float delayTime, int lane)
    {
        //Debug.Log("test 3");
        while (true)
        {
            //Debug.Log("test 4");
            CheckingGridBattle(lane);
            //Debug.Log("test 5");
            CheckingGameState(lane);
            //Debug.Log("In while");

            if (lane == 1)
            {
                PlacedObject enemy = inputManager.GetEnemyObject(0);
                PlacedObject teamObject = inputManager.GetCellObject(0);

                if (teamObject != null && enemy != null)
                {
                    yield return new WaitForSeconds(.2f);

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
                    yield return new WaitForSeconds(.2f);
                    CheckAllUnitsOnDefeat();
                    yield return new WaitForSeconds(.2f);
                    Debug.Log(inputManager.GetCellObject(1));
                    CheckingGridBattle(lane);
                    Debug.Log("Check");
                    yield return new WaitForSeconds(.2f);
                    CheckingGameState(lane);
                }
            }
            else if (lane == 2)
            {
                PlacedObject enemy = inputManager.GetEnemyObject(4);
                PlacedObject teamObject = inputManager.GetCellObject(4);

                if (gameState2 == "Win" || gameState2 == "Lose" || gameState2 == "Draw")
                {
                    EndOfRound();
                    inputManager.roundCounter = 10;
                    break;
                }

                if (teamObject != null && enemy != null)
                {
                    yield return new WaitForSeconds(.2f);

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
                    yield return new WaitForSeconds(.2f);
                    CheckAllUnitsOnDefeat();
                    yield return new WaitForSeconds(.2f);
                    Debug.Log(inputManager.GetCellObject(1));
                    CheckingGridBattle(lane);
                    Debug.Log("Check");
                    yield return new WaitForSeconds(.2f);
                    CheckingGameState(lane);
                }
            }
            if (lane == 1)
            {
                if (gameState == "Win")
                {
                    //EndOfRound();
                    //inputManager.roundCounter += 1;
                    Debug.Log("ROUND 2");
                    round2 = true;
                    break;
                }

                else if (gameState == "Lose")
                {
                    //EndOfRound();
                    //inputManager.roundCounter = 1;
                    Debug.Log("ROUND 2");
                    round2 = true;
                    break;
                }

                else if (gameState == "Draw")
                {
                    inputManager.draws -= 1;

                    if (inputManager.draws > 0)
                    {
                        //EndOfRound();
                        //inputManager.roundCounter += 0;
                        Debug.Log("ROUND 2");
                        round2 = true;
                        break;
                    }
                    else
                    {
                        //EndOfRound();
                        //inputManager.roundCounter = 1;
                        Debug.Log("ROUND 2");
                        round2 = true;
                        break;
                    }
                }
            }
            if (lane== 2)
            {
                if (gameState2 == "Win")
                {
                    EndOfRound();
                    inputManager.roundCounter += 1;
                    break;
                }

                else if (gameState2 == "Lose")
                {
                    EndOfRound();
                    inputManager.roundCounter = 1;
                    break;
                }

                else if (gameState2 == "Draw")
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
        }
        Debug.Log("TT");
        yield return new WaitForSeconds(delayTime); // letzter change
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

    public void CheckingGameState(int lane) // lane 1: toplane - lane 2: botlane - lane 3: final battle
    {
        if (lane == 1)
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
            else if (CheckObjectInGridCell(0, false) == null && CheckObjectInGridCell(1, false) == null && CheckObjectInGridCell(2, false) == null)
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
        if (lane == 2)  // 0=3, 1=4, 2=5    gamestate=gamestate2
        {
            if (CheckObjectInGridCell(3, true) == null && CheckObjectInGridCell(4, true) == null && CheckObjectInGridCell(5, true) == null)
            {
                if (CheckObjectInGridCell(3, false) == null && CheckObjectInGridCell(4, false) == null && CheckObjectInGridCell(5, false) == null)
                {
                    Debug.Log("Draw");
                    gameState2 = "Draw";
                }

                else
                {
                    Debug.Log("Win");
                    gameState2 = "Win";
                }
            }
            else if (CheckObjectInGridCell(3, false) == null && CheckObjectInGridCell(4, false) == null && CheckObjectInGridCell(5, false) == null)
            {
                if (CheckObjectInGridCell(3, true) == null && CheckObjectInGridCell(4, true) == null && CheckObjectInGridCell(5, true) == null)
                {
                    Debug.Log("Draw");
                    gameState2 = "Draw";
                }

                else
                {
                    Debug.Log("Lose");
                    gameState2 = "Lose";
                }
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

    public void CheckingGridBattle(int lane)    // lane 1: toplane - lane 2: botlane - lane 3: final battle
    {
        // lane 1
        if (lane == 1)
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
        if (lane == 2)  // 0=3, 1=4, 2=5
        {
            if (CheckObjectInGridCell(3, false) == null && CheckObjectInGridCell(4, false) != null)
            {
                UnitMove(4, 3, false);
            }

            if (CheckObjectInGridCell(4, false) == null && CheckObjectInGridCell(5, false) != null)
            {
                if (CheckObjectInGridCell(3, false) == null)
                {
                    UnitMove(5, 3, false);
                }
                else
                {
                    UnitMove(5, 4, false);
                }

            }

            if (CheckObjectInGridCell(3, true) == null && CheckObjectInGridCell(4, true) != null)
            {
                UnitMove(4, 3, true);
            }

            if (CheckObjectInGridCell(4, true) == null && CheckObjectInGridCell(5, true) != null)
            {
                if (CheckObjectInGridCell(3, true) == null)
                {
                    UnitMove(5, 3, true);
                }
                else
                {
                    UnitMove(5, 4, true);
                }
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
                if (placedObject.transform.GetSiblingIndex() < 2)
                {
                    int index = placedObject.transform.GetSiblingIndex() + 1;
                    //Debug.Log(index);

                    if (inputManager.GetCellObject(index) == null)
                    {
                        Debug.Log("No unit to buff");
                    }
                    else if (inputManager.GetCellObject(index).gameObject.activeInHierarchy == true)
                    {
                        inputManager.GetCellObject(index).baseAttack += 2;
                        inputManager.GetCellObject(index).baseHealth += 1;
                        inputManager.GetCellObject(index).attack += 2;
                        inputManager.GetCellObject(index).health += 1;
                        inputManager.UpdateFloatingText(inputManager.GetCellObject(index));
                        Debug.Log("apple: " + inputManager.GetCellObject(index));
                    }
                    else if (inputManager.GetCellObject(index).gameObject.activeInHierarchy == false)
                    {
                        index += 1;

                        if (inputManager.GetCellObject(index) != null)
                        {
                            if (inputManager.GetCellObject(index).gameObject.activeInHierarchy == true)
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
                    else
                    {
                        Debug.Log("No unit to buff");
                    }
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
                    GameObject gridCellGameObject = placedObject.AttachedGridCell(false);
                    Debug.Log(gridCellGameObject);
                    GridCell gridCell = gridCellGameObject.GetComponent<GridCell>();
                    Vector2Int pos2 = gridCell.GetPosition();
                    Debug.Log(pos2);
                    Vector3 pos3 = new Vector3(gridCellGameObject.transform.position.x, 1f, gridCellGameObject.transform.position.z);
                    Debug.Log(pos3);

                    
                    // deactivate grape
                    placedObject.gameObject.SetActive(false);
                    gridCell.UnstoreObject(placedObject);

                    
                    // spawn mini grape
                    PlacedObject placedO = PlacedObject.Create(pos3, pos2, Unit.Dir.Down, unit);
                    gridCell.SetPlacedObject(placedO);
                    gridCell.StoreObject(placedO);
                    placedO.transform.SetParent(manager.transform);
                    placedO.SettingStats();
                    inputManager.ShowFloatingText(placedO, pos3);
                    Debug.Log("spawned mini grape");
                }

                else if (manager == enemyManager)   // enemy garlic
                {
                    Unit unit = inputManager.unitList[6];
                    GameObject gridCellGameObject = placedObject.AttachedGridCell(true);
                    Debug.Log(gridCellGameObject);
                    GridCell gridCell = gridCellGameObject.GetComponent<GridCell>();
                    Vector2Int pos2 = gridCell.GetPosition();
                    Debug.Log(pos2);
                    Vector3 pos3 = new Vector3(gridCellGameObject.transform.position.x, 1f, gridCellGameObject.transform.position.z);
                    Debug.Log(pos3);


                    // deactivate grape
                    placedObject.gameObject.SetActive(false);
                    gridCell.UnstoreObject(placedObject);


                    // spawn mini grape
                    PlacedObject placedO = PlacedObject.Create(pos3, pos2, Unit.Dir.Down, unit);
                    gridCell.SetPlacedObject(placedO);
                    gridCell.StoreObject(placedO);
                    placedO.transform.SetParent(manager.transform);
                    placedO.SettingStats();
                    inputManager.ShowFloatingText(placedO, pos3);
                    Debug.Log("spawned mini grape");
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
                if (placedObject.level < 7)
                {
                    placedObject.health = placedObject.baseHealth;
                    placedObject.armor = false;
                    inputManager.UpdateFloatingText(placedObject);
                    Debug.Log("coconut armor");
                }
                if (placedObject.level >= 7)
                {
                    if (placedObject.armorTriggerCounter < 1)
                    {
                        placedObject.health = placedObject.baseHealth;
                        inputManager.UpdateFloatingText(placedObject);
                        placedObject.armorTriggerCounter += 1;
                        Debug.Log("coconut armor");
                    }
                    else if (placedObject.armorTriggerCounter >= 1)
                    {
                        placedObject.health = placedObject.baseHealth;
                        placedObject.armor = false;
                        placedObject.armorTriggerCounter += 1;
                        inputManager.UpdateFloatingText(placedObject);
                        Debug.Log("coconut armor");
                    }
                }
            }
        }
        else if (ability == "lemon")
        {
            //Debug.Log("Lemon");

            if ((placedObject.health != placedObject.baseHealth) && (placedObject.health > 0))
            {
                int randomOption = Random.Range(0, unitManager.transform.childCount);   // 0 dabei, max childcount ( in dem fall 3) nicht dabei
                int randomOption2 = Random.Range(0, unitManager.transform.childCount);
                GameObject buffedUnit = unitManager.transform.GetChild(randomOption).gameObject;
                GameObject buffedUnit2 = unitManager.transform.GetChild(randomOption2).gameObject;
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
                    while ((randomOption2 == placedObject.transform.GetSiblingIndex()) || (buffedUnit.activeInHierarchy == false))
                    {
                        randomOption2 = Random.Range(0, unitManager.transform.childCount);
                        buffedUnit2 = unitManager.transform.GetChild(randomOption2).gameObject;
                    }

                    if (placedObject.level < 4)
                    {
                        buffedUnit.GetComponent<PlacedObject>().baseAttack += 1;
                        buffedUnit.GetComponent<PlacedObject>().baseHealth += 1;
                        buffedUnit.GetComponent<PlacedObject>().attack += 1;
                        buffedUnit.GetComponent<PlacedObject>().health += 1;
                        inputManager.UpdateFloatingText(buffedUnit.GetComponent<PlacedObject>());
                        Debug.Log("lemon ability: " + buffedUnit);
                    }
                    if ((placedObject.level >= 4) && (placedObject.level < 7))
                    {
                        buffedUnit.GetComponent<PlacedObject>().baseAttack += 2;
                        buffedUnit.GetComponent<PlacedObject>().baseHealth += 2;
                        buffedUnit.GetComponent<PlacedObject>().attack += 2;
                        buffedUnit.GetComponent<PlacedObject>().health += 2;
                        inputManager.UpdateFloatingText(buffedUnit.GetComponent<PlacedObject>());
                        Debug.Log("lemon ability: " + buffedUnit);
                    }
                    if (placedObject.level >= 7)
                    {
                        buffedUnit.GetComponent<PlacedObject>().baseAttack += 4;
                        buffedUnit.GetComponent<PlacedObject>().baseHealth += 4;
                        buffedUnit.GetComponent<PlacedObject>().attack += 4;
                        buffedUnit.GetComponent<PlacedObject>().health += 4;
                        inputManager.UpdateFloatingText(buffedUnit.GetComponent<PlacedObject>());
                        Debug.Log("lemon ability: " + buffedUnit);

                        buffedUnit2.GetComponent<PlacedObject>().baseAttack += 4;
                        buffedUnit2.GetComponent<PlacedObject>().baseHealth += 4;
                        buffedUnit2.GetComponent<PlacedObject>().attack += 4;
                        buffedUnit2.GetComponent<PlacedObject>().health += 4;
                        inputManager.UpdateFloatingText(buffedUnit2.GetComponent<PlacedObject>());
                        Debug.Log("lemon ability: " + buffedUnit2);
                    }
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
                if (placedObject.level < 4)
                {
                    affectedUnit.health -= 2;
                    inputManager.UpdateFloatingText(affectedUnit);
                    CheckAbilityDefense(affectedUnit, manager, enemy);
                    Debug.Log("LEVEL 1 " + affectedUnit);
                    Debug.Log(affectedUnit.health);
                }
                if ((placedObject.level >= 4) && (placedObject.level < 7))
                {
                    affectedUnit.health -= 6;
                    inputManager.UpdateFloatingText(affectedUnit);
                    CheckAbilityDefense(affectedUnit, manager, enemy);
                    Debug.Log("LEVEL 4");
                }
                if (placedObject.level >= 7)
                {
                    affectedUnit.health -= 12;
                    inputManager.UpdateFloatingText(affectedUnit);
                    CheckAbilityDefense(affectedUnit, manager, enemy);
                    Debug.Log("LEVEL 7");
                }

                Debug.Log("Cherry eingesetzt");

                if ((affectedUnit != null) && (affectedUnit.health <= 0))
                {
                    if (manager == unitManager)
                    {
                        affectedUnit.gameObject.SetActive(false);
                        //inputManager.GetGridCell(1).GetComponent<GridCell>().UnstoreObject(affectedUnit); ----- wenn objekt unstored wird entsteht bugg mit mini grape
                    }
                    else if (manager == enemyManager)
                    {
                        affectedUnit.DestroySelf();
                        //inputManager.GetEnemyCell(1).GetComponent<GridCell>().UnstoreObject(affectedUnit);
                    }
                }
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

    public void CheckAllUnitsOnDefeat()
    {
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
    }

    public void GameState(int lane)
    {
        string state;

        if (lane == 1)
        {
            state = gameState;   
        }
        if (lane == 2)
        {
            state = gameState2;
        }
    }
}
