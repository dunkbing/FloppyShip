// Created by Binh Bui on 07, 09, 2021

using System;
using UnityEngine;

namespace Entities
{
    [ExecuteInEditMode]
    public class Wall : MonoBehaviour
    {
        [Serializable]
        public enum Side
        {
            Left, Right, Up, Down, None,
        }

        public Side side;
        private void Start()
        {
            var height = Camera.main.orthographicSize * 2f;
            var width = height * Screen.width / Screen.height;
            var position = transform.position;
            position = side switch
            {
                Side.Left => new Vector3(-width / 2, position.y, position.z),
                Side.Right => new Vector3(width / 2, position.y, position.z),
                Side.Up => new Vector3(position.x, height / 2, position.z),
                Side.Down => new Vector3(position.x, -height / 2, position.z),
                Side.None => position,
                _ => position,
            };
            transform.position = position;
        }
    }
}