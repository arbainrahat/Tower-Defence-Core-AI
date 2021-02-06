using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 
 * Script for Separate cores
 * From Creeps or Enemy
 * 
 */
public class CoreSeparate : MonoBehaviour
{
    //creeps Array Get All enemy creeps
    public  GameObject []creeps;
    //checkCreepTakeCore Array Get All Creeps of creeps Array for DeAttach Cores
    public GameObject[] checkCreepTakeCore;

    //prevCount store lenght of creeps Array
    //used to find DeActive Crreps based on remove element from creeps Array
    public int prevCount=0;
    

    private void Update()
    {
        //Find Creeps with tag "Enemy"
        //and Store in creeps Array
       creeps = GameObject.FindGameObjectsWithTag("Enemy");

        //If crreps Length greater or equal to prevCount
        //mean creeps not killed
        if (creeps.Length >= prevCount)
        {
            //copy creeps Array
            checkCreepTakeCore = creeps;
        }
        //If crreps Length less than to prevCount
        //mean creeps killed
        if (creeps.Length<prevCount)
        {
            for (int i = 0; i < checkCreepTakeCore.Length; i++)
            {
                //Find Killed Creep
                if (checkCreepTakeCore[i].activeSelf == false)
                {
                    //Find Core & Detach Core
                    GameObject core = FindChildWithTag(checkCreepTakeCore[i], "Core");
                    //Unparent the child core game object
                    if (core != null)
                    {
                        core.transform.parent = null;
                    }
                }
            }
            //Update prevCount Data Field
            prevCount = creeps.Length;
            //copy creeps Array
            checkCreepTakeCore = creeps;
        }
        else
        {
            prevCount = creeps.Length;
        }
        
    }

    //Method find the child core game object with tag name
    private GameObject FindChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == tag)
            {
                return t.GetChild(i).gameObject;
            }
        }
        return null;
    }

    //If parent GameObject Destroy than unparent Child Core
    // private void OnDestroy()
    // {
    //     GameObject core = FindChildWithTag(gameObject, "Core");
    //     //Unparent the child core game object
    //     core.transform.parent = null;
    // }
}
