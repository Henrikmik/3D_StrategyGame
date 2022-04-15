using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    private int posX;
    private int posY;
    private PlacedObject placedObject;

    // Saves a reference to the gameobject that gets placed on this cell
    public GameObject objectInThisGridSpace = null;

    // Saves if the grid space is occupied or not
    public bool isOccupied = false;

    // Set the position of the grid cell on the grid
    public void SetPosistion(int x, int y)
    {
        posX = x;
        posY = y;
    }

    // Get the position of this grid space on the grid
    public Vector2Int GetPosition()
    {
        return new Vector2Int(posX, posY);
    }

    //
    public void SetPlacedObject(PlacedObject placedObject)
    {
        this.placedObject = placedObject;
    }

    //
    public PlacedObject GetPlacedObject()
    {
        return placedObject;
    }

    // Sets the variable transform to null

    public void ClearTransform()
    {
        placedObject = null;
    }

    // Returns transform if it is zero
    public bool CanBuild()
    {
        return placedObject == null;
    }
}
