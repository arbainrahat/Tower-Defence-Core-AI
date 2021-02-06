using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorePathFollow : MonoBehaviour
{
    [SerializeField] GameObject []ActiveCorePath;
   [SerializeField] float minDistancPoint=-1f;
    [SerializeField]  GameObject shortPath;
    [SerializeField] GameObject shortWaypoint;

    [SerializeField] int changeWaypoint=0;
   public Vector3 StartPos;
    public Transform parentObject;

    Vector3 offSet;    //For maintain core height
    bool turnOffFindPath = false;
    public bool control=true;

 public   bool parentCore = false;    //for check that core attach to enemy/creep
   public bool startCheck = false;

    private void Start()
    {
        StartPos = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            startCheck = false;
            parentCore = false;
            control = true;
            transform.position = StartPos;
            transform.parent = parentObject;
        }
    }



    void Update()
    {
        

        if (turnOffFindPath == false)
        {
           ActiveCorePath = GameObject.FindGameObjectsWithTag("CorePath");
        }


        if (control == true)
        {
                if (ActiveCorePath.Length > 0)
                {

                    for (int i = 0; i < ActiveCorePath.Length; i++)
                    {
                        Transform parentNode = ActiveCorePath[i].transform;
                        for (int j = 0; j < parentNode.childCount; j++)
                        {
                            float dist = 0;
                            dist = Vector3.Distance(transform.position, parentNode.GetChild(j).position);

                            //Find shortest distance waypoint node
                            if (dist <= minDistancPoint)
                            {
                                minDistancPoint = dist;
                                //Get shortest distance waypoint node
                                shortWaypoint = parentNode.GetChild(j).gameObject;

                            }
                            //First time initialze value
                            if (minDistancPoint == -1f)
                            {
                                minDistancPoint = dist;
                            }

                        }

                    }
                    //Get Short Path Parent Node
                    shortPath = shortWaypoint.transform.parent.gameObject;
                    control = false;
                    turnOffFindPath = true;
                }
        }
       // else
       // {
       //     
       //
       //          Transform parentNode = shortPath.transform;
       //         minDistancPoint = -1f;
       //         for (int i = 0; i < parentNode.childCount; i++)
       //         {
       //             print(parentNode.GetChild(i));
       //             float dist = 0;
       //             dist = Vector3.Distance(transform.position, parentNode.GetChild(i).position);
       //
       //             //Find shortest distance waypoint node
       //             if (dist <= minDistancPoint)
       //             {
       //                 minDistancPoint = dist;
       //                 //Get shortest distance waypoint node
       //                 shortWaypoint = parentNode.GetChild(i).gameObject;
       //
       //             }
       //             //First time initialze value
       //             if (minDistancPoint == -1f)
       //             {
       //                 minDistancPoint = dist;
       //             }
       //
       //
       //         }
       //         //Get Short Path Parent Node
       //         shortPath = shortWaypoint.transform.parent.gameObject;
       //     
       // }
            //Change path when current path is DeActive
            if (shortPath.activeSelf == false)
            {
                minDistancPoint = -1;
                control = true;
            turnOffFindPath = false;
        }



            //when core attact to creep
       if (transform.parent != null && parentCore == false)
       {
           parentCore = true;
       }
      //After creep die than core follow path
        if (parentCore == true && transform.parent == null)
        {
           
            startCheck = true;
        }


        if (startCheck == true)
        {
            
            //Core first move towards waypoint
            if (control == false && changeWaypoint == 0)
            {
                offSet = new Vector3(shortWaypoint.transform.position.x, transform.position.y, shortWaypoint.transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, offSet, Time.deltaTime * 2);
                if (transform.position == offSet)
                {
                    changeWaypoint = shortWaypoint.transform.GetSiblingIndex() - 1;
                }
            }
            //core move countinue to next waypoints
            if (changeWaypoint > 0)
            {
                offSet = new Vector3(shortPath.transform.GetChild(changeWaypoint).position.x, transform.position.y, shortPath.transform.GetChild(changeWaypoint).position.z);
                transform.position = Vector3.MoveTowards(transform.position, offSet, Time.deltaTime * 2);
                if (transform.position == offSet)
                {
                    changeWaypoint--;
                }
            }
            


        }
    }

   // //Find near waypoint from active path
   // void NearWayPoint()
   // {
   //
   //     for (int i = 0; i < shortPath.transform.childCount; i++)
   //     {
   //         
   //             float dist = 0;
   //             dist = Vector3.Distance(transform.position, shortPath.transform.GetChild(i).position);
   //
   //             //Find shortest distance waypoint node
   //             if (dist <= minDistancPoint)
   //             {
   //                 minDistancPoint = dist;
   //                 //Get shortest distance waypoint node
   //                 shortWaypoint = shortPath.transform.GetChild(i).gameObject;
   //
   //             }
   //             //First time initialze value
   //             if (minDistancPoint == -1f)
   //             {
   //                 minDistancPoint = dist;
   //             }
   //
   //         
   //     }
   // }
}
