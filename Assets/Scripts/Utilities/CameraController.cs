using System;
using UnityEngine;

namespace Utilities
{
    public class CameraController : MonoBehaviour
    {
        public Animator camAnim;
        private static readonly int ShakeTrigger = Animator.StringToHash("Shake");
        public Camera cam;

        public static CameraController Instance { get; private set; }

        private void Start()
        {
            Instance ??= this;
        }

        public void Shake()
        {
            camAnim.SetTrigger(ShakeTrigger);
        }

        public void ZoomIn()
        {
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - Time.deltaTime, 5.0f, 5.3f);
        }

        public void ZoomOut()
        {
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize + Time.deltaTime, 5.0f, 5.3f);
        }
    }
}
