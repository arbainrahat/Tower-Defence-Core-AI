using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PathBuilder : MonoBehaviour
{
    //For automatic rename WayPoints
    int index;

    //Store WayPoints in list
    public List<Transform> waypoints = new List<Transform>();
    
    void Update()
    {
        if (!Application.isPlaying)     //Not Add WayPoints during PlayMode
        {
            //tem will get the child waypoints
            Transform[] tem = GetComponentsInChildren<Transform>();
            if (tem.Length > 0)
            {
                waypoints.Clear();

                index = 0;

                foreach (Transform t in tem)
                {
                    if (t != transform) //If condition will not rename parent GameObject
                    {
                        t.name = "WayPoint_" + index.ToString();
                        waypoints.Add(t);
                        index++;
                    }
                }
            }
        }
    }

    //Draw Gizmos Sphere of waypoints and Draw Line b/w waypoints
    private void OnDrawGizmos()
    {
        if (waypoints.Count > 0)
        {
            Gizmos.color = Color.green;
            foreach(Transform t in waypoints)
            {
                Gizmos.DrawSphere(t.position, 0.3f);
            }

            Gizmos.color = Color.red;
            for(int a=0; a < waypoints.Count - 1; a++)
            {
                Gizmos.DrawLine(waypoints[a].position, waypoints[a + 1].position);
            }
        }
    }
}
