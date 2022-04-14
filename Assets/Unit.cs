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
}
