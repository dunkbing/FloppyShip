// Created by Binh Bui on 07, 28, 2021

using UnityEngine;

namespace Utilities
{
    public class GameStats : MonoBehaviour
    {
        public static GameState State = GameState.Pause;
        public static GameObject CurrentPlayer;
    }

    public enum GameState
    {
        Running,
        Stop,
        Pause,
    }
}