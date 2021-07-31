using System;
using UnityEngine;
using Utilities;

namespace Entities
{
    public class CircleBullet : MonoBehaviour, IMove
    {
        private Vector3 _targetPos;
        // private float _traveled;
        private readonly float _speed = 10f;

        private void Start()
        {
            if (GameStats.CurrentPlayer)
            {
                _targetPos = (GameStats.CurrentPlayer.transform.position - transform.position).normalized;
            }
        }

        // Update is called once per frame
        private void Update()
        {
            Move();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(gameObject);
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        public void Move()
        {
            // if (GameStats.CurrentPlayer)
            // {
            //     _targetPos = GameStats.CurrentPlayer.transform.position;
            // }
            // _traveled += Time.deltaTime * 0.5;
            // transform.position = Vector3.Lerp(transform.position, _targetPos, _traveled);

            transform.position += _targetPos * (_speed * Time.deltaTime);
        }
    }
}
