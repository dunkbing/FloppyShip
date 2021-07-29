// Created by Binh Bui on 07, 26, 2021

using UnityEngine;
using Utilities;

namespace Entities.Background
{
    public class Mountain : MonoBehaviour, IMove
    {
        private float _length, _startPos;
        private readonly float _speed = 3;

        private void Start()
        {
            _startPos = transform.position.x;
            _length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void Update()
        {
            Move();
        }

        public void Move()
        {
            if (GameStats.Paused)
            {
                return;
            }

            transform.position += Vector3.left * (_speed * Time.deltaTime);

            if (transform.position.x <= _startPos - _length)
            {
                transform.position = new Vector3(_startPos, transform.position.y);
            }
        }
    }
}