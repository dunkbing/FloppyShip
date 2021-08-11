using System.Collections;
using UnityEngine;
using Utilities;

namespace Entities
{
    public class Enemy0 : FloatingObject, IMove, ISpawn
    {
        public Animator animator;

        public Transform[] bulletTransforms;
        public GameObject bulletPrefab;
        private static readonly int Charging = Animator.StringToHash("Charging");

        private float _timer;
        private float _timeLimit = 5f;
        private bool _shooting;

        private void Update()
        {
            if (!Player.Instance.Alive) return;

            Float();
            Rotate();
            Move();
        }

        private void Rotate()
        {
            var vectorToTarget = Player.Instance.transform.position - transform.position;
            var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 180;
            var q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10f);
        }

        private void StartShooting()
        {
            if (!Player.Instance.Alive)
            {
                StopShooting();
                return;
            }
            animator.SetBool(Charging, true);
            StartCoroutine(Shoot());
        }

        private void StopShooting()
        {
            CancelInvoke(nameof(StartShooting));
        }

        private IEnumerator Shoot()
        {
            yield return new WaitForSeconds(1f);

            animator.SetBool(Charging, false);
            foreach (var bulletTransform in bulletTransforms)
            {
                Instantiate(bulletPrefab, bulletTransform.position, Quaternion.identity);
            }
        }

        private void OnDisable()
        {
            StopShooting();
        }

        private void OnBecameInvisible()
        {
            StopShooting();
        }

        private void OnBecameVisible()
        {
            _shooting = false;
            InvokeRepeating(nameof(StartShooting), 2f, 2f);
        }

        public void Move()
        {
            if (GameStats.State == GameState.Stop || GameStats.State == GameState.Pause)
            {
                return;
            }

            if (_timer >= 4)
            {
                if (_shooting)
                {
                    CancelInvoke(nameof(StartShooting));
                    _shooting = false;
                }
            }

            if (transform.position.x <= 2.4f && _timer < _timeLimit)
            {
                _timer += Time.deltaTime;
                if (!_shooting)
                {
                    InvokeRepeating(nameof(StartShooting), 2f, 2f);
                    _shooting = true;
                }
            } else if (_timer >= _timeLimit)
            {
                transform.position += Vector3.left * (-GameStats.ObstacleSpeed * Time.deltaTime);

                if (transform.position.x > 7)
                {
                    ObjectPool.Instance.Retrieve(nameof(Enemy0));
                }
            } else
            {
                transform.position += Vector3.left * (GameStats.ObstacleSpeed * Time.deltaTime);
            }
            _posOffset.x = transform.position.x;
        }

        public void Spawn()
        {
            _timer = 0;
        }
    }
}
