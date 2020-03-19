using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float gunDamage = 10f;
    public float gunRange = 50f;

    public Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    // Use raycasting to fire a bullet
    void Fire()
    {
        RaycastHit hitInfo;

        // If raycast hits something, then put info into hitInfo and proceed
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, gunRange))
        {
            Debug.Log(hitInfo.transform.name);
        }

    }
}
