// Created by Binh Bui on 07, 28, 2021

using Entities;
using UnityEngine;
using Utilities;

namespace UI
{
    public class GameUI : MonoBehaviour
    {
        public GameObject mainMenu;
        public GameObject pauseMenu;

        public static GameUI Instance { get; private set; }

        private void Start()
        {
            Instance ??= this;

            AdsManager.Instance.onEarnedReward += (() =>
            {
                GameStats.State = GameState.Running;
                Player.Instance.SetActive(true);
                Player.Instance.PlayGame();
                pauseMenu.SetActive(false);
                // spawning objects
                Spawner.Instance.StartSpawning();
                Spawner.Instance.CleanTrash();
                // reset star
                GameStats.Star = 1;
                Hud.Instance.SetStar(GameStats.Star);
            });
        }

        public void StartGame()
        {
            GameStats.State = GameState.Running;
            Player.Instance.SetActive(true);
            Player.Instance.PlayGame();

            // reset score
            GameStats.Score = 0;
            GameStats.Star = 1;
            Hud.Instance.SetScore(GameStats.Score);
            Hud.Instance.SetStar(GameStats.Star);

            // spawning objects
            Spawner.Instance.RetrieveObstacles();
            Spawner.Instance.StartSpawning();

            mainMenu.SetActive(false);
        }

        public void Menu()
        {
            GameStats.State = GameState.Pause;
            Player.Instance.SetActive(true);
            Player.Instance.PreStart();
            Spawner.Instance.RetrieveObstacles();
        }

        public void PauseGame()
        {
            GameStats.State = GameState.Stop;

            pauseMenu.SetActive(true);

            Spawner.Instance.StopSpawning();
        }

        public void ShowAds()
        {
            AdsManager.Instance.ShowRewardedVideo();
        }

        public void Quit()
        {
            Application.Quit(0);
        }

        public void Click()
        {
            AudioManager.Instance.Play("Click");
        }
    }
}