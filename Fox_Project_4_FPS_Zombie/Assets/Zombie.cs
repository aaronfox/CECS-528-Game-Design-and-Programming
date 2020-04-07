using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float radiusForAttacking = 4.0f;
    public Transform targetTransform;
    public float speed = 1.0f;
    float YPosition;

    // Start is called before the first frame update
    void Start()
    {
        YPosition = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(targetTransform.position, transform.position);

        // Basic following AI
        if (distanceToPlayer <= radiusForAttacking)
        {
            transform.position = new Vector3(transform.position.x, YPosition, transform.position.z);
            Vector3 targetPostition = new Vector3(targetTransform.position.x,
                                       this.transform.position.y,
                                       targetTransform.position.z);
            this.transform.LookAt(targetPostition);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusForAttacking);
    }
}
