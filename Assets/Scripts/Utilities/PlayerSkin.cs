// Created by Binh Bui on 08, 11, 2021

using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Utilities
{
    public class PlayerSkin : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public int spriteIndex;
        public List<GameObject> fragmentsPrefabs;

        public GameObject Fragments => fragmentsPrefabs[spriteIndex];

        public static PlayerSkin Instance { get; private set; }

        private void Start()
        {
            Instance ??= this;
        }

        public void SetSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
        }

        public void ChangeSkin()
        {
            Spawner.Instance.RetrieveObstacles();
            var player = Player.Instance;
            player.PreStart();
            player.SetActive(true);
        }
    }
}