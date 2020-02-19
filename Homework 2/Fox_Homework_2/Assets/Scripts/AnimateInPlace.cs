using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This cript animates (plays) sprites in place
// Press C key to change sprites facing direction 

public class AnimateInPlace : MonoBehaviour
{
    public Sprite[] sprites;        // Set the sprites to play in the inspector
    public float FramesPerSecond;

    private SpriteRenderer spriteRenderer;
    private int dir = 1;

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (sprites.Length == 0) return;

        int index = (int)(Time.timeSinceLevelLoad * FramesPerSecond);
        index = index % sprites.Length;
        spriteRenderer.sprite = sprites[index];

        // Change sprite facing direction
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (dir == 1)
            {
                dir = -1;
                transform.localRotation = Quaternion.Euler(0, 180.0f, 0);
            }
            else if (dir == -1)
            {
                dir = 1;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
}
