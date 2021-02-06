using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    public Transform cube;
    public Transform sphere;


    // Update is called once per frame
    void Update()
    {
        cube.position= Vector3.Lerp(cube.position, sphere.position,0.01f);
    }
}
