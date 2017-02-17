using UnityEngine;
using System.Collections;

namespace ShooterAR
{
    public class Enemy : MonoBehaviour
    {
        public float speed = 10f;
        private RaycastHit hit;
        private float distance = 2;

        // Use this for initialization
        void OnEnable()
        {
            StartCoroutine(UpdateRoutine());
        }

        IEnumerator UpdateRoutine ()
        {
            while (true)
            {
                if (GameManager.Instance.isGameOver)
                {
                    break;
                }

                if (Physics.Raycast(transform.position, transform.forward, out hit, distance))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        Debug.Log("HIT");
                        GameManager.Instance.PlayerKilled();
                        this.gameObject.SetActive(false);                       
                    }
                }

                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.player.transform.position, step);
                transform.LookAt(GameManager.Instance.player.transform.position);

                yield return null;
            }
        }
    }
}