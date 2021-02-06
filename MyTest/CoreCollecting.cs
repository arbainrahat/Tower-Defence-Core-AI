using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreCollecting : MonoBehaviour
{
    //core GameObject Array store cores
    public GameObject[] core;
    //counter int for iterate cores
    public int counter = 0;
    //coreCatchPosition get core attach position child gameObject of Creep/Enemy
    private Transform coreCatchPosition;
    //isLerp used for control CoreSetToEnemy method
    public bool isLerp=false;

    public Transform ParentObject;

    private void Update()
    {
        if (isLerp)
        {
            CoreSetToEnemy();
        }

        if(ParentObject.childCount == 0)
        {
            counter = 0;
        }
    }
    //CoreSetToEnemy method set the core position to Enemy Position
    private void CoreSetToEnemy()
    {
        core[counter].transform.position= Vector3.Lerp(core[counter].transform.position, coreCatchPosition.position, 0.5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //counter check number of cores limits
            if (counter < core.Length) {
                //set parent of core, parent is creep/enemy
                core[counter].transform.parent = other.transform;
                //get core attach position of Object
                coreCatchPosition =other.transform.Find("Catch");
                isLerp = true;
                //Invoke("delay", 1.3f);  //Now addition in counter, is OnTriggerExit
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
          isLerp = false;
            counter++;
        }
    }
    //delay method increment counter value
    void delay()
    {
        counter++;
    }
}
