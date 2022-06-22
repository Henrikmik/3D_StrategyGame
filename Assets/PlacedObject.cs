using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, Unit.Dir dir, Unit unit)
    {
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

    public void GettingDamaged(int damage)
    {
        health += -damage;
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
            if ((inputManager.GetCellObject(0).GetComponent<PlacedObject>() != null) && (inputManager.GetCellObject(0).isActiveAndEnabled == true))
            {
                if (gameObject.GetComponent<PlacedObject>() == inputManager.GetCellObject(0).GetComponent<PlacedObject>())
                {
                    //Debug.Log("erste Zelle");
                    return inputManager.GetGridCell(0);
                }
                if ((inputManager.GetCellObject(1).GetComponent<PlacedObject>() != null) && (inputManager.GetCellObject(1).isActiveAndEnabled == true))
                {
                    if (gameObject.GetComponent<PlacedObject>() == inputManager.GetCellObject(1).GetComponent<PlacedObject>())
                    {
                        //Debug.Log("zweite Zelle");
                        return inputManager.GetGridCell(1);
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
            else if((inputManager.GetCellObject(3).GetComponent<PlacedObject>() != null) && (inputManager.GetCellObject(3).isActiveAndEnabled == true))
            {
                if(gameObject.GetComponent<PlacedObject>() == inputManager.GetCellObject(3).GetComponent<PlacedObject>())
                {
                    //Debug.Log("vierte Zelle");
                    return inputManager.GetGridCell(3);
                }
                if ((inputManager.GetCellObject(4).GetComponent<PlacedObject>() != null) && (inputManager.GetCellObject(4).isActiveAndEnabled == true))
                {
                    if (gameObject.GetComponent<PlacedObject>() == inputManager.GetCellObject(4).GetComponent<PlacedObject>())
                    {
                        //Debug.Log("fünfte Zelle");
                        return inputManager.GetGridCell(4);
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
            else
            {
                Debug.Log("Keine Zelle gefunden");
                return null;
            }
        }
        if (enemy == true)
        {
            if ((inputManager.GetEnemyObject(0).GetComponent<PlacedObject>() != null) && (inputManager.GetEnemyObject(0).isActiveAndEnabled == true))
            {
                if (gameObject.GetComponent<PlacedObject>() == inputManager.GetEnemyObject(0).GetComponent<PlacedObject>())
                {
                    //Debug.Log("erste Zelle");
                    return inputManager.GetEnemyCell(0);
                }
                if ((inputManager.GetEnemyObject(1).GetComponent<PlacedObject>() != null) && (inputManager.GetEnemyObject(1).isActiveAndEnabled == true))
                {
                    if (gameObject.GetComponent<PlacedObject>() == inputManager.GetEnemyObject(1).GetComponent<PlacedObject>())
                    {
                        //Debug.Log("zweite Zelle");
                        return inputManager.GetEnemyCell(1);
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
            else if ((inputManager.GetEnemyObject(3).GetComponent<PlacedObject>() != null) && (inputManager.GetEnemyObject(3).isActiveAndEnabled == true))
            {
                if (gameObject.GetComponent<PlacedObject>() == inputManager.GetEnemyObject(3).GetComponent<PlacedObject>())
                {
                    //Debug.Log("vierte Zelle");
                    return inputManager.GetEnemyCell(3);
                }
                if ((inputManager.GetEnemyObject(4).GetComponent<PlacedObject>() != null) && (inputManager.GetEnemyObject(4).isActiveAndEnabled == true))
                {
                    if (gameObject.GetComponent<PlacedObject>() == inputManager.GetEnemyObject(4).GetComponent<PlacedObject>())
                    {
                        //Debug.Log("fünfte Zelle");
                        return inputManager.GetEnemyCell(4);
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
            else
            {
                Debug.Log("Keine Zelle gefunden");
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
        Debug.Log(attack + " " + health);

        if ((level == 4) && (firstLvlUp == false))
        {
            firstLvlUp = true;
            rank = 2;
            // Do things when reaching rank 2

            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else if ((level == 7) && (secondLvlUp == false))
        {
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
