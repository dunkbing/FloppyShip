using UI;
using UnityEngine;
using Utilities;

namespace Entities
{
    public class PlayerController : Entity
    {
        public Rigidbody2D rb;
        public GameObject fragments;
        public GameObject explosion;
        public ParticleSystem fireParticle;
        public float jumpForce = 2000f;
        // private float _gravity;

        public static PlayerController Instance;

        private void Start()
        {
            Instance ??= this;
            GameStats.CurrentPlayer = gameObject;
            // _gravity = rb.gravityScale;
            OnExplode += (() =>
            {
                Destroy(Instantiate(fragments, transform.position, Quaternion.identity), 1.5f);
                Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 1f);
                Explosion.Explode(transform.position, .3f, 50f);
                MainMenu.Instance.PauseGame();
            });
        }

        private void Update()
        {
            if (GameStats.Paused)
            {
                return;
            }

            var vel = rb.velocity;
            var ang = Mathf.Atan2(vel.y, 10) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, ang));
            transform.position = new Vector3(0, transform.position.y);

            if (Input.GetMouseButton(0))
            {
                FlyUp();
            }
            else
            {
                fireParticle.Stop();
            }
        }

        private void FlyUp()
        {
            rb.AddForce(Vector2.up * (rb.gravityScale * jumpForce * Time.deltaTime));
            fireParticle.Play();

        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Bound"))
            {
                if (!GameStats.Paused)
                {
                    Explode();
                }
            }
        }

        public void PlayGame()
        {
            rb.gravityScale = 1f;
            transform.position = new Vector3(0, 0.5f);
        }

    }
}
