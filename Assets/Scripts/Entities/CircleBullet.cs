using UnityEngine;

namespace Entities
{
    public class CircleBullet : MonoBehaviour, IMove
    {
        public GameObject explosion;
        private Vector3 _targetPos;
        // private float _traveled;
        private readonly float _speed = 10f;

        private void Start()
        {
            if (Player.Instance)
            {
                _targetPos = (Player.Instance.transform.position - transform.position).normalized;
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
            // Instantiate(explosion, transform.position, Quaternion.identity);
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
