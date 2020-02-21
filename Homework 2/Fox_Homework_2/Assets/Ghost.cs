using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    //float speed = 1f;
    //float delta = 5.29f;  //delta is the difference between min y to max y.
    [SerializeField] float speed = 5.0f;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //float y = Mathf.PingPong(speed * Time.time, delta);
        //Vector3 pos = new Vector3(transform.position.x, y, transform.position.z);
        //transform.position = pos;
        rb.velocity = new Vector2(0f, speed);
    }

    // Flip direction on colliding with another object
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Make sure Ghost flies through coins or smileys
        if (other.tag != "Coin" && other.tag != "Smiley")
        {
            speed = -speed;
        }
    }
}
