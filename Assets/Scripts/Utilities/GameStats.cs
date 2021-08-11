// Created by Binh Bui on 07, 28, 2021

using System;
using System.IO;
using UnityEngine;

namespace Utilities
{
    public static class GameStats
    {
        public static GameState State = GameState.Pause;

        public const float ObstacleSpeed = 4.0f;

        public static int Score = 0;

        public static int HighScore = 0;

        public static int Star = 1;
        private static readonly string FilePath = $"{Application.persistentDataPath}/highscore.txt";

        public static void LoadHighScore()
        {
            try
            {
                HighScore = int.Parse(File.ReadAllText(FilePath));
            }
            catch (Exception e) when (e is FileNotFoundException || e is DirectoryNotFoundException ||
                                      e is IndexOutOfRangeException || e is FormatException)
            {
                Debug.Log(e.Message);
            }
        }

        public static void SaveHighScore()
        {
            if (Score >= HighScore)
            {
                HighScore = Score;
            }
            File.WriteAllText(FilePath, HighScore.ToString());
        }
    }

    public enum GameState
    {
        Running,
        Stop,
        Pause,
    }
}