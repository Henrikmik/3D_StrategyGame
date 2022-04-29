using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Collider))]
public class ObjectDragDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Camera m_cam;
    private Vector3 origin;
    void Start()
    {
        if (Camera.main.GetComponent<PhysicsRaycaster>() == null)
        {
            Debug.Log("Camera does not have a physics raycaster.");
        }

        m_cam = Camera.main;
        origin = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Ray R = m_cam.ScreenPointToRay(Input.mousePosition);    // Get the ray from mouse position
        Vector3 PO = transform.position;  // Take current position of this draggable object as Plane큦 Origin
        Vector3 PN = m_cam.transform.forward;   // Take current negative camera큦 forward as Plane큦 Normal
        float t = Vector3.Dot(PO - R.origin, PN) / Vector3.Dot(R.direction, PN);    // plane vs. line intersection in algebric form. It finds t as distance from the camera of the new point in the ray큦 direction.
        Vector3 P = R.origin + R.direction * t; // Find the new point;

        transform.position = P;
        Debug.Log("OnDrag");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Do stuff when dragging begins.
        //Debug.Log("OnBeginDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = origin;
        // Do stuff when draggin ends.
        Debug.Log("OnEndDrag");
    }
}
