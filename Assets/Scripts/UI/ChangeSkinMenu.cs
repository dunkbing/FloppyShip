// Created by Binh Bui on 08, 11, 2021

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class ChangeSkinMenu : MonoBehaviour
    {
        public List<Sprite> sprites;
        public Image skinImage;

        public static ChangeSkinMenu Instance { get; private set; }

        private void Start()
        {
            Instance ??= this;
        }

        public void ChangeSkin(int i)
        {
            PlayerSkin.Instance.spriteIndex += i;
            if (PlayerSkin.Instance.spriteIndex >= sprites.Count)
            {
                PlayerSkin.Instance.spriteIndex = 0;
            } else if (PlayerSkin.Instance.spriteIndex < 0)
            {
                PlayerSkin.Instance.spriteIndex = sprites.Count - 1;
            }
            skinImage.sprite = sprites[PlayerSkin.Instance.spriteIndex];
            PlayerSkin.Instance.SetSprite(skinImage.sprite);
        }

    }
}
