using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<GameObject> shopSpace;
    public Unit[] buyableShopObjects;
    public Canvas canvas;
    public InputManager inputManager;
    public void SetShoppingSpace()
    {
        shopSpace.Add(canvas.transform.GetChild(0).GetChild(0).gameObject);
        shopSpace.Add(canvas.transform.GetChild(0).GetChild(1).gameObject);
        shopSpace.Add(canvas.transform.GetChild(0).GetChild(2).gameObject);
        shopSpace.Add(canvas.transform.GetChild(0).GetChild(3).gameObject);

        foreach (GameObject e in shopSpace)
        {
            Unit unit = RandomOption();
            PlaceUnitInShop(e, unit);
            
            //Debug.Log(e);

        }
    }

    public void Start()
    {
        SetShoppingSpace();
        //Debug.Log(RandomOption());
    }

    public Unit RandomOption()
    {
        int randomOption = Random.Range(0, buyableShopObjects.Length);
        return buyableShopObjects[randomOption];
    }

    public void PlaceUnitInShop(GameObject e, Unit unit)
    {
        Vector3 pos3 = e.transform.position;
        Vector2Int pos2 = new Vector2Int (0, 0);

        PlacedObject placedEnemy = PlacedObject.Create(pos3, pos2, Unit.Dir.Down, unit);
        placedEnemy.SettingStats();
        placedEnemy.transform.parent = e.transform;
        placedEnemy.transform.localScale += new Vector3 (70, 70, 70);
        placedEnemy.transform.rotation = new Quaternion (0, 0, 0, 0);
        inputManager.ShowFloatingText(placedEnemy, pos3);
    }

    public void ShopReroll()
    {
        // Deleting shop objects
        foreach (GameObject g in shopSpace)
        {
            if (g.transform.childCount >= 3)
            {
                if (g.transform.GetChild(2))
                {
                    Destroy(g.transform.GetChild(2).gameObject);
                    //Debug.Log("L�schung");
                }
            }
            else
            {
                //Debug.Log("Keine L�schung");
            }
        }

        // Generating new shop objects
        foreach (GameObject e in shopSpace)
        {
            //Debug.Log("Neue Unit");
            Unit unit = RandomOption();
            PlaceUnitInShop(e, unit);
        }
    }
}
