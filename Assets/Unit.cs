using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class Unit : ScriptableObject
{
    public new string name;
    public string description;
    public string ability;

    public Transform prefab;
    public Transform visual;

    public int width;
    public int height;
    public int attack;
    public int health;
    public int level;
    public int cost;

    // ability
    public string abilityName;
    public string abilityDescription1;
    public string abilityDescription2;
    public string abilityDescription3;

    // Gets the next Direction
    //public static Dir GetNextDir(Dir dir)
    //{
    //    switch (dir)
    //    {
    //        default:
    //        case Dir.Down:      return Dir.Left;
    //        case Dir.Left:      return Dir.Up;
    //        case Dir.Up:        return Dir.Right;
    //        case Dir.Right:     return Dir.Down;
    //    }
    //}
    public enum Dir
    {
        Down,
        Left,
        Up,
        Right,
        Apple,
        Broccoli,
        Cherry,
        Coconut,
        Corn,
        Eggplant,
        Garlic,
        Grapes,
        Lemon,
        MiniGrape,
        Pineapple,
        Pumpkin,
        Tomato,
    }
    public int GetRotationAngle(Dir dir)
    {
        switch (dir)
        {
            default:
            case Dir.Down:      return 0;
            case Dir.Left:      return 90;
            case Dir.Up:        return 180;
            case Dir.Right:     return 270;
            case Dir.Apple:     return 120;
            case Dir.Cherry:    return 135;
            case Dir.Coconut:   return -85;
            case Dir.Grapes:    return 160;
            case Dir.Lemon:     return 0;
            case Dir.MiniGrape: return 0;
            case Dir.Pineapple: return 235;
        }
    }

    public List<Vector2Int> GetGridPositionList(Vector2Int offset, Dir dir)
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>();
        switch (dir)
        {
            default:
            case Dir.Down:
            case Dir.Up:
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        gridPositionList.Add(offset + new Vector2Int(x, y));
                    }
                }
                break;
            case Dir.Left:
            case Dir.Right:
                for (int x = 0; x < height; x++)
                {
                    for (int y = 0; y < width; y++)
                    {
                        gridPositionList.Add(offset + new Vector2Int(x, y));
                    }
                }
                break;
        }
        return gridPositionList;
    }

    public Dir GetUnitDir()
    {
        if (name == "Apple")
        {
            return Dir.Apple;
        }
        else if (name == "Cherry")
        {
            return Dir.Cherry;
        }
        else if (name == "Coconut")
        {
            return Dir.Coconut;
        }
        else if (name == "Grapes")
        {
            return Dir.Grapes;
        }
        else if (name == "Lemon")
        {
            return Dir.Lemon;
        }
        else if (name == "Mini Grape")
        {
            return Dir.MiniGrape;
        }
        else if (name == "Pineapple")
        {
            return Dir.Pineapple;
        }
        else
        {
            return Dir.Down;
        }
    }
}