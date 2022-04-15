using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    GameGrid gameGrid;
    GridCell gridCell;

    [SerializeField] private LayerMask whatIsAGridLayer;
    [SerializeField] private Transform testTransform;
    [SerializeField] private List<Unit> unitList;
    private Unit unit;

    private Unit.Dir dir = Unit.Dir.Down;

    private float placementPosx;
    private float placementPosz;
    public Vector3 placementVec;

    // Start is called before the first frame update
    void Start()
    {
        gameGrid = FindObjectOfType<GameGrid>();
    }

    private void Awake()
    {
        unit = unitList[0];
    }

    // Update is called once per frame
    void Update()
    {
        GridCell cellMouseIsOver = IsMouseOverAGridSpace();

        if (Input.GetMouseButtonDown(0))
        {
            //    cellMouseIsOver.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            if (cellMouseIsOver != null)
            {
                int xList = (int)cellMouseIsOver.GetComponent<Transform>().position.x;
                int zList = (int)cellMouseIsOver.GetComponent<Transform>().position.z;
                List<Vector2Int> gridPositionList = unit.GetGridPositionList(new Vector2Int(xList, zList), dir);

                if (cellMouseIsOver.CanBuild())
                {
                    placementVec = new Vector3(cellMouseIsOver.GetComponent<Transform>().position.x, 1f, cellMouseIsOver.GetComponent<Transform>().position.z);
                    PlacedObject placedObject = PlacedObject.Create(placementVec, new Vector2Int(xList, zList), dir, unit);

                    //foreach (Vector2Int gridPosition in gridPositionList)
                    //{
                    //    gridCell.GetPosition(gridPosition.x, gridPosition.y).SetTransform(builtTransform);
                    //}
                    cellMouseIsOver.SetPlacedObject(placedObject);
                    //cellMouseIsOver.isOccupied = true;
                }
                else
                {
                    Debug.Log("Cannot build atm! ");
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (cellMouseIsOver != null)
            {
                GridCell gridCell = cellMouseIsOver;
                PlacedObject placedObject = gridCell.GetPlacedObject();
                if (placedObject != null)
                {
                    placedObject.DestroySelf();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log(cellMouseIsOver.GetComponent<Transform>().position.x);
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            dir = Unit.GetNextDir(dir);
            Debug.Log("" + dir);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) { unit = unitList[0]; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { unit = unitList[1]; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { unit = unitList[2]; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { unit = unitList[3]; }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { unit = unitList[4]; }
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
