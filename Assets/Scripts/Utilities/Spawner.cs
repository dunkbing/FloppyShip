// Created by Binh Bui on 08, 08, 2021

using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utilities
{
    public class Spawner : MonoBehaviour
    {
        public static Spawner Instance { get; private set; }
        public List<GameObject> Trash { get; private set; }

        private void Awake()
        {
            Instance ??= this;

            Trash = new List<GameObject>();
        }

        public void StartSpawning()
        {
            Invoke(nameof(SpawnObstacle), 1f);
            // Invoke(nameof(SpawnEnemy), 3f);
        }

        public void StopSpawning()
        {
            CancelInvoke(nameof(SpawnObstacle));
            // CancelInvoke(nameof(SpawnStar));
        }

        private void SpawnStar()
        {
            ObjectPool.Instance.Spawn("Star", delegate(GameObject go)
            {
                go.transform.position = new Vector3(7, Random.Range(-3.5f, 3.5f));
            });
        }

        private void SpawnEnemy()
        {
            ObjectPool.Instance.Spawn("Enemy0", delegate(GameObject go)
            {
                go.transform.position = new Vector3(7, 0);
            });
            Invoke(nameof(SpawnEnemy), Random.Range(10, 15));
        }

        private void SpawnObstacle()
        {
            float time;

            var random = Random.Range(1.0f, 10.0f);
            if (random < 4f)
            {
                time = 1;
                ObjectPool.Instance.Spawn("Pipe", delegate(GameObject go)
                {
                    go.transform.position = new Vector3(7, Random.Range(-2.5f, 2.5f));
                });
            } else if (random < 8f)
            {
                time = 1;
                ObjectPool.Instance.Spawn("Pipe2", delegate(GameObject go)
                {
                    go.transform.position = new Vector3(7, Random.Range(-2.5f, 2.5f));
                });
            } else
            {
                time = 2;
                ObjectPool.Instance.Spawn("Saw", delegate(GameObject go)
                {
                    go.transform.position = new Vector3(7, 0f);
                });
            }

            Invoke(nameof(SpawnObstacle), time);
        }

        public void RetrieveObstacles()
        {
            CleanTrash();
            ObjectPool.Instance.Retrieve("Pipe");
            ObjectPool.Instance.Retrieve("Pipe2");
            ObjectPool.Instance.Retrieve("Spike");
            ObjectPool.Instance.Retrieve("Saw");
            ObjectPool.Instance.Retrieve("Enemy0");
            ObjectPool.Instance.Retrieve("Star");
        }

        private void SpawnSpike()
        {
            ObjectPool.Instance.Spawn("DoubleSpike", delegate(GameObject go)
            {
                go.transform.position = new Vector3(6, -3.4f);
            });
        }

        private void SpawnPipe()
        {
            ObjectPool.Instance.Spawn("Pipe", delegate(GameObject go)
            {
                go.transform.position = new Vector3(6, Random.Range(-2.5f, 2.5f));
            });
        }

        public void CleanTrash()
        {
            foreach (var go in Trash)
            {
                Destroy(go);
            }
        }
    }
}