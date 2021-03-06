using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{

    private int height = -2;
    private int width = 1;
    private float gridSpaceSize = 1.3f;
    private int gridCreateX;
    private int gridCreateY;
    InputManager inputManager;
    public bool createGrid2 = true;
    public bool created2Grid = true;
    private bool c2Grid = true;

    [SerializeField] private GameObject gridCellPrefab;
    private GameObject gameGrid;
    private GameObject gameGrid2;


    void Start()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        gameGrid2 = GameObject.Find("GameGrid2");
        StartCoroutine(CreateGrid());
    }

    void Update()
    {
        if ((inputManager.roundCounter == 3) && (createGrid2 == true) && (inputManager.battleOn == false))
        {
            StartCoroutine(CreateGrid());
            createGrid2 = false;
        }

        if ((c2Grid == true) && (transform.childCount >= 6))
        {
            created2Grid = false;
            c2Grid = false;
        }
    }

    // Creates the grid when the game starts
    private IEnumerator CreateGrid()
    {
        int x = 0;

        // Changing variables for building the extended Grid
        if (inputManager.roundCounter == 3)
        {
            x = 2;
            //gameGrid = gameGrid2;
        }    

        // checking if there is a Grid Cell Prefab
        if (gridCellPrefab == null)
        {
            Debug.LogError("ERROR: Grid Cell Prefab on the Game grid is not assigned");
            yield return null;
        }

        // Make the grid
        for (int y = 1; y > height; y--)
        {
            gameGrid = Instantiate(gridCellPrefab, new Vector3(x * gridSpaceSize, 0, y * gridSpaceSize), Quaternion.identity);
            gameGrid.GetComponent<GridCell>().SetPosistion(x, y);
            gameGrid.transform.parent = transform;
            gameGrid.transform.eulerAngles = new Vector3(270f, 0f, 0f);
            gameGrid.gameObject.name = "Grid Space (X: " + x.ToString() + " , Y: " + y.ToString() + ")";

            yield return new WaitForSeconds(.1f);
        }
    }

    // Gets the grid position from world position
    public Vector2Int GetGridPosFromWorld(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / gridSpaceSize);
        int y = Mathf.FloorToInt(worldPosition.z / gridSpaceSize);

        x = Mathf.Clamp(x, 0, width);
        y = Mathf.Clamp(x, 0, height);

        return new Vector2Int(x, y);
    }

    // Gets the world position of a grid position
    public Vector3 GetWorldPosFromGridPos(Vector2Int gridPos)
    {
        float x = gridPos.x * gridSpaceSize;
        float y = gridPos.y * gridSpaceSize;

        return new Vector3(x, 0, y);
    }
}
