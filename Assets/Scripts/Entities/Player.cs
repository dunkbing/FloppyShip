using UI;
using UnityEngine;
using Utilities;

namespace Entities
{
    public class Player : Entity
    {
        public Rigidbody2D rb;
        public GameObject explosion;
        public ParticleSystem fireParticle;
        public float jumpForce = 2000f;
        public ParticleSystem starExplosion;
        public ParticleSystem obsExplosion;
        public Animator anim;
        public SpriteRenderer spriteRenderer;

        private float _timer;

        private float _unvulnerableTimer;
        // private float _gravity;

        public static Player Instance;

        public bool Alive { get; set; }

        private static float _xPos = -1.5f;
        public static Vector3 DefaultPosition = new Vector3(_xPos, 0f);
        private static readonly int TakeDmg = Animator.StringToHash("TakeDmg");

        private void Start()
        {
            Instance ??= this;
            Alive = true;
            // _gravity = rb.gravityScale;
            OnExplode += (() =>
            {
                AdsManager.Instance.onEarnedReward += () =>
                {
                    anim.SetTrigger(TakeDmg);
                };
                AudioManager.Instance.Play("Explosion");
                Alive = false;
                // CameraController.Instance.Shake();
                Spawner.Instance.Trash.Add(Instantiate(PlayerSkin.Instance.Fragments, transform.position, Quaternion.identity));
                Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 1f);
                Explosion.Explode(transform.position, .3f, 70f);
                GameUI.Instance.PauseGame();
                GameStats.SaveHighScore();
                Hud.Instance.ShowResults();
            });
        }

        private void Update()
        {
            if (GameStats.State == GameState.Pause || GameStats.State == GameState.Stop)
            {
                return;
            }

            _timer += Time.deltaTime;
            if (_timer >= 1)
            {
                _timer = 0;
                Hud.Instance.SetScore(++GameStats.Score);
            }

            var vel = rb.velocity;
            var ang = Mathf.Atan2(vel.y, 10) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, ang));
            transform.position = new Vector3(_xPos, transform.position.y);

            if (Input.GetMouseButtonDown(0))
            {
                FlyUp();
            } else if (Input.GetMouseButton(0))
            {
                FlyUp();
                CameraController.Instance.ZoomIn();
            }
            else
            {
                fireParticle.Stop();
                CameraController.Instance.ZoomOut();
            }
        }

        private void FlyUp()
        {
            rb.AddForce(Vector2.up * (rb.gravityScale * jumpForce * Time.deltaTime));
            fireParticle.Play();
            AudioManager.Instance.Play("FlyUp");
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerTakeDmg")) return;

            if (other.gameObject.CompareTag("Bound") || other.gameObject.CompareTag("Obstacle"))
            {
                if (GameStats.State != GameState.Pause)
                {
                    // Explode();
                    obsExplosion.Play();
                    anim.SetTrigger(TakeDmg);
                    Hud.Instance.DecreaseStar();
                    AudioManager.Instance.Play("Hit");
                    CameraController.Instance.Shake();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Star"))
            {
                other.gameObject.SetActive(false);
                Hud.Instance.IncreaseStar();
                starExplosion.Play();
                AudioManager.Instance.Play("PowerUp");
            }
            // else if (other.CompareTag("Bullet"))
            // {
            //     Explode();
            // }
        }

        public void PlayGame()
        {
            rb.gravityScale = 1f;
            transform.position = DefaultPosition;
            Alive = true;
        }

        public void SetActive(bool active)
        {
            Alive = active;
            gameObject.SetActive(active);
        }

        public void PreStart()
        {
            rb.gravityScale = 0f;
            transform.position = DefaultPosition;
            transform.rotation = Quaternion.identity;
            var color = spriteRenderer.color;
            color.r = 1f;
            spriteRenderer.color = color;
            Alive = true;
        }
    }
}
