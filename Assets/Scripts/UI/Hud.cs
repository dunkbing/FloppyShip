// Created by Binh Bui on 08, 10, 2021

using Entities;
using TMPro;
using UnityEngine;
using Utilities;

namespace UI
{
    public class Hud : MonoBehaviour
    {
        public Animator scoreAnim;
        public TextMeshProUGUI scoreCounterTmp;
        public TextMeshProUGUI scoreTmp;
        public TextMeshProUGUI highScoreTmp;
        public TextMeshProUGUI starTmp;
        private static readonly int Score = Animator.StringToHash("Score");
        private float _deltaTime;
        public static Hud Instance { get; private set; }

        private void Start()
        {
            Instance ??= this;
            Application.targetFrameRate = 60;

            GameStats.LoadHighScore();
        }

        private void Update()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;
        }

        public void SetScore(int score)
        {
            if (score != 0 && score % 10 == 0)
            {
                scoreAnim.SetTrigger(Score);
            }
            scoreCounterTmp.SetText($"{score}");
        }

        public void SetStar(int star)
        {
            starTmp.SetText($"{star}");
        }

        public void ShowResults()
        {
            scoreTmp.SetText($"Score: {GameStats.Score}");
            highScoreTmp.SetText($"Best Score: {GameStats.HighScore}");
        }

        public void IncreaseStar()
        {
            if (GameStats.Star < 5) ++GameStats.Star;
            starTmp.SetText($"{GameStats.Star}");
        }

        public void DecreaseStar()
        {
            if (GameStats.Star > 0) --GameStats.Star;
            starTmp.SetText($"{GameStats.Star}");
            if (GameStats.Star <= 0)
            {
                Player.Instance.Explode();
            }
        }

    }
}