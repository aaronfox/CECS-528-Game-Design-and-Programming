using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseSpiral : MonoBehaviour
{
    private Vector3 origin = new Vector3(0.0f, 0.0f, 0.0f);

    // Update is called once per frame
    void Update()
    {
        // Spin the object around the world origin
        transform.RotateAround(origin, new Vector3(0, 1, 0), -100 * Time.deltaTime);
    }
}
