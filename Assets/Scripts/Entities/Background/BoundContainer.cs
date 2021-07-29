// Created by Binh Bui on 07, 28, 2021

using System.Collections.Generic;
using UnityEngine;

namespace Entities.Background
{
    public class BoundContainer : MonoBehaviour
    {
        public static BoundContainer Instance { get; private set; }

        public List<GameObject> bounds;
        // Start is called before the first frame update
        private void Start()
        {
            Instance ??= this;

            foreach (var bound in bounds)
            {
                bound.transform.position = new Vector3(60f, 0);
            }
        }

        public GameObject Get()
        {
            var index = Random.Range(0, bounds.Count);
            return bounds[index];
        }

        public void Spawn(Vector3 position)
        {
            // Shuffle();
            var bound = bounds[bounds.Count - 1];
            bound.transform.position = position;
            bound.SetActive(true);
            bounds.Remove(bound);
        }

        public void Retrieve(GameObject bound)
        {
            bound.SetActive(false);
            bounds.Add(bound);
        }

        private void Shuffle()
        {
            for (var i = 0; i < bounds.Count; i++) {
                var temp = bounds[i];
                var randomIndex = Random.Range(i, bounds.Count);
                bounds[i] = bounds[randomIndex];
                bounds[randomIndex] = temp;
            }
        }
    }
}
