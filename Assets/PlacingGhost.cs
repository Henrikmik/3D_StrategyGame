using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingGhost : MonoBehaviour
{
    private Transform visual;
    private Unit unit;

    private void Start()
    {
        RefreshVisual();

        //GridBuildingSystem3D.Instance.OnSelectChanged += Instance_OnSelectChanged;
    }

    private void Instance_OnSelectChanged(object sender, System.EventArgs e)
    {
        RefreshVisual();
    }

    private void LateUpdate()
    {
    //    Vector3 targetPosition = GridBuildingSystem3D.Instance.GetMouseWorldSnappedPosition();
    //    targetPosition.y = 1f;
    //    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 15);

    //    transform.rotation = Quaternion.Lerp(transform.rotation, GridBuildingSystem3D.Instance.GetPlacedObjectRotation(), Time.deltaTime * 15f);
    }

    private void RefreshVisual()
    {
        if (visual != null)
        {
            Destroy(visual.gameObject);
            visual = null;
        }

        //Unit unit = GridBuildingSystem3D.Instance.GetPlacedObjectTypeSO();

        //if (unit != null)
        //{
        //    visual = Instantiate(unit.visual, Vector3.zero, Quaternion.identity);
        //    visual.parent = transform;
        //    visual.localPosition = Vector3.zero;
        //    visual.localEulerAngles = Vector3.zero;
        //}
    }
}
