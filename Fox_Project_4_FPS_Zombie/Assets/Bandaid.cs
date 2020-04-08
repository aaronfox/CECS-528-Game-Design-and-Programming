using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandaid : MonoBehaviour
{
    public float angleToRotate = 1.0f;
    // Simply rotates bandaid to make it more visually attractive
    void Update()
    {
        transform.Rotate(Vector3.right, angleToRotate);
    }
}
