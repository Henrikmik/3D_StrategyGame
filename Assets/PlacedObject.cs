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

    public void SettingStats()
    {
        attack = unit.attack;
        health = unit.health;
        level = unit.level;
        nameA = unit.name;
        ability = unit.ability;

        baseAttack = unit.attack;
        baseHealth = unit.health;
        baseLevel = unit.level;

        if (ability == "coconut")
        {
            armor = true;
        }
        else
        {
            armor = false;
        }
    }

    public void SetStats()
    {
        attack = baseAttack;
        health = baseHealth;
        level = baseLevel;
        armor = true;

        if (ability == "coconut")
        {
            armor = true;
        }
        else
        {
            armor = false;
        }
    }
}
