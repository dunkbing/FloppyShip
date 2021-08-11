// Created by Binh Bui on 08, 09, 2021

using UnityEngine;
using Utilities;

namespace Entities
{
    public class Saw : FloatingObject, IMove, ISpawn
    {
        public Sprite[] sprites;
        public SpriteRenderer spriteRenderer;

        protected override void Start()
        {
            base.Start();
            amplitude = 4.5f;
            frequency = 1f;
        }

        private void Update()
        {
            Move();
            Float();
        }

        public void Move()
        {
            if (GameStats.State == GameState.Stop || GameStats.State == GameState.Pause)
            {
                return;
            }
            transform.position += Vector3.left * (GameStats.ObstacleSpeed * Time.deltaTime);
            _posOffset.x = transform.position.x;
        }

        public void Spawn()
        {
            spriteRenderer.sprite = sprites[Random.Range(0, 2)];
        }
    }
}