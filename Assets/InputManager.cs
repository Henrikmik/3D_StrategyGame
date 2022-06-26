using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

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
    public GameObject goldO;
    public GameObject drawsO;

    public GameObject canvas;
    public GameObject canvasLose;
    public Shop shop;

    [SerializeField] private LayerMask whatIsAGridLayer;
    [SerializeField] private LayerMask whatIsPlacedObjectLayer;
    [SerializeField] private Transform testTransform;
    [SerializeField] public List<Unit> unitList;
    [SerializeField] public LevelUpdate levelUpdate;
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

    public Camera c_Cam;
    public EnemySets enemySets;
    private int o = 1;
    private Vector3 mainCamPos;
    private Vector3 mainCamRot;

    public PlacedObject pOPosition1;
    public PlacedObject pOPosition2;
    public PlacedObject pOPosition3;
    public PlacedObject pOPosition4;
    public PlacedObject pOPosition5;
    public PlacedObject pOPosition6;

    // Start is called before the first frame update
    void Start()
    {
        gameGrid = FindObjectOfType<GameGrid>();
        FreezeShop = GameObject.Find("FreezeShop");
        enemySets = GameObject.Find("BattleManager").GetComponent<EnemySets>();
        mainCamPos = mainCamera.transform.parent.position;
        mainCamRot = mainCamera.transform.parent.eulerAngles;
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

        if (roundCounter >= 8)
        {
            SceneManager.LoadScene("Outro");
        }

        if (battleOn != true)
        {
            if ((hoveredObject != null) && (o == 1) && (InPauseMenuOrNot() == false) && (InHelpMenuOrNot() == false))   //  
            {
                ShowInfoText showinfoText = ShowInfoText.Create(new Vector3(81, 4.5f, 10), hoveredObject, infoPrefab, canvas);
                showInfo = showinfoText;
                //Debug.Log("INFO");
                o += 1;
            }
            if ((hoveredObject == null) && (showInfo != null) && (o != 1) && (InPauseMenuOrNot() == false) && (InHelpMenuOrNot() == false))   // 
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
                            (selectedPlacedObject.gameObject.transform.position.x, 0f, selectedPlacedObject.gameObject.transform.position.z);
                    }

                    selectedPlacedObject = placedObject;
                    selectedPlacedObject.gameObject.transform.position = new Vector3
                        (selectedPlacedObject.gameObject.transform.position.x, 0.15f, selectedPlacedObject.gameObject.transform.position.z);
                    FreezeShop.SetActive(false);
                    Sell.SetActive(true);
                }
                if (cellMouseIsOver == null)
                {
                    if (selectedPlacedObject != null)
                    {
                        selectedPlacedObject.gameObject.transform.position = new Vector3
                            (selectedPlacedObject.gameObject.transform.position.x, 0f, selectedPlacedObject.gameObject.transform.position.z);
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
                roundCounter = 8;
            }

            //if (Input.GetKeyDown(KeyCode.Alpha1)) { unit = unitList[0]; }
            //if (Input.GetKeyDown(KeyCode.Alpha2)) { unit = unitList[1]; }
            //if (Input.GetKeyDown(KeyCode.Alpha3)) { unit = unitList[2]; }
            //if (Input.GetKeyDown(KeyCode.Alpha4)) { unit = unitList[3]; }
            //if (Input.GetKeyDown(KeyCode.Alpha5)) { unit = unitList[4]; }
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
            CameraShake();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Animator camAnim = mainCamera.GetComponent<Animator>();

            camAnim.enabled = true;
            camAnim.SetBool("CameraSwing", true);
            //Debug.Log(camAnim.GetCurrentAnimatorStateInfo(0).length > camAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            //if (camAnim.GetCurrentAnimatorStateInfo(0).length > camAnim.GetCurrentAnimatorStateInfo(0).normalizedTime)
            //{
                //camAnim.enabled = false;
            //    Debug.Log("Animator false");
            mainCamera.transform.parent.position = new Vector3(mainCamPos.x + 1.2f, mainCamPos.y +  0.3f, mainCamPos.z + 4.3f);
            mainCamera.transform.parent.eulerAngles =  new Vector3 (mainCamRot.x + -2.5f, mainCamRot.y + -9.8f, mainCamRot.z + 7.9f);
            //}
            //float i = 0.0f;
            //float waitTime = 100.0f;
            //for (float i = 0.0f;  i <= waitTime; i += Time.deltaTime)
            //{
            //    //i += Time.deltaTime;
            //    Debug.Log(i);

            //    if (i >= waitTime)
            //    {
            //        camAnim.enabled = false;
            //        Debug.Log("i = 65");
            //        mainCamera.transform.position = new Vector3(1.197856f, 0.3216273f, 4.293736f);
            //        mainCamera.transform.rotation = new Quaternion(-2.504f, -9.822f, 7.873f, 0);
            //    }
            //}
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            //Animator camAnim = mainCamera.GetComponent<Animator>();

            //camAnim.enabled = true;
            //camAnim.SetBool("CameraSwing", false);
            ////camAnim.enabled = false;
            mainCamera.transform.parent.position = mainCamPos;
            mainCamera.transform.parent.eulerAngles = mainCamRot;
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
        Ray ray1 = c_Cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, whatIsPlacedObjectLayer))
        {
            return hitInfo.transform.GetComponent<PlacedObject>();
        }
        else if (Physics.Raycast(ray1, out RaycastHit hitInfo1, 100f, whatIsPlacedObjectLayer))
        {
            return hitInfo1.transform.GetComponent<PlacedObject>();
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

    public void ShowFloatingText(PlacedObject placedObject, Vector3 objectPos, bool canvas)
    {
        float cameraAnglex = mainCamera.GetComponent<Transform>().rotation.x;
        float cameraAngley = mainCamera.GetComponent<Transform>().rotation.y;
        float cameraAnglez = mainCamera.GetComponent<Transform>().rotation.z;

        if (canvas == true)
        {
            var myNewStats = Instantiate(FloatingTextPrefab, new Vector3(objectPos.x, objectPos.y - 0.1f, objectPos.z - 0.5f), Quaternion.Euler(0, 0, 0), transform);
            myNewStats.transform.parent = placedObject.transform;
            myNewStats.transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = placedObject.attack.ToString();
            myNewStats.transform.GetChild(1).GetComponentInChildren<TMP_Text>().text = placedObject.health.ToString();
        }
        else
        {
            var myNewStats = Instantiate(FloatingTextPrefab, new Vector3(objectPos.x + 0.5f, objectPos.y - 0.2f, objectPos.z - 0.5f), Quaternion.Euler(40, -50, 0), transform);
            myNewStats.transform.parent = placedObject.transform;
            myNewStats.transform.GetChild(0).GetComponentInChildren<TMP_Text>().text = placedObject.attack.ToString();
            myNewStats.transform.GetChild(1).GetComponentInChildren<TMP_Text>().text = placedObject.health.ToString();
        }
    }

    public void UpdateFloatingText(PlacedObject placedObject)
    {
        placedObject.transform.GetChild(0).GetChild(0).GetComponentInChildren<TMP_Text>().text = placedObject.attack.ToString();
        //Debug.Log(placedObject.attack);
        placedObject.transform.GetChild(0).GetChild(1).GetComponentInChildren<TMP_Text>().text = placedObject.health.ToString();
        //Debug.Log(placedObject.health);

        // Update level
        levelUpdate.UpdateLevel(placedObject);
    }

    public void StartBattlePhase()
    {
        // Updates Canvas
        UpdateCanvas(1);

        // Sets battle variable to true
        battleOn = true;

        // Creates Enemy Grid
        gameGridEnemyS.CreateEnemyGrid(0, 1);
        gameGridEnemyS.CreateEnemyGrid(0, 2);
        gameGridEnemyS.CreateEnemyGrid(0, 3);

        // Creates extended enemy Grid
        if (roundCounter >= 3)
        {
            gameGridEnemyS.CreateEnemyGrid(2, 1);
            gameGridEnemyS.CreateEnemyGrid(2, 2);
            gameGridEnemyS.CreateEnemyGrid(2, 3);
        }

        enemySets.EnemySet();
    }

    // Gets grid cell
    public GameObject GetGridCell(int pos)
    {
        return gameGrid.transform.GetChild(pos).gameObject;
    }

    public PlacedObject GetCellObject(int pos)
    {
        return GetGridCell(pos).GetComponent<GridCell>().GetPlacedObject();
    }       // Gets placed object in grid cell

    public GameObject GetEnemyCell(int pos)
    {
        if (gameGridEnemyS.transform.childCount > 0)
        {
            return gameGridEnemyS.transform.GetChild(pos).gameObject;
        }
        else
        {
            return null;
        }
    }          // Gets enemy grid cell

    public PlacedObject GetEnemyObject(int pos)
    {
        return GetEnemyCell(pos).GetComponent<GridCell>().GetPlacedObject();
    }      // Gets placed object in enemy grid cell

    public void UpdateCanvas(int scene)
    {
        if (scene == 1)
        {
            mainCamera.GetComponent<Animator>().SetBool("CameraSwing", true);
            // Sets Shop in Canvas inactive
            canvas.transform.GetChild(0).gameObject.SetActive(false);
            // Sets battle phase button inactive
            canvas.transform.GetChild(1).gameObject.SetActive(false);
            // Sets battle phase button inactive
            canvas.transform.GetChild(2).gameObject.SetActive(false);
            // Sets start battle button active
            canvas.transform.GetChild(3).gameObject.SetActive(true);
        }

        if (scene == 2)
        {
            mainCamera.GetComponent<Animator>().SetBool("CameraSwing", false);
            // Sets Shop in Canvas active
            canvas.transform.GetChild(0).gameObject.SetActive(true);
            // Sets battle phase button active
            canvas.transform.GetChild(1).gameObject.SetActive(true);
            // Sets battle phase button active
            canvas.transform.GetChild(2).gameObject.SetActive(true);
            // Sets start battle button inactive
            canvas.transform.GetChild(3).gameObject.SetActive(false);

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
    }              // Sets Canvas to Battle Canvas

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
                ShowFloatingText(placedObject, placementVec, false);
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
        placementVec = new Vector3(cellMouseIsOver.GetComponent<Transform>().position.x, 0f, cellMouseIsOver.GetComponent<Transform>().position.z);
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
        placedObject.transform.GetChild(0).GetChild(2).position = new Vector3 (placedObject.transform.GetChild(0).GetChild(2).position.x, 1.5f, placedObject.transform.GetChild(0).GetChild(2).position.z);
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
            if ((roundCounter >= 3) && (gameGrid.created2Grid == false))
            {
                //Debug.Log("UPDATEHIERARCHY");
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

        // Test placedobjects to positions
        if ((gameGrid.transform.childCount > 2) && (gameGrid.transform.GetChild(1).gameObject.activeInHierarchy == true) && (gameGrid.transform.GetChild(2).gameObject.activeInHierarchy == true))
        {
            pOPosition1 = GetCellObject(0);
            pOPosition2 = GetCellObject(1);
            pOPosition3 = GetCellObject(2);
            if (gameGrid.transform.childCount >= 4)
            {
                pOPosition4 = GetCellObject(3);
                pOPosition5 = GetCellObject(4);
                pOPosition6 = GetCellObject(5);
            }
        }
    }

    public bool InPauseMenuOrNot()
    {
        GameObject canvas_PauseMenu = GameObject.Find("Canvas_PauseMenu");
        if (canvas_PauseMenu.transform.GetChild(0).gameObject.activeInHierarchy == false)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool InHelpMenuOrNot()
    {
        GameObject canvas_HelpMenu = GameObject.Find("Canvas_HelpMenu");
        if (canvas_HelpMenu.transform.GetChild(0).gameObject.activeInHierarchy == false)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void CameraShake()
    {
        StartCoroutine(mainCamera.GetComponent<CameraShake>().Shake(.15f, .2f));
        //Debug.Log("SHAKE");
    }

    public void ShakeGold()
    {
        StartCoroutine(goldO.GetComponent<Shake>().ShakeObject(.15f, .2f));
    }   // einfügen bei cant buy unit
}

