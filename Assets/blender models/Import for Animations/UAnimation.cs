using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UAnimation : MonoBehaviour
{
    InputManager inputManager;

    private void Start()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
    }

    public IEnumerator AnimationMoveForward(PlacedObject placedObject)
    {
        Animator anim = placedObject.transform.GetChild(1).GetChild(0).GetComponent<Animator>();
        anim.enabled = true;
        anim.Play("MoveForward");
        Debug.Log("MoveForward");

        yield return new WaitForSeconds(1f);

        //if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        //{
        //    anim.enabled = false;
        //    Debug.Log("Not playing");
        //}
        //else
        //{
        //    Debug.Log("Playing");
        //}
        

        //PlacedObject placedObject = inputManager.GetCellObject(0);
        placedObject.transform.GetChild(1).eulerAngles = new Vector3(0f, 0f, 0f);
        if (placedObject.nameA == "Lemon")
        {
            placedObject.transform.GetChild(1).eulerAngles = new Vector3(0f, -180f, 0f);
            Debug.Log("LEMON");
        }
    }

    public IEnumerator AnimationAttack(int lane)
    {
        if (lane == 1)
        {
            // unit
            Animator anim = inputManager.GetCellObject(0).transform.GetChild(1).GetChild(0).GetComponent<Animator>();
            //Animator anim = inputManager.GetCellObject(0).transform.GetChild(1).GetComponent<Animator>();
            anim.enabled = true;
            anim.Play("DealDamage");
            //Debug.Log("DealDamage");

            // enemy
            Animator animE = inputManager.GetEnemyObject(0).transform.GetChild(1).GetChild(0).GetComponent<Animator>();
            //Debug.Log(animE);
            animE.enabled = true;
            animE.Play("DealDamage");
            //Debug.Log("DealDamageENEMY");

            // wait time to reset rotation
            yield return new WaitForSeconds(1.2f);

            PlacedObject placedObject = inputManager.GetCellObject(0);
            placedObject.transform.GetChild(1).eulerAngles = new Vector3(0f, 0f, 0f);
            if (placedObject.nameA == "Lemon")
            {
                placedObject.transform.GetChild(1).eulerAngles = new Vector3(0f, -180f, 0f);
                //Debug.Log("LEMON");
            }
            //animE.enabled = false;

            PlacedObject placedObjectE = inputManager.GetCellObject(0);
            placedObjectE.transform.GetChild(1).eulerAngles = new Vector3(0f, 0f, 0f);
        }
        if (lane == 2)
        {
            // unit
            Animator anim = inputManager.GetCellObject(3).transform.GetChild(1).GetChild(0).GetComponent<Animator>();
            //Animator anim = inputManager.GetCellObject(0).transform.GetChild(1).GetComponent<Animator>();
            anim.enabled = true;
            anim.Play("DealDamage");
            //Debug.Log("DealDamage");

            // enemy
            Animator animE = inputManager.GetEnemyObject(3).transform.GetChild(1).GetChild(0).GetComponent<Animator>();
            //Debug.Log(animE);
            animE.enabled = true;
            animE.Play("DealDamage");
            //Debug.Log("DealDamageENEMY");

            // wait time to reset rotation
            yield return new WaitForSeconds(1.2f);

            PlacedObject placedObject = inputManager.GetCellObject(3);
            placedObject.transform.GetChild(1).eulerAngles = new Vector3(0f, 0f, 0f);
            if (placedObject.nameA == "Lemon")
            {
                placedObject.transform.GetChild(1).eulerAngles = new Vector3(0f, -180f, 0f);
                //Debug.Log("LEMON");
            }
            //animE.enabled = false;

            PlacedObject placedObjectE = inputManager.GetCellObject(3);
            placedObjectE.transform.GetChild(1).eulerAngles = new Vector3(0f, 0f, 0f);
        }

    }
    public void Attackunit()
    {
        Animator anim = inputManager.GetCellObject(0).transform.GetChild(1).GetComponent<Animator>();
        anim.enabled = true;
        anim.Play("DealDamage");
        Debug.Log("DealDamage");
    }
}
