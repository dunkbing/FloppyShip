// Created by Binh Bui on 07, 28, 2021

using System;
using Entities;
using UnityEngine;
using Utilities;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject menu;

        public static MainMenu Instance { get; private set; }

        private void Start()
        {
            Instance ??= this;
        }

        public void StartGame()
        {
            GameStats.Paused = false;
            GameStats.CurrentPlayer.SetActive(true);
            PlayerController.Instance.PlayGame();

            menu.SetActive(false);
        }

        public void PauseGame()
        {
            GameStats.Paused = true;

            menu.SetActive(true);
        }
    }
}