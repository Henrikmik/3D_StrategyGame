using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    GameGrid gameGrid;
    [SerializeField] private LayerMask whatIsAGridLayer;

    // Start is called before the first frame update
    void Start()
    {
        gameGrid = FindObjectOfType<GameGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        GridCell cellMouseIsOver = IsMouseOverAGridSpace();
        if (cellMouseIsOver != null)
        {
            if (Input.GetMouseButton(0))
            {
                cellMouseIsOver.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
                // Debug.Log(cellMouseIsOver.isOccupied);
            }
        }
    }


    // Returns the grid cell if mouse is over grid cell and returns null if it is not
    private GridCell IsMouseOverAGridSpace()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, whatIsAGridLayer))
        {
            return hitInfo.transform.GetComponent<GridCell>();          
        }
        else
        {
            return null;
        }
    }
}
