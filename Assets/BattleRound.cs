using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleRound : MonoBehaviour
{
    public InputManager inputManager;
    public string gameState = null;
    public string gameState2 = null;
    public string finalGameState = null;
    public bool round2 = false;
    public GameObject gameGridEnemy;
    public GameObject unitManager;
    public GameObject enemyManager;

    [SerializeField] private GameObject buytheme;
    [SerializeField] private GameObject battletheme;
    //public Animator animatorUnit;

    public void StartBattle()
    {
        HealthManager(3f, 1);
        gameState = null;
        gameState2 = null;
    }

    public void HealthManager(float delayTime, int lane)
    {
        StartCoroutine(DelayAction(delayTime, lane));
    }

    IEnumerator DelayAction(float delayTime, int lane)
    {
        while (true)
        {
            CheckingGridBattle(lane);
            yield return new WaitForSeconds(0.5f);
            CheckingGameState(lane);

            if (lane == 1)
            {
                PlacedObject enemy = inputManager.GetEnemyObject(0);
                PlacedObject teamObject = inputManager.GetCellObject(0);

                if (teamObject != null && enemy != null)
                {
                    yield return new WaitForSeconds(.2f);
                    StartCoroutine(inputManager.unitAnimation.AnimationAttack(1));
                    yield return new WaitForSeconds(.2f);
                    
                    // Unit greift Enemy an

                    enemy.GettingDamaged(teamObject.attack, teamObject);
                    inputManager.UpdateFloatingText(enemy);
                    CheckAbilityAttack(teamObject, enemyManager, true);   // überprüft ability vom Angreifer
                    CheckAbilityDefense(enemy, enemyManager, true);
                    //yield return new WaitForSeconds(1f);  // für tests

                    // Enemy greift Unit an
                    teamObject.GettingDamaged(enemy.attack, enemy);
                    if ((teamObject.health <= 0) || (enemy.health <= 0))
                    {
                        FindObjectOfType<AudioManager>().Play("UnitDefeat");
                        //Debug.Log("Defeat");
                    }
                    else
                    {
                        FindObjectOfType<AudioManager>().Play("UnitHit");
                        //Debug.Log("Hit");
                    }
                    inputManager.CameraShake();

                    inputManager.UpdateFloatingText(teamObject);
                    CheckAbilityAttack(enemy, unitManager, false);   // überprüft ability vom Angreifer
                    CheckAbilityDefense(teamObject, unitManager, false);

                    yield return new WaitForSeconds(1f);
                    CheckAllUnitsOnDefeat(lane);

                    yield return new WaitForSeconds(0.5f);
                    CheckingGridBattle(lane);

                    CheckingGameState(lane);
                }
            }

            if (lane == 2)
            {
                PlacedObject enemy = inputManager.GetEnemyObject(3);
                PlacedObject teamObject = inputManager.GetCellObject(3);

                if (teamObject != null && enemy != null)
                {
                    yield return new WaitForSeconds(.2f);
                    StartCoroutine(inputManager.unitAnimation.AnimationAttack(2));
                    yield return new WaitForSeconds(.2f);

                    // Unit greift Enemy an
                    FindObjectOfType<AudioManager>().Play("UnitHit");
                    enemy.GettingDamaged(teamObject.attack, teamObject);
                    inputManager.UpdateFloatingText(enemy);
                    CheckAbilityAttack(teamObject, enemyManager, true);   // überprüft ability vom Angreifer
                    CheckAbilityDefense(enemy, enemyManager, true);

                    //yield return new WaitForSeconds(1f);  // für tests

                    // Enemy greift Unit an
                    teamObject.GettingDamaged(enemy.attack, enemy);

                    if ((teamObject.health <= 0) || (enemy.health <= 0))
                    {
                        FindObjectOfType<AudioManager>().Play("UnitDefeat");
                        //Debug.Log("Defeat");
                    }
                    else
                    {
                        FindObjectOfType<AudioManager>().Play("UnitHit");
                        //Debug.Log("Hit");
                    }
                    inputManager.CameraShake();

                    inputManager.UpdateFloatingText(teamObject);
                    CheckAbilityAttack(enemy, unitManager, false);   // überprüft ability vom Angreifer
                    CheckAbilityDefense(teamObject, unitManager, false);

                    yield return new WaitForSeconds(1f);
                    CheckAllUnitsOnDefeat(lane);

                    yield return new WaitForSeconds(0.5f);
                    CheckingGridBattle(lane);

                    CheckingGameState(lane);
                }
            }

            if (lane == 3)  // final battle on toplane
            {
                PlacedObject enemy = inputManager.GetEnemyObject(0);
                PlacedObject teamObject = inputManager.GetCellObject(0);

                if (teamObject != null && enemy != null)
                {
                    yield return new WaitForSeconds(.2f);
                    StartCoroutine(inputManager.unitAnimation.AnimationAttack(1));
                    yield return new WaitForSeconds(.2f);

                    // Unit greift Enemy an
                    FindObjectOfType<AudioManager>().Play("UnitHit");
                    enemy.GettingDamaged(teamObject.attack, teamObject);
                    inputManager.UpdateFloatingText(enemy);
                    CheckAbilityAttack(teamObject, enemyManager, true);   // überprüft ability vom Angreifer
                    CheckAbilityDefense(enemy, enemyManager, true);
                    //yield return new WaitForSeconds(1f);  // für tests

                    // Enemy greift Unit an
                    teamObject.GettingDamaged(enemy.attack, enemy);
                    if ((teamObject.health <= 0) || (enemy.health <= 0))
                    {
                        FindObjectOfType<AudioManager>().Play("UnitDefeat");
                        //Debug.Log("Defeat");
                    }
                    else
                    {
                        FindObjectOfType<AudioManager>().Play("UnitHit");
                        //Debug.Log("Hit");
                    }
                    inputManager.CameraShake();

                    inputManager.UpdateFloatingText(teamObject);
                    CheckAbilityAttack(enemy, unitManager, false);   // überprüft ability vom Angreifer
                    CheckAbilityDefense(teamObject, unitManager, false);

                    yield return new WaitForSeconds(1f);
                    CheckAllUnitsOnDefeat(lane);

                    yield return new WaitForSeconds(0.5f);
                    CheckingGridBattle(lane);

                    CheckingGameState(lane);
                }
            }

            if (lane == 4)  // final battle on botlane
            {
                PlacedObject enemy1 = inputManager.GetEnemyObject(3);
                PlacedObject teamObject1 = inputManager.GetCellObject(3);

                if (teamObject1 != null && enemy1 != null)
                {
                    yield return new WaitForSeconds(.2f);
                    StartCoroutine(inputManager.unitAnimation.AnimationAttack(2));
                    yield return new WaitForSeconds(.2f);

                    // Unit greift Enemy an
                    FindObjectOfType<AudioManager>().Play("UnitHit");
                    enemy1.GettingDamaged(teamObject1.attack, teamObject1);
                    inputManager.UpdateFloatingText(enemy1);
                    CheckAbilityAttack(teamObject1, enemyManager, true);   // überprüft ability vom Angreifer
                    CheckAbilityDefense(enemy1, enemyManager, true);
                    //yield return new WaitForSeconds(1f);  // für tests

                    // Enemy greift Unit an
                    teamObject1.GettingDamaged(enemy1.attack, enemy1);
                    if ((teamObject1.health <= 0) || (enemy1.health <= 0))
                    {
                        FindObjectOfType<AudioManager>().Play("UnitDefeat");
                        //Debug.Log("Defeat");
                    }
                    else
                    {
                        FindObjectOfType<AudioManager>().Play("UnitHit");
                        //Debug.Log("Hit");
                    }
                    inputManager.CameraShake();

                    inputManager.UpdateFloatingText(teamObject1);
                    CheckAbilityAttack(enemy1, unitManager, false);   // überprüft ability vom Angreifer
                    CheckAbilityDefense(teamObject1, unitManager, false);

                    yield return new WaitForSeconds(1f);
                    CheckAllUnitsOnDefeat(lane);

                    yield return new WaitForSeconds(0.5f);
                    CheckingGridBattle(lane);

                    CheckingGameState(lane);
                }
            }


            yield return new WaitForSeconds(.5f);

            if (lane == 1)
            {
                if (inputManager.roundCounter >= 3)
                {
                    if ((gameState == "Win") || (gameState == "Lose") || (gameState == "Draw"))
                    {
                        //Debug.Log("ROUND 2");
                        lane = 2;
                    }
                }
                else
                {
                    if (gameState == "Win")
                    {
                        inputManager.roundCounter += 1;
                        FindObjectOfType<AudioManager>().Play("Win");
                        //yield return new WaitForSeconds(2f);

                        EndOfRound();
                        break;
                    }
                    else if(gameState == "Lose")
                    {
                        inputManager.roundCounter = 1;
                        LoseScreen();
                        //EndOfRound();
                        break;
                    }
                    else if(gameState == "Draw")
                    {
                        if (inputManager.draws > 0)
                        {
                            Debug.Log("Draw Draw");
                            inputManager.roundCounter += 1;
                            FindObjectOfType<AudioManager>().Play("Draw");
                            //yield return new WaitForSeconds(2f);

                            inputManager.draws -= 1;
                            EndOfRound();
                            break;
                        }
                        else
                        {
                            Debug.Log("Draw Draw");
                            //inputManager.roundCounter = 1;
                            FindObjectOfType<AudioManager>().Play("Draw");
                            //yield return new WaitForSeconds(2f);

                            inputManager.draws -= 1;
                            LoseScreen();
                            break;
                        }
                    }
                }
            }
            if (lane == 2)  // draw + 1 roundCounter // Lose resets to round 1 or main menu
            {
                // Win
                if ((gameState == "Win") && (gameState2 == "Win"))
                {
                    // WIN
                    inputManager.roundCounter += 1;
                    FindObjectOfType<AudioManager>().Play("Win");
                    //yield return new WaitForSeconds(2f);

                    EndOfRound();
                    break;
                }

                else if ((gameState == "Win") && (gameState2 == "Draw"))
                {
                    // WIN
                    inputManager.roundCounter += 1;
                    FindObjectOfType<AudioManager>().Play("Win");
                    //yield return new WaitForSeconds(2f);

                    EndOfRound();
                    break;
                }

                else if ((gameState == "Draw") && (gameState2 == "Win"))
                {
                    // WIN
                    inputManager.roundCounter += 1;
                    FindObjectOfType<AudioManager>().Play("Win");
                    //yield return new WaitForSeconds(2f);

                    EndOfRound();
                    break;
                }

                // Draw
                else if ((gameState == "Draw") && (gameState2 == "Draw"))
                {
                    // Draw
                    if (inputManager.draws > 0)
                    {
                        inputManager.roundCounter += 1;
                        FindObjectOfType<AudioManager>().Play("Draw");
                        //yield return new WaitForSeconds(2f);

                        inputManager.draws -= 1;
                        EndOfRound();
                        break;
                    }
                    else
                    {
                        //inputManager.roundCounter = 1;
                        LoseScreen();

                        inputManager.draws -= 1;
                        //EndOfRound();
                        break;
                    }
                }

                // Final Battle
                else if ((gameState == "Win") && (gameState2 == "Lose"))
                {
                    if (gameState == "Win")
                    {
                        // units stay on lane 1
                        // enemy units go over to lane 1
                        if (inputManager.GetEnemyCell(3).GetComponent<GridCell>().isOccupied == true)
                        {
                            UnitMove(3, 0, true);
                        }
                        if (inputManager.GetEnemyCell(4).GetComponent<GridCell>().isOccupied == true)
                        {
                            UnitMove(4, 1, true);
                        }
                        if (inputManager.GetEnemyCell(5).GetComponent<GridCell>().isOccupied == true)
                        {
                            UnitMove(5, 2, true);
                        }
                        lane = 3;
                        yield return new WaitForSeconds(2f);
                    }
                }

                else if ((gameState == "Lose") && (gameState2 == "Win"))
                {
                    if (gameState2 == "Win")
                    {
                        // units stay on lane 2
                        // enemy units go to lane 2
                        if (inputManager.GetEnemyCell(0).GetComponent<GridCell>().isOccupied == true)
                        {
                            UnitMove(0, 3, true);
                        }
                        if (inputManager.GetEnemyCell(1).GetComponent<GridCell>().isOccupied == true)
                        {
                            UnitMove(1, 4, true);
                        }
                        if (inputManager.GetEnemyCell(2).GetComponent<GridCell>().isOccupied == true)
                        {
                            UnitMove(2, 5, true);
                        }
                        lane = 4;
                        yield return new WaitForSeconds(2f);
                    }
                }

                // Lose
                else if ((gameState == "Lose") && (gameState2 == "Draw"))
                {
                    // Lose
                    inputManager.roundCounter = 1;
                    LoseScreen();
                    //EndOfRound();
                    break;
                }

                else if ((gameState == "Draw") && (gameState2 == "Lose"))
                {
                    // Lose
                    inputManager.roundCounter = 1;
                    LoseScreen();
                    //EndOfRound();
                    break;
                }

                else if ((gameState == "Lose") && (gameState2 == "Lose"))
                {
                    // Lose
                    inputManager.roundCounter = 1;
                    LoseScreen();
                    //EndOfRound();
                    break;
                }
            }

            // Win
            if (finalGameState == "Win")
            {
                Debug.Log("Winner Winner");
                inputManager.roundCounter += 1;
                FindObjectOfType<AudioManager>().Play("Win");
                //yield return new WaitForSeconds(2f);

                EndOfRound();
                break;
            }
            // Draw
            else if (finalGameState == "Draw")
            {
                if (inputManager.draws > 0)
                {
                    Debug.Log("Draw Draw");
                    inputManager.roundCounter += 1;
                    FindObjectOfType<AudioManager>().Play("Draw");
                    //yield return new WaitForSeconds(2f);

                    inputManager.draws -= 1;
                    EndOfRound();
                    break;
                }
                else
                {
                    Debug.Log("Draw Draw");
                    inputManager.roundCounter = 1;
                    LoseScreen();

                    inputManager.draws -= 1;
                    //EndOfRound();
                    break;
                }
            }
            // Lose
            else if (finalGameState == "Lose")
            {
                Debug.Log("Lose Lose");
                inputManager.roundCounter = 1;
                LoseScreen();
                //EndOfRound();
                break;
            }

        }
        yield return new WaitForSeconds(delayTime); // letzter change
    }

    public void UnitMove(int oldPos, int newPos, bool enemy)
    {
        if (enemy == false)
        {
            PlacedObject movingPlacedObject = inputManager.GetCellObject(oldPos);
            GameObject newGridCell = inputManager.GetGridCell(newPos);
            GameObject oldGridCell = inputManager.GetGridCell(oldPos);

            // animation
            //StartCoroutine(inputManager.unitAnimation.AnimationMoveForward(movingPlacedObject));

            oldGridCell.GetComponent<GridCell>().UnstoreObject(movingPlacedObject);

            newGridCell.GetComponent<GridCell>().StoreObject(movingPlacedObject);
            newGridCell.GetComponent<GridCell>().SetPlacedObject(movingPlacedObject);
            movingPlacedObject.transform.position = new Vector3(newGridCell.transform.position.x, 0f, newGridCell.transform.position.z);
        }
        else
        {
            PlacedObject movingPlacedObject = inputManager.GetEnemyObject(oldPos);
            GameObject newGridCell = inputManager.GetEnemyCell(newPos);
            GameObject oldGridCell = inputManager.GetEnemyCell(oldPos);

            oldGridCell.GetComponent<GridCell>().UnstoreObject(movingPlacedObject);

            newGridCell.GetComponent<GridCell>().StoreObject(movingPlacedObject);
            newGridCell.GetComponent<GridCell>().SetPlacedObject(movingPlacedObject);
            movingPlacedObject.transform.position = new Vector3(newGridCell.transform.position.x, 0f, newGridCell.transform.position.z);
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
                    //Debug.Log("Draw");
                    gameState = "Draw";
                }

                else
                {
                    //Debug.Log("Win");
                    gameState = "Win";
                }
            }
            else if (CheckObjectInGridCell(0, false) == null && CheckObjectInGridCell(1, false) == null && CheckObjectInGridCell(2, false) == null)
            {
                if (CheckObjectInGridCell(0, true) == null && CheckObjectInGridCell(1, true) == null && CheckObjectInGridCell(2, true) == null)
                {
                    //Debug.Log("Draw");
                    gameState = "Draw";
                }

                else
                {
                    //Debug.Log("Lose");
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
                    //Debug.Log("Draw");
                    gameState2 = "Draw";
                }

                else
                {
                    //Debug.Log("Win");
                    gameState2 = "Win";
                }
            }
            else if (CheckObjectInGridCell(3, false) == null && CheckObjectInGridCell(4, false) == null && CheckObjectInGridCell(5, false) == null)
            {
                if (CheckObjectInGridCell(3, true) == null && CheckObjectInGridCell(4, true) == null && CheckObjectInGridCell(5, true) == null)
                {
                    //Debug.Log("Draw");
                    gameState2 = "Draw";
                }

                else
                {
                    //Debug.Log("Lose");
                    gameState2 = "Lose";
                }
            }
        }
        if (lane == 3)
        {
            if (CheckObjectInGridCell(0, true) == null && CheckObjectInGridCell(1, true) == null && CheckObjectInGridCell(2, true) == null)
            {
                if (CheckObjectInGridCell(0, false) == null && CheckObjectInGridCell(1, false) == null && CheckObjectInGridCell(2, false) == null)
                {
                    //Debug.Log("Draw");
                    finalGameState = "Draw";
                }

                else
                {
                    //Debug.Log("Win");
                    finalGameState = "Win";
                }
            }
            else if (CheckObjectInGridCell(0, false) == null && CheckObjectInGridCell(1, false) == null && CheckObjectInGridCell(2, false) == null)
            {
                if (CheckObjectInGridCell(0, true) == null && CheckObjectInGridCell(1, true) == null && CheckObjectInGridCell(2, true) == null)
                {
                    //Debug.Log("Draw");
                    finalGameState = "Draw";
                }

                else
                {
                    //Debug.Log("Lose");
                    finalGameState = "Lose";
                }
            }
        }
        if (lane == 4)
        {
            if (CheckObjectInGridCell(3, true) == null && CheckObjectInGridCell(4, true) == null && CheckObjectInGridCell(5, true) == null)
            {
                if (CheckObjectInGridCell(3, false) == null && CheckObjectInGridCell(4, false) == null && CheckObjectInGridCell(5, false) == null)
                {
                    //Debug.Log("Draw");
                    finalGameState = "Draw";
                }

                else
                {
                    //Debug.Log("Win");
                    finalGameState = "Win";
                }
            }
            else if (CheckObjectInGridCell(3, false) == null && CheckObjectInGridCell(4, false) == null && CheckObjectInGridCell(5, false) == null)
            {
                if (CheckObjectInGridCell(3, true) == null && CheckObjectInGridCell(4, true) == null && CheckObjectInGridCell(5, true) == null)
                {
                    //Debug.Log("Draw");
                    finalGameState = "Draw";
                }

                else
                {
                    //Debug.Log("Lose");
                    finalGameState = "Lose";
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
            //Debug.Log(pos);
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

        // lane 3
        if (lane == 3)
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
        if (lane == 4)  // 0=3, 1=4, 2=5
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

    public void SetUpBuyingPhase()  // anders machen
    {
        int childNumb = unitManager.transform.childCount;
        //Debug.Log("First child count: " + childNumb);

        // Destroy Mini grapes and pineapples

        //
        int childs = unitManager.transform.childCount;
        for (int i = childs - 1; i > 0; i--)
        {
            if ((unitManager.transform.GetChild(i).gameObject.name == "Mini Grape(Clone)") || (unitManager.transform.GetChild(i).GetComponent<PlacedObject>().ability == "mini grapes"))
                GameObject.Destroy(unitManager.transform.GetChild(i).gameObject);
        }
        //

        if (inputManager.pOPosition1 != null)
        {
            PlacedObject placeInTeam = inputManager.pOPosition1;
            GridCell gridCell = inputManager.GetGridCell(0).GetComponent<GridCell>();

            placeInTeam.gameObject.SetActive(true);
            placeInTeam.SetStats();
            inputManager.UpdateFloatingText(placeInTeam);
            placeInTeam.transform.position = new Vector3(gridCell.transform.position.x, 0f, gridCell.transform.position.z);
            gridCell.StoreObject(placeInTeam);
            gridCell.SetPlacedObject(placeInTeam);
        }
        if (inputManager.pOPosition2 != null)
        {
            PlacedObject placeInTeam = inputManager.pOPosition2;
            GridCell gridCell = inputManager.GetGridCell(1).GetComponent<GridCell>();

            placeInTeam.gameObject.SetActive(true);
            placeInTeam.SetStats();
            inputManager.UpdateFloatingText(placeInTeam);
            placeInTeam.transform.position = new Vector3(gridCell.transform.position.x, 0f, gridCell.transform.position.z);
            gridCell.StoreObject(placeInTeam);
            gridCell.SetPlacedObject(placeInTeam);
        }
        if (inputManager.pOPosition3 != null)
        {
            PlacedObject placeInTeam = inputManager.pOPosition3;
            GridCell gridCell = inputManager.GetGridCell(2).GetComponent<GridCell>();

            placeInTeam.gameObject.SetActive(true);
            placeInTeam.SetStats();
            inputManager.UpdateFloatingText(placeInTeam);
            placeInTeam.transform.position = new Vector3(gridCell.transform.position.x, 0f, gridCell.transform.position.z);
            gridCell.StoreObject(placeInTeam);
            gridCell.SetPlacedObject(placeInTeam);
        }
        if (inputManager.pOPosition4 != null)
        {
            PlacedObject placeInTeam = inputManager.pOPosition4;
            GridCell gridCell = inputManager.GetGridCell(3).GetComponent<GridCell>();

            placeInTeam.gameObject.SetActive(true);
            placeInTeam.SetStats();
            inputManager.UpdateFloatingText(placeInTeam);
            placeInTeam.transform.position = new Vector3(gridCell.transform.position.x, 0f, gridCell.transform.position.z);
            gridCell.StoreObject(placeInTeam);
            gridCell.SetPlacedObject(placeInTeam);
        }
        if (inputManager.pOPosition5 != null)
        {
            PlacedObject placeInTeam = inputManager.pOPosition5;
            GridCell gridCell = inputManager.GetGridCell(4).GetComponent<GridCell>();

            placeInTeam.gameObject.SetActive(true);
            placeInTeam.SetStats();
            inputManager.UpdateFloatingText(placeInTeam);
            placeInTeam.transform.position = new Vector3(gridCell.transform.position.x, 0f, gridCell.transform.position.z);
            gridCell.StoreObject(placeInTeam);
            gridCell.SetPlacedObject(placeInTeam);
        }
        if (inputManager.pOPosition6 != null)
        {
            PlacedObject placeInTeam = inputManager.pOPosition6;
            GridCell gridCell = inputManager.GetGridCell(5).GetComponent<GridCell>();

            placeInTeam.gameObject.SetActive(true);
            placeInTeam.SetStats();
            inputManager.UpdateFloatingText(placeInTeam);
            placeInTeam.transform.position = new Vector3(gridCell.transform.position.x, 0f, gridCell.transform.position.z);
            gridCell.StoreObject(placeInTeam);
            gridCell.SetPlacedObject(placeInTeam);
        }
    }

    public void EndOfRound()
    {
        inputManager.battleOn = false;
        inputManager.UpdateCanvas(2);
        inputManager.DestroyField();
        gameState = null;
        gameState2 = null;
        finalGameState = null;
        SetUpBuyingPhase();
        inputManager.shop.ShopReroll();

        if (inputManager.draws == 0)
        {
            inputManager.drawsO.SetActive(false);
        }

        //Deactivate Battletheme, Play Buytheme
        buytheme.SetActive(true);
        battletheme.SetActive(false);
    }

    private void LoseScreen()
    {
        inputManager.canvas.SetActive(false);
        inputManager.canvasLose.transform.GetChild(0).gameObject.SetActive(true);
        battletheme.SetActive(false);        
        FindObjectOfType<AudioManager>().Play("Lose");
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
                GameObject bufferGridCell = placedObject.AttachedGridCell(enemy);
                //Debug.Log(placedObject.AssignedGridCell());
                int siblingIndex = placedObject.AssignedGridCell().transform.GetSiblingIndex();

                if (inputManager.roundCounter < 3)  // only one lane pre round 3
                {
                    if (placedObject.level < 4)
                    {
                        //Debug.Log(inputManager.GetCellObject(siblingIndex + 1));
                        PlacedObject appleBuffedUnit = inputManager.GetCellObject(siblingIndex + 1);
                        if ((appleBuffedUnit != null) && (appleBuffedUnit.gameObject.activeInHierarchy == true))   // buffing the unit behind if active
                        {
                            appleBuffedUnit.baseAttack += 2;
                            appleBuffedUnit.baseHealth += 1;
                            appleBuffedUnit.attack += 2;
                            appleBuffedUnit.health += 1;
                            inputManager.UpdateFloatingText(appleBuffedUnit);

                            appleBuffedUnit.transform.GetChild(2).gameObject.SetActive(true);
                            FindObjectOfType<AudioManager>().Play("Buff2");
                            //Debug.Log("apple: " + inputManager.GetCellObject(siblingIndex + 1));
                        }
                    }
                    else if (placedObject.level < 7)
                    {
                        PlacedObject appleBuffedUnit = inputManager.GetCellObject(siblingIndex + 1);
                        if ((appleBuffedUnit != null) && (appleBuffedUnit.gameObject.activeInHierarchy == true))   // buffing the unit behind if active
                        {
                            appleBuffedUnit.baseAttack += 3;
                            appleBuffedUnit.baseHealth += 2;
                            appleBuffedUnit.attack += 3;
                            appleBuffedUnit.health += 2;
                            inputManager.UpdateFloatingText(appleBuffedUnit);

                            appleBuffedUnit.transform.GetChild(2).gameObject.SetActive(true);
                            FindObjectOfType<AudioManager>().Play("Buff2");
                            //Debug.Log("apple: " + inputManager.GetCellObject(siblingIndex + 1));
                        }
                        if (siblingIndex - 1 >= 0)
                        {
                            PlacedObject appleBuffedUnitTwo = inputManager.GetCellObject(siblingIndex - 1);
                            if ((appleBuffedUnitTwo != null) && (appleBuffedUnitTwo.gameObject.activeInHierarchy == true))   // buffing unit in front
                            {
                                appleBuffedUnitTwo.baseAttack += 3;
                                appleBuffedUnitTwo.baseHealth += 2;
                                appleBuffedUnitTwo.attack += 3;
                                appleBuffedUnitTwo.health += 2;
                                inputManager.UpdateFloatingText(appleBuffedUnitTwo);

                                appleBuffedUnitTwo.transform.GetChild(2).gameObject.SetActive(true);
                                //Debug.Log("apple: " + inputManager.GetCellObject(siblingIndex - 1));
                            }
                        }
                        else
                        {
                            //Debug.Log("No sibling in front");
                        }
                    }
                    else if (placedObject.level >= 7)
                    {
                        PlacedObject appleBuffedUnit = inputManager.GetCellObject(siblingIndex + 1);
                        if ((appleBuffedUnit != null) && (appleBuffedUnit.gameObject.activeInHierarchy == true))   // buffing the unit behind if active
                        {
                            appleBuffedUnit.baseAttack += 5;
                            appleBuffedUnit.baseHealth += 4;
                            appleBuffedUnit.attack += 5;
                            appleBuffedUnit.health += 4;
                            inputManager.UpdateFloatingText(appleBuffedUnit);

                            appleBuffedUnit.transform.GetChild(2).gameObject.SetActive(true);
                            FindObjectOfType<AudioManager>().Play("Buff2");
                            //Debug.Log("apple: " + inputManager.GetCellObject(siblingIndex + 1));
                        }
                        if (siblingIndex - 1 >= 0)
                        {
                            PlacedObject appleBuffedUnitTwo = inputManager.GetCellObject(siblingIndex - 1);
                            if ((appleBuffedUnitTwo != null) && (appleBuffedUnitTwo.gameObject.activeInHierarchy == true))   // buffing unit in front
                            {
                                appleBuffedUnitTwo.baseAttack += 5;
                                appleBuffedUnitTwo.baseHealth += 4;
                                appleBuffedUnitTwo.attack += 5;
                                appleBuffedUnitTwo.health += 4;
                                inputManager.UpdateFloatingText(appleBuffedUnitTwo);

                                appleBuffedUnitTwo.transform.GetChild(2).gameObject.SetActive(true);
                                //Debug.Log("apple: " + inputManager.GetCellObject(siblingIndex - 1));
                            }
                        }
                        else
                        {
                           // Debug.Log("No sibling in front");
                        }
                    }
                }
                else if(inputManager.roundCounter >= 3)
                {
                    if (placedObject.level < 4)
                    {
                        PlacedObject appleBuffedUnit = inputManager.GetCellObject(siblingIndex + 1);
                        if ((appleBuffedUnit != null) && (appleBuffedUnit.gameObject.activeInHierarchy == true))   // buffing the unit behind if active
                        {
                            appleBuffedUnit.baseAttack += 2;
                            appleBuffedUnit.baseHealth += 1;
                            appleBuffedUnit.attack += 2;
                            appleBuffedUnit.health += 1;
                            inputManager.UpdateFloatingText(appleBuffedUnit);

                            appleBuffedUnit.transform.GetChild(2).gameObject.SetActive(true);
                            FindObjectOfType<AudioManager>().Play("Buff2");
                            //Debug.Log("apple: " + inputManager.GetCellObject(siblingIndex + 1));
                        }
                    }
                    else if (placedObject.level < 7)
                    {
                        PlacedObject appleBuffedUnit = inputManager.GetCellObject(siblingIndex + 1);
                        if ((appleBuffedUnit != null) && (appleBuffedUnit.gameObject.activeInHierarchy == true))   // buffing the unit behind if active
                        {
                            appleBuffedUnit.baseAttack += 3;
                            appleBuffedUnit.baseHealth += 2;
                            appleBuffedUnit.attack += 3;
                            appleBuffedUnit.health += 2;
                            inputManager.UpdateFloatingText(appleBuffedUnit);

                            appleBuffedUnit.transform.GetChild(2).gameObject.SetActive(true);
                            FindObjectOfType<AudioManager>().Play("Buff2");
                            //Debug.Log("apple: " + inputManager.GetCellObject(siblingIndex + 1) + " !!!!");
                        }
                        if (siblingIndex - 1 >= 0)
                        {
                            PlacedObject appleBuffedUnitTwo = inputManager.GetCellObject(siblingIndex - 1);
                            if ((appleBuffedUnitTwo != null) && (appleBuffedUnitTwo.gameObject.activeInHierarchy == true))   // buffing unit in front
                            {
                                appleBuffedUnitTwo.baseAttack += 3;
                                appleBuffedUnitTwo.baseHealth += 2;
                                appleBuffedUnitTwo.attack += 3;
                                appleBuffedUnitTwo.health += 2;
                                inputManager.UpdateFloatingText(appleBuffedUnitTwo);

                                appleBuffedUnitTwo.transform.GetChild(2).gameObject.SetActive(true);
                                //Debug.Log("apple: " + inputManager.GetCellObject(siblingIndex - 1));
                            }
                        }
                        if (siblingIndex - 3 >= 0)
                        {
                            PlacedObject appleBuffedThree = inputManager.GetCellObject(siblingIndex - 3);
                            if ((appleBuffedThree != null) && (appleBuffedThree.gameObject.activeInHierarchy == true))   // buffing unit in front
                            {
                                appleBuffedThree.baseAttack += 3;
                                appleBuffedThree.baseHealth += 2;
                                appleBuffedThree.attack += 3;
                                appleBuffedThree.health += 2;
                                inputManager.UpdateFloatingText(appleBuffedThree);

                                appleBuffedThree.transform.GetChild(2).gameObject.SetActive(true);
                                //Debug.Log("apple: " + inputManager.GetCellObject(siblingIndex - 3));
                            }
                        }
                        if (siblingIndex + 3 <= 5)
                        {
                            PlacedObject appleBuffedUnitFour = inputManager.GetCellObject(siblingIndex + 3);
                            if ((appleBuffedUnitFour != null) && (appleBuffedUnitFour.gameObject.activeInHierarchy == true))   // buffing unit in front
                            {
                                appleBuffedUnitFour.baseAttack += 3;
                                appleBuffedUnitFour.baseHealth += 2;
                                appleBuffedUnitFour.attack += 3;
                                appleBuffedUnitFour.health += 2;
                                inputManager.UpdateFloatingText(appleBuffedUnitFour);

                                appleBuffedUnitFour.transform.GetChild(2).gameObject.SetActive(true);
                                //Debug.Log("apple: " + inputManager.GetCellObject(siblingIndex + 3));
                            }
                        }
                        else
                        {
                            //Debug.Log("No sibling in front");
                        }
                    }
                    else if (placedObject.level >= 7)
                    {
                        PlacedObject appleBuffedUnit = inputManager.GetCellObject(siblingIndex + 1);
                        if ((appleBuffedUnit != null) && (appleBuffedUnit.gameObject.activeInHierarchy == true))   // buffing the unit behind if active
                        {
                            appleBuffedUnit.baseAttack += 3;
                            appleBuffedUnit.baseHealth += 2;
                            appleBuffedUnit.attack += 3;
                            appleBuffedUnit.health += 2;
                            inputManager.UpdateFloatingText(appleBuffedUnit);

                            appleBuffedUnit.transform.GetChild(2).gameObject.SetActive(true);
                            FindObjectOfType<AudioManager>().Play("Buff2");
                            //Debug.Log("apple: " + inputManager.GetCellObject(siblingIndex + 1));
                        }
                        if (siblingIndex - 1 >= 0)
                        {
                            PlacedObject appleBuffedUnitTwo = inputManager.GetCellObject(siblingIndex - 1);
                            if ((appleBuffedUnitTwo != null) && (appleBuffedUnitTwo.gameObject.activeInHierarchy == true))   // buffing unit in front
                            {
                                appleBuffedUnitTwo.baseAttack += 3;
                                appleBuffedUnitTwo.baseHealth += 2;
                                appleBuffedUnitTwo.attack += 3;
                                appleBuffedUnitTwo.health += 2;
                                inputManager.UpdateFloatingText(appleBuffedUnitTwo);

                                appleBuffedUnitTwo.transform.GetChild(2).gameObject.SetActive(true);
                                //Debug.Log("apple: " + inputManager.GetCellObject(siblingIndex - 1));
                            }
                        }
                        if (siblingIndex - 3 >= 0)
                        {
                            PlacedObject appleBuffedThree = inputManager.GetCellObject(siblingIndex - 3);
                            if ((appleBuffedThree != null) && (appleBuffedThree.gameObject.activeInHierarchy == true))   // buffing unit in front
                            {
                                appleBuffedThree.baseAttack += 5;
                                appleBuffedThree.baseHealth += 4;
                                appleBuffedThree.attack += 5;
                                appleBuffedThree.health += 4;
                                inputManager.UpdateFloatingText(appleBuffedThree);

                                appleBuffedThree.transform.GetChild(2).gameObject.SetActive(true);
                                //Debug.Log("apple: " + inputManager.GetCellObject(siblingIndex - 3));
                            }
                        }
                        if (siblingIndex + 3 <= 5)
                        {
                            PlacedObject appleBuffedUnitFour = inputManager.GetCellObject(siblingIndex + 3);
                            if ((appleBuffedUnitFour != null) && (appleBuffedUnitFour.gameObject.activeInHierarchy == true))   // buffing unit in front
                            {
                                appleBuffedUnitFour.baseAttack += 5;
                                appleBuffedUnitFour.baseHealth += 4;
                                appleBuffedUnitFour.attack += 5;
                                appleBuffedUnitFour.health += 4;
                                inputManager.UpdateFloatingText(appleBuffedUnitFour);

                                appleBuffedUnitFour.transform.GetChild(2).gameObject.SetActive(true);
                                //Debug.Log("apple: " + inputManager.GetCellObject(siblingIndex + 3));
                            }
                        }
                    }
                }
            }
        }
        else if (ability == "pineapple")
        {
            if (placedObject.health <= 0)
            {
                if (manager == unitManager)
                {
                    Unit unit = inputManager.unitList[2];
                    GameObject gridCellGameObject = placedObject.AttachedGridCell(false);
                    GridCell gridCell;
                    if (gridCellGameObject.transform.position.x >= 2.6f)
                    {
                        gridCell = inputManager.GetGridCell(5).GetComponent<GridCell>();
                    }
                    else
                    {
                        gridCell = inputManager.GetGridCell(2).GetComponent<GridCell>();
                    }
                    Vector2Int pos2 = gridCell.GetPosition();
                    Vector3 pos3 = new Vector3(gridCell.transform.position.x, 0f, gridCell.transform.position.z);

                    // spawn extra pineapple
                    if (gridCell.isOccupied == false)
                    {
                        PlacedObject placedO = PlacedObject.Create(pos3, pos2, Unit.Dir.Down, unit);
                        gridCell.SetPlacedObject(placedO);
                        gridCell.StoreObject(placedO);
                        placedO.transform.SetParent(manager.transform);
                        placedO.SettingStats();
                        placedO.name = "Mini Grape(Clone)";
                        placedO.ability = "mini grape";
                        placedO.health = 1;
                        if (placedO.level < 4)
                        {
                            placedO.attack = (Mathf.RoundToInt(placedObject.attack * 0.5f));
                        }
                        else if (placedO.level < 7)
                        {
                            placedO.attack = (Mathf.RoundToInt(placedObject.attack * 1f));
                        }
                        else if (placedO.level >= 7)
                        {
                            placedO.attack = (Mathf.RoundToInt(placedObject.attack * 1.5f));
                        }
                        else    // should not happen
                        {
                            placedO.attack = (Mathf.RoundToInt(placedObject.attack * 1f));
                        }

                        inputManager.ShowFloatingText(placedO, pos3, false);

                        Transform floatingStats = placedO.transform.GetChild(0);
                        //Debug.Log(floatingStats);
                        floatingStats.transform.localPosition = new Vector3(0.4f, 0.35f, -0.3f);
                        floatingStats.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
                        floatingStats.GetChild(2).position = new Vector3(placedO.transform.GetChild(0).GetChild(2).position.x, 1.5f, placedO.transform.GetChild(0).GetChild(2).position.z);

                        // rotate spawned pineapple
                        placedO.transform.GetChild(1).GetComponent<LockRotation>().enabled = true;

                        inputManager.levelUpdate.UpdateLevel(placedO);
                        //Debug.Log("Summon Pineapple");
                    }
                }
                else if (manager == enemyManager)
                {
                    Unit unit = inputManager.unitList[9];
                    GameObject gridCellGameObject = placedObject.AttachedGridCell(true);
                    GridCell gridCell;
                    if (gridCellGameObject.transform.position.x >= 2.6f)
                    {
                        gridCell = inputManager.GetEnemyCell(5).GetComponent<GridCell>();
                    }
                    else
                    {
                        gridCell = inputManager.GetEnemyCell(2).GetComponent<GridCell>();
                    }
                    Vector2Int pos2 = gridCell.GetPosition();
                    Vector3 pos3 = new Vector3(gridCell.transform.position.x, 0f, gridCell.transform.position.z);

                    // spawn extra pineapple
                    if (gridCell.isOccupied == false)
                    {
                        PlacedObject placedO = PlacedObject.Create(pos3, pos2, Unit.Dir.Down, unit);
                        gridCell.SetPlacedObject(placedO);
                        gridCell.StoreObject(placedO);
                        placedO.transform.SetParent(manager.transform);
                        placedO.SettingStats();
                        placedO.name = "Mini Grape(Clone)";
                        placedO.ability = "mini grape";
                        placedO.health = 1;
                        if (placedO.level < 4)
                        {
                            placedO.attack = (Mathf.RoundToInt(placedObject.attack * 0.5f));
                        }
                        else if (placedO.level < 7)
                        {
                            placedO.attack = (Mathf.RoundToInt(placedObject.attack * 1f));
                        }
                        else if (placedO.level >= 7)
                        {
                            placedO.attack = (Mathf.RoundToInt(placedObject.attack * 1.5f));
                        }
                        else    // should not happen
                        {
                            placedO.attack = (Mathf.RoundToInt(placedObject.attack * 1f));
                        }

                        inputManager.ShowFloatingText(placedO, pos3, false);

                        placedO.transform.GetChild(0).transform.localPosition = new Vector3 (0.7f, 0.15f, -0.6f);
                        placedO.transform.GetChild(0).transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
                        placedO.transform.GetChild(0).GetChild(2).position = new Vector3 (placedO.transform.GetChild(0).GetChild(2).position.x, 1.5f, placedO.transform.GetChild(0).GetChild(2).position.z);

                        placedO.transform.GetChild(1).eulerAngles = new Vector3 (0f, 0f, 0f);

                        inputManager.levelUpdate.UpdateLevel(placedO);
                        //Debug.Log("Summon Aubergine");
                    }
                }
                else
                {
                    //Debug.Log("No manager found");
                }
            }
        }
        else if (ability == "grapes")
        {

            if (placedObject.health <= 0)
            {
                if (manager == unitManager) // unit grape
                {
                    Unit unit = inputManager.unitList[6];
                    GameObject gridCellGameObject = placedObject.AttachedGridCell(false);
                    //Debug.Log(gridCellGameObject);
                    GridCell gridCell = gridCellGameObject.GetComponent<GridCell>();
                    Vector2Int pos2 = gridCell.GetPosition();
                    Vector3 pos3 = new Vector3(gridCellGameObject.transform.position.x, 0f, gridCellGameObject.transform.position.z);

                    // deactivate grape
                    placedObject.gameObject.SetActive(false);
                    placedObject.transform.GetChild(1).GetChild(0).eulerAngles = new Vector3(0f, 0f, 0f);
                    placedObject.transform.GetChild(1).GetChild(0).localPosition = new Vector3(0f, 0f, 0f);
                    placedObject.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
                    gridCell.UnstoreObject(placedObject);

                    if (placedObject.level < 4)
                    {
                        // spawn mini grape
                        PlacedObject placedO = PlacedObject.Create(pos3, pos2, Unit.Dir.Down, unit);
                        gridCell.SetPlacedObject(placedO);
                        gridCell.StoreObject(placedO);
                        placedO.transform.SetParent(manager.transform);
                        placedO.SettingStats();
                        inputManager.ShowFloatingText(placedO, new Vector3(pos3.x + 0.4f, pos3.y + 0.35f, pos3.z + 0.1f), false);
                        
                        // floating stats
                        placedO.transform.GetChild(0).transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);

                        placedO.transform.GetChild(0).position = new Vector3 (placedO.transform.GetChild(0).position.x - 0.1f, placedO.transform.GetChild(0).position.y + 0.5f, placedO.transform.GetChild(0).position.z);

                        // lock rotation
                        placedO.transform.GetChild(1).GetComponent<LockRotation>().enabled = true;

                        inputManager.levelUpdate.UpdateLevel(placedO);
                        //Debug.Log("spawned mini grape");
                    }
                    else if (placedObject.level < 7)
                    {
                        // spawn mini grape
                        PlacedObject placedO = PlacedObject.Create(pos3, pos2, Unit.Dir.Down, unit);
                        gridCell.SetPlacedObject(placedO);
                        gridCell.StoreObject(placedO);
                        placedO.transform.SetParent(manager.transform);
                        placedO.SettingStats();
                        placedO.attack += 2;    // 3 attack
                        placedO.health += 2;    // 3 health
                        inputManager.ShowFloatingText(placedO, new Vector3(pos3.x + 0.4f, pos3.y + 0.35f, pos3.z + 0.1f), false);
                        
                        placedO.transform.GetChild(0).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        placedO.transform.GetChild(0).GetChild(2).position = new Vector3(placedO.transform.GetChild(0).GetChild(2).position.x, 1.5f, placedO.transform.GetChild(0).GetChild(2).position.z);
                        //placedO.transform.GetChild(0).position = new Vector3(placedO.transform.GetChild(0).position.x - 0.1f, placedO.transform.GetChild(0).position.y + 0.5f, placedO.transform.GetChild(0).position.z);

                        // lock rotation
                        placedO.transform.GetChild(1).GetComponent<LockRotation>().enabled = true;

                        inputManager.levelUpdate.UpdateLevel(placedO);
                        //Debug.Log("spawned mini grape");

                        // spawn mini grape 2
                        if (inputManager.GetGridCell(gridCellGameObject.transform.GetSiblingIndex() + 1).GetComponent<GridCell>().objectInThisGridSpace == null)
                        {
                            GameObject gridCellGameObject2 = inputManager.GetGridCell(gridCellGameObject.transform.GetSiblingIndex() + 1);
                            GridCell gridCell2 = gridCellGameObject2.GetComponent<GridCell>();
                            Vector2Int pos22 = gridCell2.GetPosition();
                            Vector3 pos32 = new Vector3(gridCellGameObject2.transform.position.x, 0f, gridCellGameObject2.transform.position.z);

                            PlacedObject placedO2 = PlacedObject.Create(pos32, pos22, Unit.Dir.Down, unit);
                            gridCell2.SetPlacedObject(placedO2);
                            gridCell2.StoreObject(placedO2);
                            placedO2.transform.SetParent(manager.transform);
                            placedO2.SettingStats();
                            placedO2.attack += 2;
                            placedO2.health += 2;
                            inputManager.ShowFloatingText(placedO2, new Vector3(pos32.x + 0.4f, pos32.y + 0.35f, pos32.z + 0.1f), false);
                            
                            placedO2.transform.GetChild(0).transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
                            placedO2.transform.GetChild(0).GetChild(2).position = new Vector3(placedO2.transform.GetChild(0).GetChild(2).position.x, 1.5f, placedO2.transform.GetChild(0).GetChild(2).position.z);
                            //placedO.transform.GetChild(0).position = new Vector3(placedO.transform.GetChild(0).position.x - 0.1f, placedO.transform.GetChild(0).position.y + 0.5f, placedO.transform.GetChild(0).position.z);

                            // lock rotation
                            placedO2.transform.GetChild(1).GetComponent<LockRotation>().enabled = true;

                            inputManager.levelUpdate.UpdateLevel(placedO2);
                            //Debug.Log("spawned mini grape 2");
                        }
                        else if (inputManager.GetGridCell(gridCellGameObject.transform.GetSiblingIndex() + 2) != null)
                        {
                            if (inputManager.GetGridCell(gridCellGameObject.transform.GetSiblingIndex() + 2).GetComponent<GridCell>().objectInThisGridSpace == null)
                            {
                                GameObject gridCellGameObject2 = inputManager.GetGridCell(gridCellGameObject.transform.GetSiblingIndex() + 2);
                                GridCell gridCell2 = gridCellGameObject2.GetComponent<GridCell>();
                                Vector2Int pos22 = gridCell2.GetPosition();
                                Vector3 pos32 = new Vector3(gridCellGameObject2.transform.position.x, 0f, gridCellGameObject2.transform.position.z);

                                PlacedObject placedO2 = PlacedObject.Create(pos32, pos22, Unit.Dir.Down, unit);
                                gridCell2.SetPlacedObject(placedO2);
                                gridCell2.StoreObject(placedO2);
                                placedO2.transform.SetParent(manager.transform);
                                placedO2.SettingStats();
                                placedO2.attack += 2;
                                placedO2.health += 2;
                                inputManager.ShowFloatingText(placedO2, new Vector3(pos32.x + 0.4f, pos32.y + 0.35f, pos32.z + 0.1f), false);

                                placedO2.transform.GetChild(0).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                placedO2.transform.GetChild(0).GetChild(2).position = new Vector3(placedO2.transform.GetChild(0).GetChild(2).position.x, 1.5f, placedO2.transform.GetChild(0).GetChild(2).position.z);
                                //placedO.transform.GetChild(0).position = new Vector3(placedO.transform.GetChild(0).position.x - 0.1f, placedO.transform.GetChild(0).position.y + 0.5f, placedO.transform.GetChild(0).position.z);

                                // lock rotation
                                placedO2.transform.GetChild(1).GetComponent<LockRotation>().enabled = true;

                                inputManager.levelUpdate.UpdateLevel(placedO2);
                                //Debug.Log("spawned mini grape 2");
                            }
                        }
                        else
                        {
                            //Debug.Log("No space for mini grape 2");
                        }

                    }
                    else if (placedObject.level >= 7)
                    {
                        // spawn mini grape
                        PlacedObject placedO = PlacedObject.Create(pos3, pos2, Unit.Dir.Down, unit);
                        gridCell.SetPlacedObject(placedO);
                        gridCell.StoreObject(placedO);
                        placedO.transform.SetParent(manager.transform);
                        placedO.SettingStats();
                        placedO.attack += 5;    // 6 attack
                        placedO.health += 5;    // 6 health
                        inputManager.ShowFloatingText(placedO, new Vector3(pos3.x + 0.4f, pos3.y + 0.35f, pos3.z + 0.1f), false);
                        
                        placedO.transform.GetChild(0).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        placedO.transform.GetChild(0).GetChild(2).position = new Vector3(placedO.transform.GetChild(0).GetChild(2).position.x, 1.5f, placedO.transform.GetChild(0).GetChild(2).position.z);
                        //placedO.transform.GetChild(0).position = new Vector3(placedO.transform.GetChild(0).position.x - 0.1f, placedO.transform.GetChild(0).position.y + 0.5f, placedO.transform.GetChild(0).position.z);

                        // lock rotation
                        placedO.transform.GetChild(1).GetComponent<LockRotation>().enabled = true;

                        inputManager.levelUpdate.UpdateLevel(placedO);
                        //Debug.Log("spawned mini grape");

                        // spawn mini grape 2
                        if (inputManager.GetGridCell(gridCellGameObject.transform.GetSiblingIndex() + 1).GetComponent<GridCell>().objectInThisGridSpace == null)
                        {
                            GameObject gridCellGameObject2 = inputManager.GetGridCell(gridCellGameObject.transform.GetSiblingIndex() + 1);
                            GridCell gridCell2 = gridCellGameObject2.GetComponent<GridCell>();
                            Vector2Int pos22 = gridCell2.GetPosition();
                            Vector3 pos32 = new Vector3(gridCellGameObject2.transform.position.x, 0f, gridCellGameObject2.transform.position.z);

                            PlacedObject placedO2 = PlacedObject.Create(pos32, pos22, Unit.Dir.Down, unit);
                            gridCell2.SetPlacedObject(placedO2);
                            gridCell2.StoreObject(placedO2);
                            placedO2.transform.SetParent(manager.transform);
                            placedO2.SettingStats();
                            placedO2.attack += 5;   // 6 attack
                            placedO2.health += 5;   // 6 health
                            inputManager.ShowFloatingText(placedO2, new Vector3(pos32.x + 0.4f, pos32.y + 0.35f, pos32.z + 0.1f), false);
                            
                            placedO2.transform.GetChild(0).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                            placedO2.transform.GetChild(0).GetChild(2).position = new Vector3(placedO2.transform.GetChild(0).GetChild(2).position.x, 1.5f, placedO2.transform.GetChild(0).GetChild(2).position.z);
                            //placedO.transform.GetChild(0).position = new Vector3(placedO.transform.GetChild(0).position.x - 0.1f, placedO.transform.GetChild(0).position.y + 0.5f, placedO.transform.GetChild(0).position.z);

                            // lock rotation
                            placedO2.transform.GetChild(1).GetComponent<LockRotation>().enabled = true;

                            inputManager.levelUpdate.UpdateLevel(placedO2);
                            //Debug.Log("spawned mini grape 2");
                        }
                        else if (inputManager.GetGridCell(gridCellGameObject.transform.GetSiblingIndex() + 2) != null)
                        {
                            if (inputManager.GetGridCell(gridCellGameObject.transform.GetSiblingIndex() + 2).GetComponent<GridCell>().objectInThisGridSpace == null)
                            {
                                GameObject gridCellGameObject2 = inputManager.GetGridCell(gridCellGameObject.transform.GetSiblingIndex() + 2);
                                GridCell gridCell2 = gridCellGameObject2.GetComponent<GridCell>();
                                Vector2Int pos22 = gridCell2.GetPosition();
                                Vector3 pos32 = new Vector3(gridCellGameObject2.transform.position.x, 0f, gridCellGameObject2.transform.position.z);

                                PlacedObject placedO2 = PlacedObject.Create(pos32, pos22, Unit.Dir.Down, unit);
                                gridCell2.SetPlacedObject(placedO2);
                                gridCell2.StoreObject(placedO2);
                                placedO2.transform.SetParent(manager.transform);
                                placedO2.SettingStats();
                                placedO2.attack += 5;   // 6 attack
                                placedO2.health += 5;   // 6 health
                                inputManager.ShowFloatingText(placedO2, new Vector3(pos32.x + 0.4f, pos32.y + 0.35f, pos32.z + 0.1f), false);

                                placedO2.transform.GetChild(0).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                placedO2.transform.GetChild(0).GetChild(2).position = new Vector3(placedO2.transform.GetChild(0).GetChild(2).position.x, 1.5f, placedO2.transform.GetChild(0).GetChild(2).position.z);
                                //placedO.transform.GetChild(0).position = new Vector3(placedO.transform.GetChild(0).position.x - 0.1f, placedO.transform.GetChild(0).position.y + 0.5f, placedO.transform.GetChild(0).position.z);

                                // lock rotation
                                placedO2.transform.GetChild(1).GetComponent<LockRotation>().enabled = true;

                                inputManager.levelUpdate.UpdateLevel(placedO2);
                                //Debug.Log("spawned mini grape 2");
                            }
                        }
                        else
                        {
                            //Debug.Log("No space for mini grape 2");
                        }
                    }
                }
                else if (manager == enemyManager)   // enemy corn
                {
                    Unit unit = inputManager.unitList[13];
                    GameObject gridCellGameObject = placedObject.AttachedGridCell(true);
                    GridCell gridCell = gridCellGameObject.GetComponent<GridCell>();
                    Vector2Int pos2 = gridCell.GetPosition();
                    Vector3 pos3 = new Vector3(gridCellGameObject.transform.position.x, 0f, gridCellGameObject.transform.position.z);

                    // deactivate grape
                    placedObject.gameObject.SetActive(false);
                    gridCell.UnstoreObject(placedObject);

                    if (placedObject.level < 4)
                    {
                        // spawn mini grape
                        PlacedObject placedO = PlacedObject.Create(pos3, pos2, Unit.Dir.Down, unit);
                        gridCell.SetPlacedObject(placedO);
                        gridCell.StoreObject(placedO);
                        placedO.transform.SetParent(manager.transform);
                        placedO.SettingStats();
                        inputManager.ShowFloatingText(placedO, new Vector3(pos3.x + 0.3f, pos3.y + 0.35f, pos3.z + 0.1f), false);
                        
                        placedO.transform.GetChild(0).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        placedO.transform.GetChild(0).GetChild(2).position = new Vector3(placedO.transform.GetChild(0).GetChild(2).position.x, 1.5f, placedO.transform.GetChild(0).GetChild(2).position.z);
                        
                        inputManager.levelUpdate.UpdateLevel(placedO);
                        //Debug.Log("spawned mini grape");
                    }
                    else if (placedObject.level < 7)
                    {
                        // spawn mini grape
                        PlacedObject placedO = PlacedObject.Create(pos3, pos2, Unit.Dir.Down, unit);
                        gridCell.SetPlacedObject(placedO);
                        gridCell.StoreObject(placedO);
                        placedO.transform.SetParent(manager.transform);
                        placedO.SettingStats();
                        placedO.attack += 2;
                        placedO.health += 2;
                        inputManager.ShowFloatingText(placedO, new Vector3(pos3.x + 0.3f, pos3.y + 0.35f, pos3.z + 0.1f), false);
                        
                        placedO.transform.GetChild(0).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        placedO.transform.GetChild(0).GetChild(2).position = new Vector3(placedO.transform.GetChild(0).GetChild(2).position.x, 1.5f, placedO.transform.GetChild(0).GetChild(2).position.z);
                        
                        inputManager.levelUpdate.UpdateLevel(placedO);
                        //Debug.Log("spawned mini grape");

                        // spawn mini grape 2
                        if (inputManager.GetEnemyCell(gridCellGameObject.transform.GetSiblingIndex() + 1).GetComponent<GridCell>().objectInThisGridSpace == null)
                        {
                            GameObject gridCellGameObject2 = inputManager.GetEnemyCell(gridCellGameObject.transform.GetSiblingIndex() + 1);
                            GridCell gridCell2 = gridCellGameObject2.GetComponent<GridCell>();
                            Vector2Int pos22 = gridCell2.GetPosition();
                            Vector3 pos32 = new Vector3(gridCellGameObject2.transform.position.x, 0f, gridCellGameObject2.transform.position.z);

                            PlacedObject placedO2 = PlacedObject.Create(pos32, pos22, Unit.Dir.Down, unit);
                            gridCell2.SetPlacedObject(placedO2);
                            gridCell2.StoreObject(placedO2);
                            placedO2.transform.SetParent(manager.transform);
                            placedO2.SettingStats();
                            placedO2.attack += 2;
                            placedO2.health += 2;
                            inputManager.ShowFloatingText(placedO2, new Vector3(pos32.x + 0.3f, pos32.y + 0.35f, pos32.z + 0.1f), false);
                            
                            placedO2.transform.GetChild(0).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                            placedO2.transform.GetChild(0).GetChild(2).position = new Vector3(placedO2.transform.GetChild(0).GetChild(2).position.x, 1.5f, placedO2.transform.GetChild(0).GetChild(2).position.z);
                            
                            inputManager.levelUpdate.UpdateLevel(placedO2);
                            //Debug.Log("spawned mini grape 2");
                        }
                        else if (inputManager.GetEnemyCell(gridCellGameObject.transform.GetSiblingIndex() + 2) != null)
                        {
                            if (inputManager.GetEnemyCell(gridCellGameObject.transform.GetSiblingIndex() + 2).GetComponent<GridCell>().objectInThisGridSpace == null)
                            {
                                GameObject gridCellGameObject2 = inputManager.GetEnemyCell(gridCellGameObject.transform.GetSiblingIndex() + 2);
                                GridCell gridCell2 = gridCellGameObject2.GetComponent<GridCell>();
                                Vector2Int pos22 = gridCell2.GetPosition();
                                Vector3 pos32 = new Vector3(gridCellGameObject2.transform.position.x, 0f, gridCellGameObject2.transform.position.z);

                                PlacedObject placedO2 = PlacedObject.Create(pos32, pos22, Unit.Dir.Down, unit);
                                gridCell2.SetPlacedObject(placedO2);
                                gridCell2.StoreObject(placedO2);
                                placedO2.transform.SetParent(manager.transform);
                                placedO2.SettingStats();
                                placedO2.attack += 2;
                                placedO2.health += 2;
                                inputManager.ShowFloatingText(placedO2, new Vector3(pos32.x + 0.3f, pos32.y + 0.35f, pos32.z + 0.1f), false);

                                placedO2.transform.GetChild(0).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                placedO2.transform.GetChild(0).GetChild(2).position = new Vector3(placedO2.transform.GetChild(0).GetChild(2).position.x, 1.5f, placedO2.transform.GetChild(0).GetChild(2).position.z);

                                inputManager.levelUpdate.UpdateLevel(placedO2);
                                //Debug.Log("spawned mini grape 2");
                            }
                        }
                        else
                        {
                            //Debug.Log("No space for mini grape 2");
                        }
                    }
                    else if (placedObject.level >= 7)
                    {
                        // spawn mini grape
                        PlacedObject placedO = PlacedObject.Create(pos3, pos2, Unit.Dir.Down, unit);
                        gridCell.SetPlacedObject(placedO);
                        gridCell.StoreObject(placedO);
                        placedO.transform.SetParent(manager.transform);
                        placedO.SettingStats();
                        placedO.attack += 5;    // 6 attack
                        placedO.health += 5;    // 6 health
                        inputManager.ShowFloatingText(placedO, new Vector3(pos3.x + 0.3f, pos3.y + 0.35f, pos3.z + 0.1f), false);
                        
                        placedO.transform.GetChild(0).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        placedO.transform.GetChild(0).GetChild(2).position = new Vector3(placedO.transform.GetChild(0).GetChild(2).position.x, 1.5f, placedO.transform.GetChild(0).GetChild(2).position.z);
                        
                        inputManager.levelUpdate.UpdateLevel(placedO);
                        //Debug.Log("spawned mini grape");

                        // spawn mini grape 2
                        if (inputManager.GetEnemyCell(gridCellGameObject.transform.GetSiblingIndex() + 1).GetComponent<GridCell>().objectInThisGridSpace == null)
                        {
                            GameObject gridCellGameObject2 = inputManager.GetEnemyCell(gridCellGameObject.transform.GetSiblingIndex() + 1);
                            GridCell gridCell2 = gridCellGameObject2.GetComponent<GridCell>();
                            Vector2Int pos22 = gridCell2.GetPosition();
                            Vector3 pos32 = new Vector3(gridCellGameObject2.transform.position.x, 0f, gridCellGameObject2.transform.position.z);

                            PlacedObject placedO2 = PlacedObject.Create(pos32, pos22, Unit.Dir.Down, unit);
                            gridCell2.SetPlacedObject(placedO2);
                            gridCell2.StoreObject(placedO2);
                            placedO2.transform.SetParent(manager.transform);
                            placedO2.SettingStats();
                            placedO2.attack += 5;   // 6 attack
                            placedO2.health += 5;   // 6 health
                            inputManager.ShowFloatingText(placedO2, new Vector3(pos32.x + 0.3f, pos32.y + 0.35f, pos32.z + 0.1f), false);
                            
                            placedO2.transform.GetChild(0).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                            placedO2.transform.GetChild(0).GetChild(2).position = new Vector3(placedO2.transform.GetChild(0).GetChild(2).position.x, 1.5f, placedO2.transform.GetChild(0).GetChild(2).position.z);
    
                            inputManager.levelUpdate.UpdateLevel(placedO2);
                            //Debug.Log("spawned mini grape 2");
                        }
                        else if (inputManager.GetEnemyCell(gridCellGameObject.transform.GetSiblingIndex() + 2) != null)
                        {
                            if (inputManager.GetEnemyCell(gridCellGameObject.transform.GetSiblingIndex() + 2).GetComponent<GridCell>().objectInThisGridSpace == null)
                            {
                                GameObject gridCellGameObject2 = inputManager.GetEnemyCell(gridCellGameObject.transform.GetSiblingIndex() + 2);
                                GridCell gridCell2 = gridCellGameObject2.GetComponent<GridCell>();
                                Vector2Int pos22 = gridCell2.GetPosition();
                                Vector3 pos32 = new Vector3(gridCellGameObject2.transform.position.x, 0f, gridCellGameObject2.transform.position.z);

                                PlacedObject placedO2 = PlacedObject.Create(pos32, pos22, Unit.Dir.Down, unit);
                                gridCell2.SetPlacedObject(placedO2);
                                gridCell2.StoreObject(placedO2);
                                placedO2.transform.SetParent(manager.transform);
                                placedO2.SettingStats();
                                placedO2.attack += 5;   // 6 attack
                                placedO2.health += 5;   // 6 health
                                inputManager.ShowFloatingText(placedO2, new Vector3(pos32.x + 0.3f, pos32.y + 0.35f, pos32.z + 0.1f), false);

                                placedO2.transform.GetChild(0).transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                placedO2.transform.GetChild(0).GetChild(2).position = new Vector3(placedO2.transform.GetChild(0).GetChild(2).position.x, 1.5f, placedO2.transform.GetChild(0).GetChild(2).position.z);

                                inputManager.levelUpdate.UpdateLevel(placedO2);
                                //Debug.Log("spawned mini grape 2");
                            }
                        }
                        else
                        {
                            //Debug.Log("No space for mini grape 2");
                        }
                    }
                }
                else
                {
                    //Debug.Log("No Manager found");
                }
            }
        }
        else if (ability == "mini grapes")
        {
            //if (placedObject.health <= 0)
            //{
            //    Destroy(placedObject.gameObject);
            //    Debug.Log("Mini grape");
            //}
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

                    placedObject.transform.GetChild(4).gameObject.SetActive(true);
                    FindObjectOfType<AudioManager>().Play("Break");
                    //Debug.Log("coconut armor");
                }
                if (placedObject.level >= 7)
                {
                    if (placedObject.armorTriggerCounter < 1)
                    {
                        placedObject.health = placedObject.baseHealth;
                        inputManager.UpdateFloatingText(placedObject);
                        placedObject.armorTriggerCounter += 1;

                        placedObject.transform.GetChild(4).gameObject.SetActive(true);
                        FindObjectOfType<AudioManager>().Play("Break");
                        //Debug.Log("coconut armor");
                    }
                    else if (placedObject.armorTriggerCounter >= 1)
                    {
                        placedObject.health = placedObject.baseHealth;
                        placedObject.armor = false;
                        placedObject.armorTriggerCounter += 1;
                        inputManager.UpdateFloatingText(placedObject);

                        placedObject.transform.GetChild(4).gameObject.SetActive(true);
                        FindObjectOfType<AudioManager>().Play("Break");
                        //Debug.Log("coconut armor");
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

                //Debug.Log(placedObject.transform.GetSiblingIndex());

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

                        buffedUnit.transform.GetChild(3).gameObject.SetActive(true);
                        FindObjectOfType<AudioManager>().Play("Buff1");
                        //Debug.Log("lemon ability: " + buffedUnit);
                    }
                    if ((placedObject.level >= 4) && (placedObject.level < 7))
                    {
                        buffedUnit.GetComponent<PlacedObject>().baseAttack += 2;
                        buffedUnit.GetComponent<PlacedObject>().baseHealth += 2;
                        buffedUnit.GetComponent<PlacedObject>().attack += 2;
                        buffedUnit.GetComponent<PlacedObject>().health += 2;
                        inputManager.UpdateFloatingText(buffedUnit.GetComponent<PlacedObject>());

                        buffedUnit.transform.GetChild(3).gameObject.SetActive(true);
                        FindObjectOfType<AudioManager>().Play("Buff1");
                        //Debug.Log("lemon ability: " + buffedUnit);
                    }
                    if (placedObject.level >= 7)
                    {
                        buffedUnit.GetComponent<PlacedObject>().baseAttack += 4;
                        buffedUnit.GetComponent<PlacedObject>().baseHealth += 4;
                        buffedUnit.GetComponent<PlacedObject>().attack += 4;
                        buffedUnit.GetComponent<PlacedObject>().health += 4;
                        inputManager.UpdateFloatingText(buffedUnit.GetComponent<PlacedObject>());

                        buffedUnit.transform.GetChild(3).gameObject.SetActive(true);
                        FindObjectOfType<AudioManager>().Play("Buff1");
                        //Debug.Log("lemon ability: " + buffedUnit);

                        buffedUnit2.GetComponent<PlacedObject>().baseAttack += 4;
                        buffedUnit2.GetComponent<PlacedObject>().baseHealth += 4;
                        buffedUnit2.GetComponent<PlacedObject>().attack += 4;
                        buffedUnit2.GetComponent<PlacedObject>().health += 4;
                        inputManager.UpdateFloatingText(buffedUnit2.GetComponent<PlacedObject>());

                        buffedUnit2.transform.GetChild(3).gameObject.SetActive(true);
                        //Debug.Log("lemon ability: " + buffedUnit2);
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
            //int numberOfUnitsOnField = NumberOfUnitsOnField(enemy);

            //if (numberOfUnitsOnField > 1)
            //{
                PlacedObject affectedUnit = null;

                if (manager == unitManager)
                {
                    //Debug.Log(placedObject.transform.position.x);
                    if (placedObject.transform.position.x >= 2)
                    {
                        if (inputManager.GetCellObject(4) != null)
                        {
                            affectedUnit = inputManager.GetCellObject(4).GetComponent<PlacedObject>();
                            //Debug.Log("LANE 2");
                        }
                    }
                    else
                    {
                        if (inputManager.GetCellObject(1) != null)
                        {
                            //Debug.Log("LANE 1");
                            affectedUnit = inputManager.GetCellObject(1).GetComponent<PlacedObject>();
                        }
                    }
                }
                else if (manager == enemyManager)
                {
                    //Debug.Log(placedObject.transform.position.x);
                    if (placedObject.transform.position.x >= 2)
                    {
                        if (inputManager.GetEnemyObject(4) != null)
                        //Debug.Log("LANE 2");
                        affectedUnit = inputManager.GetEnemyObject(4).GetComponent<PlacedObject>();
                    }
                    else
                    {
                        if (inputManager.GetEnemyObject(1) != null)
                        //Debug.Log("LANE 1");
                        affectedUnit = inputManager.GetEnemyObject(1).GetComponent<PlacedObject>();
                    }  
                }
                if (affectedUnit != null)
                {
                if (placedObject.level < 4)
                {
                    affectedUnit.health -= 2;

                    affectedUnit.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
                    affectedUnit.transform.GetChild(0).GetChild(3).GetComponentInChildren<TMP_Text>().text = "2";

                    inputManager.UpdateFloatingText(affectedUnit);
                    CheckAbilityDefense(affectedUnit, manager, enemy);
                    //Debug.Log("LEVEL 1 " + affectedUnit);
                    //Debug.Log(affectedUnit.health);
                }
                if ((placedObject.level >= 4) && (placedObject.level < 7))
                {
                    affectedUnit.health -= 6;

                    affectedUnit.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
                    affectedUnit.transform.GetChild(0).GetChild(3).GetComponentInChildren<TMP_Text>().text = "6";

                    inputManager.UpdateFloatingText(affectedUnit);
                    CheckAbilityDefense(affectedUnit, manager, enemy);
                    //Debug.Log("LEVEL 4");
                }
                if (placedObject.level >= 7)
                {
                    affectedUnit.health -= 12;

                    affectedUnit.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
                    affectedUnit.transform.GetChild(0).GetChild(3).GetComponentInChildren<TMP_Text>().text = "12";

                    inputManager.UpdateFloatingText(affectedUnit);
                    CheckAbilityDefense(affectedUnit, manager, enemy);
                    //Debug.Log("LEVEL 7");
                }
            }
                

                //Debug.Log("Cherry eingesetzt");
            //}
            //else
            //{
            //    //Debug.Log("No Target");
            //}
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

    public void CheckAllUnitsOnDefeat(int lane)
    {

        //// ENEMY
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

        if ((lane == 2) || (lane == 4))
        {
            if ((inputManager.GetEnemyObject(3) != null) && (inputManager.GetEnemyObject(3).health <= 0))
            {
                inputManager.GetEnemyObject(3).DestroySelf();
                inputManager.GetEnemyCell(3).GetComponent<GridCell>().UnstoreObject(inputManager.GetEnemyObject(3));
            }

            if ((inputManager.GetEnemyObject(4) != null) && (inputManager.GetEnemyObject(4).health <= 0))
            {
                inputManager.GetEnemyObject(4).DestroySelf();
                inputManager.GetEnemyCell(4).GetComponent<GridCell>().UnstoreObject(inputManager.GetEnemyObject(4));
            }

            if ((inputManager.GetEnemyObject(5) != null) && (inputManager.GetEnemyObject(5).health <= 0))
            {
                inputManager.GetEnemyObject(5).DestroySelf();
                inputManager.GetEnemyCell(5).GetComponent<GridCell>().UnstoreObject(inputManager.GetEnemyObject(5));
            }
        }


        //// UNIT
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

        if ((lane == 2) ||(lane == 4))
        {
            if ((inputManager.GetCellObject(3) != null) && (inputManager.GetCellObject(3).health <= 0))
            {
                inputManager.GetCellObject(3).gameObject.SetActive(false);
                inputManager.GetGridCell(3).GetComponent<GridCell>().UnstoreObject(inputManager.GetCellObject(3));
            }

            if ((inputManager.GetCellObject(4) != null) && (inputManager.GetCellObject(4).health <= 0))
            {
                inputManager.GetCellObject(4).gameObject.SetActive(false);
                inputManager.GetGridCell(4).GetComponent<GridCell>().UnstoreObject(inputManager.GetCellObject(4));
            }

            if ((inputManager.GetCellObject(5) != null) && (inputManager.GetCellObject(5).health <= 0))
            {
                inputManager.GetCellObject(5).gameObject.SetActive(false);
                inputManager.GetGridCell(5).GetComponent<GridCell>().UnstoreObject(inputManager.GetCellObject(5));
            }
        }
    }
}
