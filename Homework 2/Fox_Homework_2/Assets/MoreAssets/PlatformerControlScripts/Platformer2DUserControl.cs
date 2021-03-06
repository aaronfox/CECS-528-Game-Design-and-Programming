using System;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        public bool hasSmiley = false;
        [SerializeField] public GameObject cogPrefab;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }

        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = Input.GetButtonDown("Jump");
            }
        }

        IEnumerator ShootCog()
        {
            GameObject cog = Instantiate(cogPrefab, new Vector2(transform.position.x + 0.2f, transform.position.y), Quaternion.identity);
            cog.GetComponent<Rigidbody2D>().velocity = new Vector2(12.0f * transform.localScale.x, 6.0f);
            yield return new WaitForSeconds(1.5f);
            Destroy(cog);
        }

        private void Shoot()
        {
            StartCoroutine(ShootCog());
        }

        IEnumerator GoFaster(float speed)
        {
            float before_speed = GetComponent<PlatformerCharacter2D>().m_MaxSpeed;
            GetComponent<PlatformerCharacter2D>().m_MaxSpeed = speed;
            yield return new WaitForSeconds(2.0f);
            GetComponent<PlatformerCharacter2D>().m_Anim.SetBool("Rolling", false);
            GetComponent<PlatformerCharacter2D>().m_MaxSpeed = before_speed;
        }

        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.DownArrow);
            float h = Input.GetAxis("Horizontal");

            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;

            bool shootButton = Input.GetKeyDown(KeyCode.LeftShift);
            // Check if user is able to shoot after getting smiley
            if (shootButton && hasSmiley)
            {
                Shoot();
            }

            bool rollButton = Input.GetKeyDown(KeyCode.RightShift);
            if (rollButton && GetComponent<PlatformerCharacter2D>().m_Anim.GetFloat("Speed") > 4f)
            {
                GetComponent<PlatformerCharacter2D>().m_Anim.SetBool("Rolling", true);
                StartCoroutine(GoFaster(25.0f));

            }
        }
    }
}
