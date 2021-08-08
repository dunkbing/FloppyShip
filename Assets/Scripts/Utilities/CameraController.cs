using System;
using UnityEngine;

namespace Utilities
{
    public class CameraController : MonoBehaviour
    {
        public Animator camAnim;
        private static readonly int ShakeTrigger = Animator.StringToHash("Shake");

        public static CameraController Instance { get; private set; }

        private void Start()
        {
            Instance ??= this;
        }

        public void Shake()
        {
            camAnim.SetTrigger(ShakeTrigger);
        }
    }
}
