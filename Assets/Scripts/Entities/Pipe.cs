// Created by Binh Bui on 08, 11, 2021

using UnityEngine;
using Utilities;

namespace Entities
{
    public class Pipe : MonoBehaviour, IMove, ISpawn
    {
        public float offsetY;
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

        public void Spawn()
        {
            if (Random.Range(0, 10f) < 1f)
            {
                ObjectPool.Instance.Spawn("Star", go =>
                {
                    var max = transform.position.y + offsetY;
                    var min = transform.position.y - offsetY;
                    go.transform.position = new Vector3(transform.position.x, Random.Range(min, max));
                });
            }
        }
    }
}