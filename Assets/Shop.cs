using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public List<GameObject> shopSpace;
    public Unit[] buyableShopObjects;
    public Canvas canvas;
    public InputManager inputManager;
    public bool freezedShop = false;
    public GameObject hudPre3;
    public GameObject hudPost3;
    //public Sprite shopFreezed;
    //public Sprite shopUnfreezed;
    public GameObject freezeSprite;
    public GameObject unfreezeSprite;
    public Image FreezeButton;
    public GameObject FreezeImage;
    public GameObject UnfreezeImage;

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
    }

    public Unit RandomOption()
    {
        int randomOption = Random.Range(0, buyableShopObjects.Length);
        return buyableShopObjects[randomOption];
    }

    public void PlaceUnitInShop(GameObject e, Unit unit)
    {
        Vector3 pos3 = new Vector3 (e.transform.position.x + 0.1f, e.transform.position.y - 0.4f, e.transform.position.z - 0.1f);
        Vector2Int pos2 = new Vector2Int (0, 0);

        PlacedObject placedEnemy = PlacedObject.Create(pos3, pos2, Unit.Dir.Down, unit);
        placedEnemy.SettingStats();
        placedEnemy.transform.parent = e.transform;
        placedEnemy.transform.localScale += new Vector3 (0.7f, 0.7f, 0.7f);
        //placedEnemy.transform.rotation = new Quaternion (0, 0, 0, 0);
        inputManager.ShowFloatingText(placedEnemy, pos3, true);
        PlacedObject placedObject = transform.GetComponent<PlacedObject>();
        inputManager.levelUpdate.UpdateLevel(placedEnemy);

        placedEnemy.transform.GetChild(1).GetChild(0).GetComponent<Animator>().enabled = false;
        //placedEnemy.transform.GetChild(1).GetComponent<LockRotation>().enabled = true;
    }

    public void ShopReroll()
    {
        if (inputManager.gold >= 10)
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
                FindObjectOfType<AudioManager>().Play("Reroll");
                // costs 10 gold
                inputManager.gold -= 10;
                // Deleting shop objects
                foreach (GameObject g in shopSpace)
                {
                    if (g.transform.childCount >= 2)
                    {
                        if (g.transform.GetChild(1))
                        {
                            Destroy(g.transform.GetChild(1).gameObject);
                            //Debug.Log("L?schung");
                        }
                    }
                    else
                    {
                        //Debug.Log("Keine L?schung");
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
        else
        {
            FindObjectOfType<AudioManager>().Play("Error");
            inputManager.ShakeGold();
        }
    }
    public void FreezeShop()
    {
        if (freezedShop == true)
        {
            freezedShop = false;
            FreezeImage.SetActive(false);
            UnfreezeImage.SetActive(true);
            FindObjectOfType<AudioManager>().Play("Unfreeze");
            freezeSprite.SetActive(true);
            unfreezeSprite.SetActive(false);
            //FreezeButton.sprite = shopFreezed;
            //Debug.Log("Shop is not freezed");
        }
        else if (freezedShop == false)
        {
            freezedShop = true;
            FreezeImage.SetActive(true);
            UnfreezeImage.SetActive(false);
            FindObjectOfType<AudioManager>().Play("Freeze");
            freezeSprite.SetActive(false);
            unfreezeSprite.SetActive(true);

            //FreezeButton.sprite = shopUnfreezed;
            //Debug.Log("Shop is freezed");
        }
    }

    public void SellUnit()
    {
        PlacedObject pObj = inputManager.selectedPlacedObject;
        GridCell pGrid = inputManager.selectedPlacedObjectGrid;

        if (pObj != null)
        {
            FindObjectOfType<AudioManager>().Play("Sell");
            if (pObj.level >= 4)
            {
                inputManager.gold += 20;
            }
            else
            {
                inputManager.gold += 10;
            }

            pGrid.ClearTransform();
            pGrid.UnstoreObject(pObj);
            pObj.DestroySelf();

            inputManager.selectedPlacedObject = null;
            inputManager.selectedPlacedObjectGrid = null;
            inputManager.FreezeShop.SetActive(true);
            inputManager.Sell.SetActive(false);
        }
    }
}
