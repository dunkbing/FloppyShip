// Created by Binh Bui on 07, 27, 2021

using UnityEngine;

namespace Entities.Background
{
    public class Bound : MonoBehaviour, IMove
    {
        private readonly float _resetPos = -30f;

        private Vector3 _spawnPos;
        private readonly float _speed = 10;

        // Start is called before the first frame update
        private void Start()
        {
            _spawnPos = new Vector3(90f, 0);
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            Move();
        }

        public void Move()
        {
            // reset pos
            if (transform.position.x <= _resetPos)
            {
                var newPos = transform.position + _spawnPos;
                Instantiate(BoundContainer.Instance.Get(), newPos, Quaternion.identity);
                Destroy(gameObject);
            }
            // move
            transform.position += Vector3.left * (_speed * Time.fixedDeltaTime);
        }
    }
}
