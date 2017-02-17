using UnityEngine;
using System.Collections;
using CnControls;

namespace ShooterAR
{
    public class Gun : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public Transform bulletSpawnPosition;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (CnInputManager.GetButtonDown("Jump"))
            {
                TriggerFire();
            }
        }

        void TriggerFire()
        {
            ObjectPool.Instance.SpawnBullet(bulletPrefab, bulletSpawnPosition);
        }
    }
}
