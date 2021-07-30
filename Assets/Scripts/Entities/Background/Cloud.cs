// Created by Binh Bui on 07, 27, 2021

using UnityEngine;
using Utilities;

namespace Entities.Background
{
    public class Cloud : MonoBehaviour, IMove
    {
        public float parallaxEffect;
        private readonly float _speed = 10;
        private void Update()
        {
            Move();
        }

        private void OnBecameInvisible()
        {
            transform.position = new Vector3(9.8f, transform.position.y);
        }

        public void Move()
        {
            if (GameStats.State == GameState.Stop)
            {
                return;
            }

            transform.position += Vector3.left * (_speed * parallaxEffect * Time.deltaTime);
        }
    }
}