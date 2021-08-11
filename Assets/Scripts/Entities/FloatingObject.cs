// Created by Binh Bui on 08, 10, 2021

using System;
using UnityEngine;

namespace Entities
{
    public abstract class FloatingObject : MonoBehaviour
    {
        // Position Storage Variables
        protected Vector3 _posOffset;
        private Vector3 _tempPos;
        public float amplitude = 0.5f;
        public float frequency = 1f;

        protected virtual void Start()
        {
            _posOffset = transform.position;
        }

        protected void Float()
        {
            _tempPos = _posOffset;
            _tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;

            transform.position = _tempPos;
        }
    }
}