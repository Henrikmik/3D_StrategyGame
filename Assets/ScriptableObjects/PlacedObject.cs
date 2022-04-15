using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, Unit.Dir dir, Unit unit)
    {
        Transform placedObjectTransform = Instantiate(unit.prefab, worldPosition, Quaternion.Euler(0, unit.GetRotationAngle(dir), 0));

        PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();

        placedObject.unit = unit;
        placedObject.origin = origin;
        placedObject.dir = dir;

        return placedObject;
    }

    private Unit unit;
    private Vector2Int origin;
    private Unit.Dir dir;

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
