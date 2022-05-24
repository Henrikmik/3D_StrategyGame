using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Collider))]
public class ObjectDragDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    Camera m_cam;
    InputManager inputManager;

    private Vector3 origin;
    private GridCell oldGridCell;
    void Start()
    {
        if (Camera.main.GetComponent<PhysicsRaycaster>() == null)
        {
            Debug.Log("Camera does not have a physics raycaster.");
        }

        m_cam = Camera.main;
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();

        origin = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Ray R = m_cam.ScreenPointToRay(Input.mousePosition);    // Get the ray from mouse position
        Vector3 PO = transform.position;  // Take current position of this draggable object as Plane�s Origin
        Vector3 PN = m_cam.transform.forward;   // Take current negative camera�s forward as Plane�s Normal
        float t = Vector3.Dot(PO - R.origin, PN) / Vector3.Dot(R.direction, PN);    // plane vs. line intersection in algebric form. It finds t as distance from the camera of the new point in the ray�s direction.
        Vector3 P = R.origin + R.direction * t; // Find the new point;

        transform.position = P;
        //Debug.Log("OnDrag");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        oldGridCell = inputManager.IsMouseOverAGridSpace();
        // Do stuff when dragging begins.
        //Debug.Log("OnBeginDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Do stuff when draggin ends.
        //Debug.Log("OnEndDrag");
        
        GridCell cellMouseIsOver = inputManager.IsMouseOverAGridSpace();
        PlacedObject placedObject = transform.GetComponent<PlacedObject>();

        if (cellMouseIsOver != null)    // mouse is over a grid cell
        {
            if (cellMouseIsOver.CanBuild()) // grid cell is empty
            {
                if (oldGridCell != null)    // was already on the field
                {
                    inputManager.DragOnGridCell(cellMouseIsOver, placedObject);
                    origin = transform.position;
                    oldGridCell.UnstoreObject(placedObject);
                    oldGridCell.ClearTransform();

                    oldGridCell = cellMouseIsOver;
                    //Debug.Log("Vom Feld");

                }

                else    // objects origin is in the shop
                {
                    inputManager.DragOnGridCell(cellMouseIsOver, placedObject);
                    origin = transform.position;
                    oldGridCell = cellMouseIsOver;
                    //Debug.Log("Aus Shop");
                }
            }

            else if (cellMouseIsOver.CanBuild() == false)   // grid cell is occupied
            {
                
                if (oldGridCell != null)    // object was already on the field
                {
                    PlacedObject swappedPlacedObject = cellMouseIsOver.objectInThisGridSpace;


                    if ((swappedPlacedObject.nameA == placedObject.nameA) && (swappedPlacedObject != placedObject)) // same type but not the same object
                    {
                        if (eventData.button == PointerEventData.InputButton.Right)
                        {
                            if (swappedPlacedObject.level < 7)  // object level is lower than 7
                            {
                                swappedPlacedObject.level += placedObject.level;
                                swappedPlacedObject.LevelupStats();
                                inputManager.UpdateFloatingText(swappedPlacedObject);

                                placedObject.DestroySelf();
                            }
                            else    // object level is higher than 6
                            {
                                ResetPosition();
                            }
                        }
                        if (eventData.button == PointerEventData.InputButton.Left)
                        {
                            // place selected object
                            inputManager.DragOnGridCell(cellMouseIsOver, placedObject);
                            origin = transform.position;
                             
                            // switch other object
                            inputManager.DragOnGridCell(oldGridCell, swappedPlacedObject);
                            swappedPlacedObject.GetComponent<ObjectDragDrop>().origin = swappedPlacedObject.transform.position;

                            oldGridCell = cellMouseIsOver;
                        }
                    }
                    else    // not the same type -> switch positions
                    {
                        // place selected object
                        inputManager.DragOnGridCell(cellMouseIsOver, placedObject);
                        origin = transform.position;

                        // switch other object
                        inputManager.DragOnGridCell(oldGridCell, swappedPlacedObject);
                        swappedPlacedObject.GetComponent<ObjectDragDrop>().origin = swappedPlacedObject.transform.position;

                        oldGridCell = cellMouseIsOver;
                    }
                }
                else if ((oldGridCell == null) && (cellMouseIsOver.objectInThisGridSpace == null))  // object from shop and grid cell empty
                {
                    inputManager.DragOnGridCell(cellMouseIsOver, placedObject);
                    origin = transform.position;
                    oldGridCell = cellMouseIsOver;
                }
                else    // object from shop and grid cell occupied
                {
                    PlacedObject swappedPlacedObject = cellMouseIsOver.objectInThisGridSpace;   // unit on targeted grid

                    if ((oldGridCell == null) && (cellMouseIsOver.objectInThisGridSpace.nameA == placedObject.nameA))   // objects have the same type
                    {
                        if (eventData.button == PointerEventData.InputButton.Right)
                        {
                            if (swappedPlacedObject.level < 7)  // object level is lower than 7
                            {
                                swappedPlacedObject.level += placedObject.level;
                                swappedPlacedObject.LevelupStats();
                                inputManager.UpdateFloatingText(swappedPlacedObject);

                                placedObject.DestroySelf();
                            }
                            else    // object level is higher than 6
                            {
                                ResetPosition();
                            }
                        }
                        if (eventData.button == PointerEventData.InputButton.Left)
                        {
                            ResetPosition();
                        }
                    }
                    else
                    {
                        ResetPosition();
                    }
                }
            }
            else
            {
                ResetPosition();
            }
        }
        else
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        transform.position = origin;
        //Debug.Log("Cannot build atm! ");
    }
}
