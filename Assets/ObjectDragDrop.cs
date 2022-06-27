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
    Camera c_cam;
    GameObject gameGrid;

    private Vector3 origin;
    private GridCell oldGridCell;
    void Start()
    {
        if (Camera.main.GetComponent<PhysicsRaycaster>() == null)
        {
            Debug.Log("Camera does not have a physics raycaster.");
        }

        m_cam = Camera.main;
        c_cam = GameObject.Find("Canvas_Camera").GetComponent<Camera>();
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        gameGrid = GameObject.Find("GameGrid");

        origin = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (inputManager.battleOn != true)
        {
            Ray R = m_cam.ScreenPointToRay(Input.mousePosition);

            Vector3 P = R.origin + R.direction * 8f;

            transform.position = P;

            //Debug.Log("OnDrag");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (inputManager.battleOn != true)
        {
            //oldGridCell = inputManager.IsMouseOverAGridSpace();
            oldGridCell = transform.GetComponent<PlacedObject>().AssignedGridCell();
            // Do stuff when dragging begins.
            //Debug.Log("OnBeginDrag");
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (inputManager.battleOn != true)
        {
            // Do stuff when draggin ends.
            //Debug.Log("OnEndDrag");

            GridCell cellMouseIsOver = inputManager.IsMouseOverAGridSpace();
            PlacedObject hoveredObject = inputManager.IsMouseOverAplacedobject();
            PlacedObject placedObject = transform.GetComponent<PlacedObject>();

            if (cellMouseIsOver != null)    // mouse is over a grid cell
            {
                if (cellMouseIsOver.CanBuild()) // grid cell is empty
                {
                    if (oldGridCell != null)    // was already on the field
                    {
                        inputManager.DragOnGridCell(cellMouseIsOver, placedObject);
                        PlayPlaceSound();
                        origin = transform.position;
                        oldGridCell.UnstoreObject(placedObject);
                        oldGridCell.ClearTransform();
                        oldGridCell = cellMouseIsOver;
                        //Debug.Log("Vom Feld");

                    }

                    else    // objects origin is in the shop
                    {
                        if (inputManager.gold >= 30)
                        {
                            inputManager.DragOnGridCell(cellMouseIsOver, placedObject);
                            PlayPlaceSound();
                            origin = transform.position;
                            oldGridCell = cellMouseIsOver;

                            Transform infoStats = placedObject.transform.GetChild(0);
                            infoStats.position = new Vector3 (infoStats.position.x + 0.9f, infoStats.position.y + 0.25f, infoStats.position.z + 0.1f);
                            inputManager.gold -= 30;
                            //Debug.Log("Aus Shop");
                        }
                        else
                        {
                            FindObjectOfType<AudioManager>().Play("Error");
                            inputManager.ShakeGold();
                            ResetPosition();
                        }
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
                            PlayPlaceSound();
                            origin = transform.position;

                            // switch other object
                            inputManager.DragOnGridCell(oldGridCell, swappedPlacedObject);
                            swappedPlacedObject.GetComponent<ObjectDragDrop>().origin = swappedPlacedObject.transform.position;

                            oldGridCell = cellMouseIsOver;
                        }
                    }
                    else if ((oldGridCell == null) && (cellMouseIsOver.objectInThisGridSpace == null))  // object from shop and grid cell empty
                    {
                        if (inputManager.gold >= 30)
                        {
                            inputManager.DragOnGridCell(cellMouseIsOver, placedObject);
                            PlayPlaceSound();
                            origin = transform.position;
                            oldGridCell = cellMouseIsOver;
                            inputManager.gold -= 30;
                        }
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
                                    if (inputManager.gold >= 30)
                                    {
                                        swappedPlacedObject.level += placedObject.level;
                                        swappedPlacedObject.LevelupStats();
                                        inputManager.UpdateFloatingText(swappedPlacedObject);
                                        inputManager.gold -= 30;
                                        placedObject.DestroySelf();
                                    }
                                    else
                                    {
                                        FindObjectOfType<AudioManager>().Play("Error");
                                        inputManager.ShakeGold();
                                        ResetPosition();
                                    }
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
    }

    public void ResetPosition()
    {
        transform.position = origin;
        //Debug.Log("Cannot build atm! ");
    }
    public void PlayPlaceSound()
    {
        FindObjectOfType<AudioManager>().Play("MoveUnit");
    }
}
