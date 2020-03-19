﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float gunDamage = 10f;
    public float gunRange = 50f;
    public float impactForce = 300f;

    public Camera playerCamera;
    public ParticleSystem flash;
    public ParticleSystem hitObjectEffect;

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
        // Play flashing particle effect to make it look like bullet is firing
        flash.Play();

        RaycastHit hitInfo;

        // If raycast hits something, then put info into hitInfo and proceed
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hitInfo, gunRange))
        {
            Debug.Log(hitInfo.transform.name);
            // Find enemy component in parents
            Enemy enemy = hitInfo.transform.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(gunDamage);
            }

            //// Push back on enemy if they have a rigidbody
            //if(hitInfo.rigidbody)
            //{
            //    print("adding force");
            //    hitInfo.rigidbody.AddForce(-hitInfo.normal * impactForce);
            //}

            GameObject hitObjectEffectGameObject = Instantiate(hitObjectEffect.gameObject, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            Destroy(hitObjectEffectGameObject, 2f);
        }

    }
}