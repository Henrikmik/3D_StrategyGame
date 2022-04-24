using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGridEnemy : MonoBehaviour
{
    private int height = 2;
    private int width = 2;
    private float gridSpaceSize = 1.3f;
    private int gridOffset = 2;

    [SerializeField] private GameObject gridCellPrefab;
    private GameObject gameGridEnemy;

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

    public void CreateEnemyGrid(int x, int y)
    {
        gameGridEnemy = new GameObject();

        if (gridCellPrefab == null)
        {
            Debug.LogError("ERROR: Grid Cell Prefab on the Game grid is not assigned");
            //yield return null;
        }

        gameGridEnemy = Instantiate(gridCellPrefab, new Vector3(x * gridSpaceSize, 0, (y + gridOffset) * gridSpaceSize), Quaternion.identity); //ändern auf 3
        gameGridEnemy.GetComponent<GridCell>().SetPosistion(x, y);
        gameGridEnemy.transform.parent = transform;
        gameGridEnemy.gameObject.name = "Grid Space (X: " + x.ToString() + " , Y: " + y.ToString() + ")";

        //yield return new WaitForSeconds(.1f);
    }
}
