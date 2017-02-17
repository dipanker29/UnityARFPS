using UnityEngine;
using System.Collections;

namespace ShooterAR
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 80f;
        private float selfDestroyTime = 5f;

        private RaycastHit hit;
        private float distance = 5;

        // Use this for initialization
        void OnEnable()
        {
            StartCoroutine(UpdateRoutine());
            Invoke("SelfDestroy", selfDestroyTime);
        }

        IEnumerator UpdateRoutine()
        {
            while (true)
            {
                if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
                {
                    if (hit.collider.CompareTag("Enemy"))
                    {
                        hit.collider.gameObject.SetActive(false);
                        CancelInvoke();
                        GameManager.Instance.EnemyKilled();
                        gameObject.SetActive(false);
                    }
                }

                transform.Translate(Vector3.forward * speed * Time.deltaTime);
                yield return 0;
            }
        }

        void SelfDestroy()
        {
            gameObject.SetActive(false);
        }
    }
}
