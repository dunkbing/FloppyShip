using UnityEngine;
using Utilities;

namespace Entities
{
    public class PlayerController : Entity
    {
        public Rigidbody2D rb;
        public float force = 2000f;
        private float _gravity;

        private void Start()
        {
            _gravity = rb.gravityScale;
        }

        private void Update()
        {
            var vel = rb.velocity;
            var ang = Mathf.Atan2(vel.y, 10) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, ang));
            transform.position = new Vector3(0, transform.position.y);
            Jump();
        }

        private void Jump()
        {
            if (Input.GetMouseButton(0))
            {
                rb.AddForce(Vector2.up * (_gravity * force * Time.deltaTime));
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                AudioManager.Instance.Play(AudioName.PlayerExplosion);
                Explode();
            }
        }
    }
}
