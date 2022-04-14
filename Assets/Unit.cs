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

    public List<Vector2Int> GetGridPositionList(Vector2Int offset)
    {
        List<Vector2Int> gridPositionList = new List<Vector2Int>();
        //switch (dir)
        {
            //default:
            //case Dir.Down:
            //case Dir.Up:
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < width; y++)
                    {
                        gridPositionList.Add(offset + new Vector2Int(x, y));
                    }
                }
                //break;
        }
        return gridPositionList;
    }
}