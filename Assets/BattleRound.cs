using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRound : MonoBehaviour
{
    public InputManager inputManager;
    public Animator appleAnim;

    void Start()
    {
        HealthManager(3f);
    }

    public void HealthManager(float delayTime)
    {
        StartCoroutine(DelayAction(delayTime));
    }

    IEnumerator DelayAction(float delayTime)
    {
        while (true)
        {
            inputManager.GetEnemyObject(0).health -= inputManager.GetCellObject(0).attack;
            inputManager.UpdateFloatingText(inputManager.GetEnemyObject(0));

            inputManager.GetCellObject(0).health -= inputManager.GetEnemyObject(0).attack;
            inputManager.UpdateFloatingText(inputManager.GetCellObject(0));
            appleAnim.SetBool("Hit", true);

            if (inputManager.GetEnemyObject(0).health <= 0)
            {
                Debug.Log("GEWONNEN");
                //enemyBattle.DestroySelf();
                break;
            }

            if (inputManager.GetCellObject(0).health <= 0)
            {
                Debug.Log("VERLOREN");
                //unitBattle.DestroySelf();
                break;
            }
            yield return new WaitForSeconds(delayTime);
        }
    }

    public void ManageBattle()
    {
        if (inputManager.roundover == false)
        {
            PlacedObject enemyBattle = inputManager.GetEnemyObject(0);
            PlacedObject unitBattle = inputManager.GetCellObject(0);

            if (enemyBattle && unitBattle != null)
            {
                inputManager.healthE = enemyBattle.health;
                inputManager.healthU = unitBattle.health;
            }

            if ((inputManager.healthU > 0) && (inputManager.healthE > 0))
            {
                if (enemyBattle && unitBattle != null)
                {
                    HealthManager(1f);
                }
            }

            if (enemyBattle.health <= 0)
            {
                Debug.Log("GEWONNEN");
                //enemyBattle.DestroySelf();
                inputManager.roundover = true;
            }

            if (unitBattle.health <= 0)
            {
                Debug.Log("VERLOREN");
                //unitBattle.DestroySelf();
                inputManager.roundover = true;
            }
        }
    }
}
