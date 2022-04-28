using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    GameGrid gameGrid;
    GridCell gridCell;
    public BattleRound battleRound;
    public GameObject battleStart;
    public GameGridEnemy gameGridEnemyS;
    public Camera mainCamera;
    public GameObject canvas;
    public Shop shop;

    [SerializeField] private LayerMask whatIsAGridLayer;
    [SerializeField] private Transform testTransform;
    [SerializeField] private List<Unit> unitList;
    private Unit unit;

    private Unit.Dir dir = Unit.Dir.Down;

    public Vector3 placementVec;
    public GameObject FloatingTextPrefab;

    public bool battleOn = false;
    public bool roundover = true;

    public int healthE = 10;
    public int healthU = 10;
    public int roundCounter = 1;

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

        if (battleOn != true)
        {
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
                        cellMouseIsOver.StoreObject(placedObject);
                        cellMouseIsOver.SetPlacedObject(placedObject);
                        placedObject.SettingStats();
                        ShowFloatingText(placedObject, placementVec);
                        //foreach (Vector2Int gridPosition in gridPositionList)
                        //{
                        //    gridCell.GetPosition(gridPosition.x, gridPosition.y).SetTransform(builtTransform);
                        //}
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



        if (Input.GetKeyDown(KeyCode.U))
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

    public void ShowFloatingText(PlacedObject placedObject, Vector3 objectPos)
    {
        float cameraAnglex = mainCamera.GetComponent<Transform>().rotation.x;
        float cameraAngley = mainCamera.GetComponent<Transform>().rotation.y;
        float cameraAnglez = mainCamera.GetComponent<Transform>().rotation.z;

        var myNewStats = Instantiate(FloatingTextPrefab, objectPos, Quaternion.Euler(cameraAnglex + 20f, cameraAngley - 50f, 0), transform);
        myNewStats.transform.parent = placedObject.transform;
        myNewStats.GetComponent<TextMesh>().text = myNewStats.GetComponentInParent<PlacedObject>().nameA + "\n Attack: " + myNewStats.GetComponentInParent<PlacedObject>().attack +"\n Health: " + myNewStats.GetComponentInParent<PlacedObject>().health;
    }


    public void UpdateFloatingText(PlacedObject placedObject)
    {
        placedObject.GetComponentInChildren<TextMesh>().text = placedObject.GetComponentInParent<PlacedObject>().nameA + "\n Attack: " + placedObject.GetComponentInParent<PlacedObject>().attack + "\n Health: " + placedObject.GetComponentInParent<PlacedObject>().health;
    }
    public void StartBattlePhase()
    {
        // Sets battle variable to true
        battleOn = true;

        // Creates Enemy Grid
        gameGridEnemyS.CreateEnemyGrid(0, 1);
        gameGridEnemyS.CreateEnemyGrid(0, 2);
        gameGridEnemyS.CreateEnemyGrid(0, 3);

        gameGridEnemyS.CreateEnemyGrid(2, 1);
        gameGridEnemyS.CreateEnemyGrid(2, 2);
        gameGridEnemyS.CreateEnemyGrid(2, 3);

        // Gets the first enemy grid cell
        GridCell enemyGridCell = gameGridEnemyS.transform.GetChild(0).GetComponent<GridCell>();

        // Gets the second enemy grid cell
        GridCell enemyGridCell2 = gameGridEnemyS.transform.GetChild(1).GetComponent<GridCell>();

        // Updates Canvas
        UpdateCanvas(1);

        // Creates enemy on the first grid cell
        Unit enemy = unitList[5];
        Vector2Int enemyPos = enemyGridCell.GetPosition();
        Vector3 enemyPos3 = new Vector3(enemyGridCell.transform.position.x, 1f, enemyGridCell.transform.position.z);

        PlacedObject placedEnemy = PlacedObject.Create(enemyPos3, enemyPos, Unit.Dir.Down, enemy);
        enemyGridCell.SetPlacedObject(placedEnemy);
        enemyGridCell.StoreObject(placedEnemy);
        placedEnemy.SettingStats();
        ShowFloatingText(placedEnemy, enemyPos3);

        // Creates enemy on second grid cell
        Unit enemy2 = unitList[6];
        Vector2Int enemy2Pos = enemyGridCell2.GetPosition();
        Vector3 enemy2Pos3 = new Vector3(enemyGridCell2.transform.position.x, 1f, enemyGridCell2.transform.position.z);

        PlacedObject placedEnemy2 = PlacedObject.Create(enemy2Pos3, enemy2Pos, Unit.Dir.Down, enemy2);
        enemyGridCell2.SetPlacedObject(placedEnemy2);
        enemyGridCell2.StoreObject(placedEnemy2);
        placedEnemy2.SettingStats();
        ShowFloatingText(placedEnemy2, enemy2Pos3);
    }

    // Gets grid cell
    public GameObject GetGridCell(int pos)
    {
        return gameGrid.transform.GetChild(pos).gameObject;
    }

    // Gets placed object in grid cell
    public PlacedObject GetCellObject(int pos)
    {
        return GetGridCell(pos).GetComponent<GridCell>().GetPlacedObject();
    }

    // Gets enemy grid cell
    public GameObject GetEnemyCell(int pos)
    {
        return gameGridEnemyS.transform.GetChild(pos).gameObject;
    }

    // Gets placed object in enemy grid cell
    public PlacedObject GetEnemyObject(int pos)
    {
        return GetEnemyCell(pos).GetComponent<GridCell>().GetPlacedObject();
    }

    // Sets Canvas to Battle Canvas
    public void UpdateCanvas(int scene)
    {
        if (scene == 1)
        {
            // Sets Shop in Canvas inactive
            canvas.transform.GetChild(0).gameObject.SetActive(false);
            // Sets battle phase button inactive
            canvas.transform.GetChild(1).gameObject.SetActive(false);
            // Sets start battle button active
            canvas.transform.GetChild(2).gameObject.SetActive(true);
        }

        if (scene == 2)
        {
            // Sets Shop in Canvas inactive
            canvas.transform.GetChild(0).gameObject.SetActive(true);
            // Sets battle phase button inactive
            canvas.transform.GetChild(1).gameObject.SetActive(true);
            // Sets start battle button active
            canvas.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    public void SetRoundoverFalse()
    {
        roundover = false;
        battleRound.StartBattle();
    }

    public void DestroyField()
    {
        GameObject gridEins = gameGridEnemyS.transform.GetChild(0).gameObject;
        GameObject gridZwei = gameGridEnemyS.transform.GetChild(1).gameObject;
        GameObject gridDrei = gameGridEnemyS.transform.GetChild(2).gameObject;

        Destroy(gridEins.GetComponent<GridCell>().GetPlacedObject().gameObject);
        Destroy(gridZwei.GetComponent<GridCell>().GetPlacedObject().gameObject);
        Destroy(gridZwei.GetComponent<GridCell>().GetPlacedObject().gameObject);
        Destroy(gridEins);
        Destroy(gridZwei);
        Destroy(gridDrei);
    }
}

