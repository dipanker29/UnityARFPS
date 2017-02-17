using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    private static ObjectPool instance;
    public static ObjectPool Instance
    {
        get
        {
            return instance;
        }
    }

    private List<GameObject> bulletObjects;
    private List<GameObject> enemyObjects;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

	// Use this for initialization
	void Start ()
    {
        bulletObjects = new List<GameObject>();
        enemyObjects = new List<GameObject>();
    }

    #region BULLET POOL
    public void SpawnBullet (GameObject bulletPrefab, Transform spawnPos)
    {
        if (PoolBulletObject (spawnPos) == null)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.transform.position = spawnPos.position;
            obj.transform.rotation = spawnPos.rotation;
            obj.transform.SetParent(this.transform);
            obj.SetActive(true);

            bulletObjects.Add(obj);
        }
    }

    GameObject PoolBulletObject (Transform spawnPos)
    {
        if (bulletObjects == null)
        {
            return null;
        }
        else
        {
            for (int i = 0; i < bulletObjects.Count; i++)
            {
                if (bulletObjects[i].activeInHierarchy == false)
                {
                    bulletObjects[i].transform.position = spawnPos.position;
                    bulletObjects[i].transform.rotation = spawnPos.rotation;
                    bulletObjects[i].SetActive(true);
                    return bulletObjects[i];
                }
            }
        }
        return null;
    }
    #endregion

    #region ENEMY POOL
    public void SpawnEnemy(GameObject enemyPrefab, Transform spawnPos)
    {
        if (PoolEnemyObject(spawnPos) == null)
        {
            GameObject obj = Instantiate(enemyPrefab);
            obj.transform.localPosition = spawnPos.localPosition;
            obj.transform.localRotation = spawnPos.localRotation;
            obj.transform.SetParent(this.transform, false);
            obj.SetActive(true);

            enemyObjects.Add(obj);
        }
    }

    GameObject PoolEnemyObject(Transform spawnPos)
    {
        if (enemyObjects == null)
        {
            return null;
        }
        else
        {
            for (int i = 0; i < enemyObjects.Count; i++)
            {
                if (enemyObjects[i].activeInHierarchy == false)
                {
                    enemyObjects[i].transform.localPosition = spawnPos.localPosition;
                    enemyObjects[i].transform.localRotation = spawnPos.localRotation;
                    enemyObjects[i].SetActive(true);
                    return enemyObjects[i];
                }
            }
        }
        return null;
    }
    #endregion
}
