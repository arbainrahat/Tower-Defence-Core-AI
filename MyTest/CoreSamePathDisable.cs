using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreSamePathDisable : MonoBehaviour
{
    //Get paths of core that will affect with Tower
    public GameObject[] CoreFollowPath;
    //get game object that also control same core path
    public GameObject sameCompHave;

    private void Update()
    {

        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.up));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            
            if (hit.transform.tag == "Tower")
            {
                PathDisable();
                if (sameCompHave != null)
                {
                    sameCompHave.GetComponent<CoreSamePathDisable>().enabled = false;
                }
            }
            else
            {
                PathEnable();
                if (sameCompHave != null)
                {
                    sameCompHave.GetComponent<CoreSamePathDisable>().enabled = true;
                }
                
            }
        }
       
    }

    //Function for Active or Deactive Core Paths
    void PathEnable()
    {
        for (int i = 0; i < CoreFollowPath.Length; i++)
        {
            CoreFollowPath[i].SetActive(true);
        }
    }
    void PathDisable()
    {
        for (int i = 0; i < CoreFollowPath.Length; i++)
        {
            CoreFollowPath[i].SetActive(false);
        }
    }
}
