using UnityEngine;
using System.Collections;

namespace ShooterAR
{
    public class EnemyController : MonoBehaviour
    {
        public GameObject enemyPrefab;
        public Transform[] enemySpawnPosition;
        public Transform playerPosition;
        public Transform cameraObject;

        private float spawnTime = 5f;
        private bool canSpawn = true;
        private int spawnCount = 0;

        // Use this for initialization
        void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        IEnumerator SpawnRoutine ()
        {
            while (true)
            {
                playerPosition.position = cameraObject.position;
                playerPosition.rotation = cameraObject.rotation;

                if (GameManager.Instance.isGameOver)
                {
                    break;
                }

                yield return new WaitForSeconds(spawnTime);
                if (GameManager.Instance.isInFieldOfView)
                {
                    SpawnEnemy();
                    spawnTime += ((float)spawnCount / 100f);
                }
            }
        }

        public void SpawnEnemy()
        {
            if (enemySpawnPosition == null || enemySpawnPosition.Length == 0)
            {
                Debug.LogError("Assign Enemy Spawn Position");
                return;
            }

            int randomPostion = Random.Range(0, enemySpawnPosition.Length);
            ObjectPool.Instance.SpawnEnemy(enemyPrefab, enemySpawnPosition[randomPostion]);
            spawnCount++;
        }
    }
}
