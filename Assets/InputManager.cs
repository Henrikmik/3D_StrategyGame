using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    GameGrid gameGrid;

    [SerializeField] private LayerMask whatIsAGridLayer;
    [SerializeField] private Transform testTransform;
    [SerializeField] private Unit unit;

    private float placementPosx;
    private float placementPosz;
    public Vector3 placementVec;

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
            //    cellMouseIsOver.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            if (Input.GetMouseButtonDown(0))
            {
                if (cellMouseIsOver.CanBuild())
                {
                    placementVec = new Vector3(cellMouseIsOver.GetComponent<Transform>().position.x, 1f, cellMouseIsOver.GetComponent<Transform>().position.z);
                    Transform builtTransform = Instantiate(unit.prefab, placementVec, Quaternion.identity);
                    cellMouseIsOver.SetTransform(builtTransform);
                    //cellMouseIsOver.isOccupied = true;
                }
                else
                {
                    Debug.Log("Cannot build atm! ");
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log(cellMouseIsOver.GetComponent<Transform>().position.x);
        }
        
        else
        {

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

    //public static Vector3 GetMouseWorldPosition() => InputManager.GetMouseWorldPosition_Instance();

    public Vector3 GetMouseWorldPosition_Instance()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 100f, whatIsAGridLayer))
        {
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
