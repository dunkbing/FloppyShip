// Created by Binh Bui on 07, 30, 2021

using UnityEngine;

namespace Utilities
{
    public class Explosion : MonoBehaviour
    {
        public static void Explode(Vector3 position, float radius, float force)
        {
            var collider2Ds = new Collider2D[8];
            Physics2D.OverlapCircleNonAlloc(position, radius, collider2Ds);
            foreach (var obj in collider2Ds)
            {
                if (obj == null) continue;

                Vector2 direction = obj.transform.position - position;
                var rb2d = obj.GetComponent<Rigidbody2D>();
                if (rb2d != null)
                {
                    rb2d.AddForce(direction * force, ForceMode2D.Impulse);
                }
            }
        }
    }
}