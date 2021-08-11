// Created by Binh Bui on 08, 08, 2021

using UnityEngine;
using Utilities;

namespace Entities
{
    public class Obstacle : Entity, IMove
    {
        private void Update()
        {
            Move();
        }

        public void Move()
        {
            if (GameStats.State == GameState.Stop || GameStats.State == GameState.Pause)
            {
                return;
            }
            transform.position += Vector3.left * (GameStats.ObstacleSpeed * Time.deltaTime);
        }
    }
}
