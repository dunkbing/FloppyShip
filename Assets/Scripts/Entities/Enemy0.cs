using System.Collections;
using UnityEngine;
using Utilities;

namespace Entities
{
    public class Enemy0 : MonoBehaviour
    {
        public Animator animator;

        public Transform[] bulletTransforms;
        public GameObject bulletPrefab;
        private static readonly int Charging = Animator.StringToHash("Charging");

        // Position Storage Variables
        private Vector3 _posOffset;
        private Vector3 _tempPos;
        public float amplitude = 0.5f;
        public float frequency = 1f;

        private void Start()
        {
            _posOffset = transform.position;
            InvokeRepeating(nameof(StartShooting), 2f, 2f);
        }

        private void Update()
        {
            Float();
            Rotate();
        }

        private void Rotate()
        {
            var vectorToTarget = GameStats.CurrentPlayer.transform.position - transform.position;
            var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 180;
            var q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10f);
        }

        private void Float()
        {
            _tempPos = _posOffset;
            _tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;

            transform.position = _tempPos;
        }

        private void StartShooting()
        {
            animator.SetBool(Charging, true);
            StartCoroutine(Shoot());
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
            CancelInvoke(nameof(StartShooting));
        }
    }
}
