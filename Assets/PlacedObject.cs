using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlacedObject : MonoBehaviour
{
    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, Unit.Dir dir, Unit unit)
    {
        //dir = unit.GetUnitDir();
        //Debug.Log(unit.GetUnitDir());

        Transform placedObjectTransform = Instantiate(unit.prefab, worldPosition, Quaternion.Euler(0, unit.GetRotationAngle(dir), 0));

        PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();

        placedObject.unit = unit;
        placedObject.origin = origin;
        placedObject.dir = dir;

        return placedObject;
    }


    private Unit unit;
    private Vector2Int origin;
    private Unit.Dir dir;
    InputManager inputManager;

    // stats that can be changed during the battle
    public int attack;
    public int health;
    public int level;

    // stats that dont get changed
    public string nameA;
    public string ability;

    // base values that can increase through buffs and level
    public int baseAttack;
    public int baseHealth;
    public int baseLevel;

    // special abilitiy
    public bool armor;
    public bool firstLvlUp = false;
    public bool secondLvlUp = false;
    public int armorTriggerCounter = 0;
    public int rank = 1;

    // ability
    public string abilityName;
    public string abilityDescription;

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void GettingDamaged(int damage, PlacedObject placedObject)
    {
        health += -damage;
        transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        transform.GetChild(0).GetChild(3).GetComponentInChildren<TMP_Text>().text = placedObject.attack.ToString();
    }

    public void Print()
    {
        Debug.Log(health);
    }

    public void SettingStats()    // initialize base stats --> permanent
    {
        attack = unit.attack;
        health = unit.health;
        level = unit.level;
        nameA = unit.name;
        ability = unit.ability;

        baseAttack = unit.attack;
        baseHealth = unit.health;
        baseLevel = unit.level;

        // ability
        abilityName = unit.abilityName;
        if (rank == 2)
        {
            abilityDescription= unit.abilityDescription2;
        }
        else if(rank == 3)
        {
            abilityDescription = unit.abilityDescription3;
        }
        else
        {
            abilityDescription = unit.abilityDescription1;
        }


        if (ability == "coconut")
        {
            armor = true;
        }
        else
        {
            armor = false;
        }
    }
    public void SettingStatsEnemy(int attackv, int healthv, int levelv)
    {
        attack = attackv;
        health = healthv;
        level = levelv;
        nameA = unit.name;
        ability = unit.ability;

        baseAttack = attackv;
        baseHealth = healthv;
        baseLevel = levelv;

        // ability
        abilityName = unit.abilityName;
        if (rank == 2)
        {
            abilityDescription = unit.abilityDescription2;
        }
        else if (rank == 3)
        {
            abilityDescription = unit.abilityDescription3;
        }
        else
        {
            abilityDescription = unit.abilityDescription1;
        }


        if (ability == "coconut")
        {
            armor = true;
        }
        else
        {
            armor = false;
        }
    }

    public void SetStats()  // initialize stats per round -->
    {
        attack = baseAttack;
        health = baseHealth;
        //level = baseLevel;
        armor = true;
        armorTriggerCounter = 0;

        // ability
        abilityName = unit.abilityName;
        if (rank == 2)
        {
            abilityDescription = unit.abilityDescription2;
        }
        else if (rank == 3)
        {
            abilityDescription = unit.abilityDescription3;
        }
        else
        {
            abilityDescription = unit.abilityDescription1;
        }

        if (ability == "coconut")
        {
            armor = true;
        }
        else
        {
            armor = false;
        }
    }

    public GameObject AttachedGridCell(bool enemy)  // true -> enemy, false -> unit
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();

        if (enemy == false)     // BUG DARF nicht in die erste zeile kommen -> wenn grape auf zweiter linie stirbt!!!!!!!!!!!!!!!!!!
        {
            inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();

            if (transform.position.z < -1)  // third line
            {
                if (transform.position.x > 0)
                {
                    // second lane
                    return inputManager.GetGridCell(5);
                }
                else
                {
                    // first lane
                    return inputManager.GetGridCell(2);
                }
            }
            else if ((transform.position.z > -1) && (transform.position.z < 1)) // second line
            {
                if (transform.position.x > 0)
                {
                    // second lane
                    return inputManager.GetGridCell(4);
                }
                else
                {
                    // first lane
                    return inputManager.GetGridCell(1);
                }
            }
            else if ((transform.position.z > 1) && (transform.position.z < 2))  // thrid line
            {
                if (transform.position.x > 0)
                {
                    // second lane
                    return inputManager.GetGridCell(3);
                }
                else
                {
                    // first lane
                    return inputManager.GetGridCell(0);
                }
            }
            else
            {
                return null;
            }
        }
        else if (enemy == true)
        {
            inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();

            if (transform.position.z > 6)  // third line
            {
                if (transform.position.x > 0)
                {
                    // second lane
                    return inputManager.GetEnemyCell(5);
                }
                else
                {
                    // first lane
                    return inputManager.GetEnemyCell(2);
                }
            }
            else if ((transform.position.z > 4) && (transform.position.z < 6)) // second line
            {
                if (transform.position.x > 0)
                {
                    // second lane
                    return inputManager.GetEnemyCell(4);
                }
                else
                {
                    // first lane
                    return inputManager.GetEnemyCell(1);
                }
            }
            else if ((transform.position.z > 3) && (transform.position.z < 4))  // thrid line
            {
                if (transform.position.x > 0)
                {
                    // second lane
                    return inputManager.GetEnemyCell(3);
                }
                else
                {
                    // first lane
                    return inputManager.GetEnemyCell(0);
                }
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    public void LevelupStats()
    {
        FindObjectOfType<AudioManager>().Play("RankUp");
        health += 1;
        baseHealth += 1;
        attack += 1;
        baseAttack += 1;
        //Debug.Log(attack + " " + health);

        if (level > 7)
        {
            level = 7;
        }

        if ((level == 4) && (firstLvlUp == false))
        {
            firstLvlUp = true;
            rank = 2;
            // Do things when reaching rank 2

            transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
        }
        else if ((level == 7) && (secondLvlUp == false))
        {
            Debug.Log(level);
            secondLvlUp = true;
            rank = 3;
            // Do things when reaching rank 3

            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
    }

    public GridCell AssignedGridCell()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();

        if (transform.position.z < -1)  // third line
        {
            if (transform.position.x > 0)
            {
                // second lane
                return inputManager.GetGridCell(5).GetComponent<GridCell>();
            }
            else
            {
                // first lane
                return inputManager.GetGridCell(2).GetComponent<GridCell>();
            }
        }
        else if ((transform.position.z > -1) && (transform.position.z < 1)) // second line
        {
            if (transform.position.x > 0)
            {
                // second lane
                return inputManager.GetGridCell(4).GetComponent<GridCell>();
            }
            else
            {
                // first lane
                return inputManager.GetGridCell(1).GetComponent<GridCell>();
            }
        }
        else if ((transform.position.z > 1) && (transform.position.z < 2))  // thrid line
        {
            if (transform.position.x > 0)
            {
                // second lane
                return inputManager.GetGridCell(3).GetComponent<GridCell>();
            }
            else
            {
                // first lane
                return inputManager.GetGridCell(0).GetComponent<GridCell>();
            }
        }
        else
        {
            return null;
        }
    }
}
