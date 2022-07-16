using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowInfoText : MonoBehaviour
{
    public InputManager inputManager;
    public static Transform prefab;
    private TMP_Text oName;
    private TMP_Text abilityName;
    private TMP_Text ability;
    private PlacedObject placedObject;


    public static ShowInfoText Create(Vector3 infoPosition, PlacedObject placedObject, Transform prefab, GameObject canvas)
    {
        Transform showInfoTextTransform = Instantiate(prefab, infoPosition, Quaternion.Euler(0, 0, 0));

        ShowInfoText showInfoText = showInfoTextTransform.GetComponent<ShowInfoText>();

        showInfoText.placedObject = placedObject;
        showInfoText.transform.SetParent(canvas.transform);
        showInfoText.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        showInfoText.transform.rotation = new Quaternion(0, 0, 0, 0);

        return showInfoText;
    }

    void Start()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        prefab = inputManager.infoPrefab;
        oName = transform.GetChild(0).GetComponent<TMP_Text>();
        //abilityName = transform.GetChild(2).GetComponent<TMP_Text>();
        ability = transform.GetChild(1).GetComponent<TMP_Text>();
    }

    void Update()
    {
        placedObject = inputManager.IsMouseOverAplacedobject();

        if (placedObject != null)
        {
            oName.text = "Rank: " + placedObject.rank + " " + placedObject.nameA;
            //abilityName.text = placedObject.abilityName;

            ability.text = placedObject.abilityDescription;
            //Debug.Log("TextUpdate");
        }
    }

    // Destroys Gameobject
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
