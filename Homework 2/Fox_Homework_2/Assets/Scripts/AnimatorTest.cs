using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTest : MonoBehaviour
{
    public float xSpeedInc = 0.01f;
    public float maxSpeed = 3.5f;
    float speed;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0;
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        // slow down
        if (Input.GetKey(KeyCode.A))
        {
            speed -= xSpeedInc;
            if (speed < 0) speed = 0;
        }
        // Speed up
        if (Input.GetKey(KeyCode.D))
        {
            speed += xSpeedInc;
            if (speed > maxSpeed) speed = maxSpeed;
        }
        transform.Translate(Time.deltaTime * speed, 0, 0);
        animator.SetFloat("Speed", speed);
    }
}
