using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thwomp : MonoBehaviour
{
    [SerializeField] Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Fall if player is coming
        if (transform.position.x - 2.5f <= player.position.x)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        }
    }
}
