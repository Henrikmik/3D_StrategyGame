using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    GridCell gridCell;

    public GameGrid gameGrid;
    public GameObject enemyManager;
    public GameObject unitManager;
    public BattleRound battleRound;
    public GameObject battleStart;
    public GameGridEnemy gameGridEnemyS;
    public Camera mainCamera;
    public GameObject canvas;
    public GameObject canvasLose;
    public Shop shop;

    [SerializeField] private LayerMask whatIsAGridLayer;
    [SerializeField] private LayerMask whatIsPlacedObjectLayer;
    [SerializeField] private Transform testTransform;
    [SerializeField] public List<Unit> unitList;
    private Unit unit;

    private Unit.Dir dir = Unit.Dir.Down;

    public Vector3 placementVec;
    public GameObject FloatingTextPrefab;

    public bool battleOn = false;
    public bool roundover = true;

    public int healthE = 10;
    public int healthU = 10;
    public int roundCounter = 1;
    public int gold = 100;
    public int draws = 1;

    public PlacedObject selectedPlacedObject;
    public GridCell selectedPlacedObjectGrid;

    public GameObject FreezeShop;
    public GameObject Sell;

    public ShowInfoText showInfotext;
    public Transform infoPrefab;

    private int o = 1;
    // Start is called before the first frame update
    void Start()
    {
        gameGrid = FindObjectOfType<GameGrid>();
        FreezeShop = GameObject.Find("FreezeShop");
    }

    private void Awake()
    {
        unit = unitList[0];
        gold = 100;
    }

    private ShowInfoText showInfo;
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(IsMouseOverAplacedobject());
        GridCell cellMouseIsOver = IsMouseOverAGridSpace();
        PlacedObject hoveredObject = IsMouseOverAplacedobject();

        if (battleOn != true)
        {
            if ((hoveredObject != null) && (o == 1))   //  
            {
                ShowInfoText showinfoText = ShowInfoText.Create(new Vector3(6.8f, 4.5f, 0), hoveredObject, infoPrefab, canvas);
                showInfo = showinfoText;
                //Debug.Log("INFO");
                o += 1;
            }
            if ((hoveredObject == null) && (showInfo != null) && (o != 1))   // 
            {
                showInfo.DestroySelf();
                //Debug.Log("Destroyed");
                o = 1;
            }

            UpdateHierarchie();

            if (Input.GetMouseButtonDown(1))
            {
                if (cellMouseIsOver != null)
                {
                    selectedPlacedObjectGrid = cellMouseIsOver;
                    PlacedObject placedObject = selectedPlacedObjectGrid.GetPlacedObject();
                    if (selectedPlacedObject != null)
                    {
                        selectedPlacedObject.gameObject.transform.position = new Vector3
                            (selectedPlacedObject.gameObject.transform.position.x, 1f, selectedPlacedObject.gameObject.transform.position.z);
                    }

                    selectedPlacedObject = placedObject;
                    selectedPlacedObject.gameObject.transform.position = new Vector3
                        (selectedPlacedObject.gameObject.transform.position.x, 1.1f, selectedPlacedObject.gameObject.transform.position.z);
                    FreezeShop.SetActive(false);
                    Sell.SetActive(true);
                }
                if (cellMouseIsOver == null)
                {
                    if (selectedPlacedObject != null)
                    {
                        selectedPlacedObject.gameObject.transform.position = new Vector3
                            (selectedPlacedObject.gameObject.transform.position.x, 1f, selectedPlacedObject.gameObject.transform.position.z);
                    }
                    selectedPlacedObject = null;
                    selectedPlacedObjectGrid = null;
                    FreezeShop.SetActive(true);
                    Sell.SetActive(false);
                }
            }

            // Debug Key Inputs

            if (Input.GetKeyDown(KeyCode.I))
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
            roundCounter = 3;
            shop.ShopReroll();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            gold += 100;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            DestroyField();
        }
    }

    // Returns the grid cell if mouse is over grid cell and returns null if it is not
    public GridCell IsMouseOverAGridSpace()
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

    // returns placedobject if mouse is over placed object and returns null if it is not
    public PlacedObject IsMouseOverAplacedobject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, whatIsPlacedObjectLayer))
        {
            return hitInfo.transform.GetComponent<PlacedObject>();
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

        var myNewStats = Instantiate(FloatingTextPrefab, objectPos, Quaternion.Euler(cameraAnglex + 40f, cameraAngley - 60f, -20f), transform);
        myNewStats.transform.parent = placedObject.transform;
        myNewStats.GetComponent<TextMesh>().text = myNewStats.GetComponentInParent<PlacedObject>().nameA
            + "\n Attack: " + myNewStats.GetComponentInParent<PlacedObject>().attack 
            + "\n Health: " + myNewStats.GetComponentInParent<PlacedObject>().health 
            + "\n Level: " + myNewStats.GetComponentInParent<PlacedObject>().level;
    }

    public void UpdateFloatingText(PlacedObject placedObject)
    {
        placedObject.GetComponentInChildren<TextMesh>().text = placedObject.GetComponentInParent<PlacedObject>().nameA
            + "\n Attack: " + placedObject.GetComponentInParent<PlacedObject>().attack
            + "\n Health: " + placedObject.GetComponentInParent<PlacedObject>().health
            + "\n Level: " + placedObject.GetComponentInParent<PlacedObject>().level;
    }

    public void StartBattlePhase()
    {
        // Sets battle variable to true
        battleOn = true;

        // Creates Enemy Grid
        gameGridEnemyS.CreateEnemyGrid(0, 1);
        gameGridEnemyS.CreateEnemyGrid(0, 2);
        gameGridEnemyS.CreateEnemyGrid(0, 3);

        // Creates extenden enemy Grid
        if (roundCounter >= 3)
        {
            gameGridEnemyS.CreateEnemyGrid(2, 1);
            gameGridEnemyS.CreateEnemyGrid(2, 2);
            gameGridEnemyS.CreateEnemyGrid(2, 3);

            // Gets the first Gric Cell of the second lane
            GridCell enemyGridCell4 = gameGridEnemyS.transform.GetChild(3).GetComponent<GridCell>();

            // Gets the second Grid Cell of the second lane
            GridCell enemyGridCell5 = gameGridEnemyS.transform.GetChild(4).GetComponent<GridCell>();

            // Gets the third Gric Cell of the second lane
            GridCell enemyGridCell6 = gameGridEnemyS.transform.GetChild(5).GetComponent<GridCell>();

            // Creates enemy on second lane, first grid cell
            Unit enemy4 = unitList[10];
            Vector2Int enemy4Pos = enemyGridCell4.GetPosition();
            Vector3 enemy4Pos3 = new Vector3(enemyGridCell4.transform.position.x, 1f, enemyGridCell4.transform.position.z);

            PlacedObject placedEnemy4 = PlacedObject.Create(enemy4Pos3, enemy4Pos, Unit.Dir.Down, enemy4);
            enemyGridCell4.SetPlacedObject(placedEnemy4);
            enemyGridCell4.StoreObject(placedEnemy4);
            placedEnemy4.transform.SetParent(enemyManager.transform);
            placedEnemy4.SettingStats();
            ShowFloatingText(placedEnemy4, enemy4Pos3);

            // Creates enemy on second lane, second grid cell
            Unit enemy5 = unitList[11];
            Vector2Int enemy5Pos = enemyGridCell5.GetPosition();
            Vector3 enemy5Pos3 = new Vector3(enemyGridCell5.transform.position.x, 1f, enemyGridCell5.transform.position.z);

            PlacedObject placedEnemy5 = PlacedObject.Create(enemy5Pos3, enemy5Pos, Unit.Dir.Down, enemy5);
            enemyGridCell5.SetPlacedObject(placedEnemy5);
            enemyGridCell5.StoreObject(placedEnemy5);
            placedEnemy5.transform.SetParent(enemyManager.transform);
            placedEnemy5.SettingStats();
            ShowFloatingText(placedEnemy5, enemy5Pos3);

            // Creates enemy on second lane, third grid cell
            Unit enemy6 = unitList[12];
            Vector2Int enemy6Pos = enemyGridCell6.GetPosition();
            Vector3 enemy6Pos3 = new Vector3(enemyGridCell6.transform.position.x, 1f, enemyGridCell6.transform.position.z);

            PlacedObject placedEnemy6 = PlacedObject.Create(enemy6Pos3, enemy6Pos, Unit.Dir.Down, enemy6);
            enemyGridCell6.SetPlacedObject(placedEnemy6);
            enemyGridCell6.StoreObject(placedEnemy6);
            placedEnemy6.transform.SetParent(enemyManager.transform);
            placedEnemy6.SettingStats();
            ShowFloatingText(placedEnemy6, enemy6Pos3);
        }

        // Gets the first enemy grid cell
        GridCell enemyGridCell = gameGridEnemyS.transform.GetChild(0).GetComponent<GridCell>();

        // Gets the second enemy grid cell
        GridCell enemyGridCell2 = gameGridEnemyS.transform.GetChild(1).GetComponent<GridCell>();

        // Gets the third enemy grid cell
        GridCell enemyGridCell3 = gameGridEnemyS.transform.GetChild(2).GetComponent<GridCell>();

        // Updates Canvas
        UpdateCanvas(1);

        // Creates enemy on the first grid cell
        Unit enemy = unitList[7];
        Vector2Int enemyPos = enemyGridCell.GetPosition();
        Vector3 enemyPos3 = new Vector3(enemyGridCell.transform.position.x, 1f, enemyGridCell.transform.position.z);

        PlacedObject placedEnemy = PlacedObject.Create(enemyPos3, enemyPos, Unit.Dir.Down, enemy);
        enemyGridCell.SetPlacedObject(placedEnemy);
        enemyGridCell.StoreObject(placedEnemy);
        placedEnemy.transform.SetParent(enemyManager.transform);
        placedEnemy.SettingStats();
        ShowFloatingText(placedEnemy, enemyPos3);

        // Creates enemy on second grid cell
        Unit enemy2 = unitList[8];
        Vector2Int enemy2Pos = enemyGridCell2.GetPosition();
        Vector3 enemy2Pos3 = new Vector3(enemyGridCell2.transform.position.x, 1f, enemyGridCell2.transform.position.z);

        PlacedObject placedEnemy2 = PlacedObject.Create(enemy2Pos3, enemy2Pos, Unit.Dir.Down, enemy2);
        enemyGridCell2.SetPlacedObject(placedEnemy2);
        enemyGridCell2.StoreObject(placedEnemy2);
        placedEnemy2.transform.SetParent(enemyManager.transform);
        placedEnemy2.SettingStats();
        ShowFloatingText(placedEnemy2, enemy2Pos3);

        // Creates enemy on third grid cell
        Unit enemy3 = unitList[9];
        Vector2Int enemy3Pos = enemyGridCell3.GetPosition();
        Vector3 enemy3Pos3 = new Vector3(enemyGridCell3.transform.position.x, 1f, enemyGridCell3.transform.position.z);

        PlacedObject placedEnemy3 = PlacedObject.Create(enemy3Pos3, enemy3Pos, Unit.Dir.Down, enemy3);
        enemyGridCell3.SetPlacedObject(placedEnemy3);
        enemyGridCell3.StoreObject(placedEnemy3);
        placedEnemy3.transform.SetParent(enemyManager.transform);
        placedEnemy3.SettingStats();
        ShowFloatingText(placedEnemy3, enemy3Pos3);
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
            // Sets Shop in Canvas active
            canvas.transform.GetChild(0).gameObject.SetActive(true);
            // Sets battle phase button active
            canvas.transform.GetChild(1).gameObject.SetActive(true);
            // Sets start battle button inactive
            canvas.transform.GetChild(2).gameObject.SetActive(false);
            // Sets gold to 100
            if (roundCounter >= 3)
            {
                gold = 210;
            }
            else
            {
                gold = 110;
            }
        }
    }

    public void SetRoundoverFalse()
    {
        roundover = false;
        battleRound.StartBattle();
    }

    public void DestroyField()
    {
        for (int i = 0; i < gameGridEnemyS.transform.childCount; i++)
        {
            GameObject gridDestroy = gameGridEnemyS.transform.GetChild(i).gameObject;
            Destroy(gridDestroy);
        }
    }

    public void PlaceOnGridCell(GridCell cellMouseIsOver)
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
    }   //Unnötig

    public void DragOnGridCell(GridCell cellMouseIsOver, PlacedObject placedObject)
    {
        placementVec = new Vector3(cellMouseIsOver.GetComponent<Transform>().position.x, 1f, cellMouseIsOver.GetComponent<Transform>().position.z);
        placedObject.transform.position = placementVec;
        placedObject.transform.SetParent(null);
        placedObject.transform.SetParent(unitManager.transform);

        if (placedObject.level < 4)
        {
            placedObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (placedObject.level < 7)
        {
            placedObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        else
        {
            placedObject.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }

        placedObject.transform.rotation = new Quaternion (0, 0, 0, 0);
        cellMouseIsOver.StoreObject(placedObject);
        cellMouseIsOver.SetPlacedObject(placedObject);
        placedObject.transform.GetChild(0).transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
        placedObject.transform.GetChild(0).transform.rotation = Quaternion.Euler (40, -55, 0);
    }

    public void UpdateHierarchie()
    {
        if ((gameGrid.transform.childCount > 2) && (gameGrid.transform.GetChild(1).gameObject.activeInHierarchy == true) && (gameGrid.transform.GetChild(2).gameObject.activeInHierarchy == true))
        {
            if (GetCellObject(0) != null)
            {
                GetCellObject(0).transform.SetSiblingIndex(0);
            }
            if (GetCellObject(1) != null)
            {
                GetCellObject(1).transform.SetSiblingIndex(1);
            }
            if (GetCellObject(2) != null)
            {
                GetCellObject(2).transform.SetSiblingIndex(2);
            }
            // Update Hierarchy lane 2
            if (roundCounter >= 3)
            {
                if (GetCellObject(3) != null)
                {
                    GetCellObject(3).transform.SetSiblingIndex(3);
                }
                if (GetCellObject(4) != null)
                {
                    GetCellObject(4).transform.SetSiblingIndex(4);
                }
                if (GetCellObject(5) != null)
                {
                    GetCellObject(5).transform.SetSiblingIndex(5);
                }
            }
        }
    }
}

