using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public List<GameObject> shopSpace;
    public Unit[] buyableShopObjects;
    public Canvas canvas;
    public InputManager inputManager;
    public bool freezedShop = false;
    public GameObject hudPre3;
    public GameObject hudPost3;
    public void SetShoppingSpace()
    {
        hudPre3.SetActive(true);
        hudPost3.SetActive(false);

        shopSpace.Add(canvas.transform.GetChild(0).GetChild(0).gameObject);
        shopSpace.Add(canvas.transform.GetChild(0).GetChild(1).gameObject);
        shopSpace.Add(canvas.transform.GetChild(0).GetChild(2).gameObject);

        foreach (GameObject e in shopSpace)
        {
            Unit unit = RandomOption();
            PlaceUnitInShop(e, unit);
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
        placedEnemy.transform.localScale += new Vector3 (40, 40, 40);
        placedEnemy.transform.rotation = new Quaternion (0, 0, 0, 0);
        inputManager.ShowFloatingText(placedEnemy, pos3);
    }

    public void ShopReroll()
    {
        if (inputManager.roundCounter < 3)
        {
            hudPre3.SetActive(true);
            hudPost3.SetActive(false);

            shopSpace.Clear();

            shopSpace.Add(canvas.transform.GetChild(0).GetChild(0).gameObject);
            shopSpace.Add(canvas.transform.GetChild(0).GetChild(1).gameObject);
            shopSpace.Add(canvas.transform.GetChild(0).GetChild(2).gameObject);
        }
        else if (inputManager.roundCounter >= 3)
        {
            hudPre3.SetActive(false);
            hudPost3.SetActive(true);

            shopSpace.Clear();

            shopSpace.Add(canvas.transform.GetChild(0).GetChild(0).gameObject);
            shopSpace.Add(canvas.transform.GetChild(0).GetChild(1).gameObject);
            shopSpace.Add(canvas.transform.GetChild(0).GetChild(2).gameObject);
            shopSpace.Add(canvas.transform.GetChild(0).GetChild(3).gameObject);
            shopSpace.Add(canvas.transform.GetChild(0).GetChild(4).gameObject);
        }




        if (freezedShop == false)
        {
            // Deleting shop objects
            foreach (GameObject g in shopSpace)
            {
                if (g.transform.childCount >= 2)
                {
                    if (g.transform.GetChild(1))
                    {
                        Destroy(g.transform.GetChild(1).gameObject);
                        //Debug.Log("Löschung");
                    }
                }
                else
                {
                    //Debug.Log("Keine Löschung");
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
        else
        {

        }

    }
    public void FreezeShop()
    {
        if (freezedShop == true)
        {
            freezedShop = false;
            //Debug.Log("Shop is not freezed");
        }
        else if (freezedShop == false)
        {
            freezedShop = true;
            //Debug.Log("Shop is freezed");
        }
    }
}
