using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	//private GameObject[] enemies;
	[System.Serializable]
    public class ObjectPool
	{
        public string type; //Easy
        public int amount; // 100
        public GameObject prefab; // Easy Enemy
	}

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public List<ObjectPool> pools;

    private List<string> enemyTypes = new List<string>();

    private float timer = 0;
    private float maxTimer = 1;

    public static Vector3 basePos;

    // Start is called before the first frame update
    void Start()
    {
       //enemies = Resources.LoadAll<GameObject>("Enemies");
        basePos = GameObject.Find("Base").transform.position;

        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(ObjectPool pool in pools)
		{
            enemyTypes.Add(pool.type);
        }

        InstantiateEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

		if (timer < 0)
		{
            //Instantiate(enemies[Random.Range(0, enemies.Length)], transform.position, Quaternion.identity);
            EnableEnemies(enemyTypes[Random.Range(0, enemyTypes.Count)]);
            timer = maxTimer;
		}
	}

    private void InstantiateEnemies()
	{
        GameObject temp;
        int amount;

        foreach (ObjectPool pool in pools)
		{
            Queue<GameObject> objectPool = new Queue<GameObject>();

            amount = pool.amount;

            for (int i = 0; i < amount; i++)
			{
                temp = Instantiate(pool.prefab, transform.position, Quaternion.identity, gameObject.transform);
                temp.SetActive(false);
                objectPool.Enqueue(temp);
            }

            poolDictionary.Add(pool.type, objectPool);
		}
    }

    private void EnableEnemies(string type)
	{
        GameObject temp = poolDictionary[type].Dequeue();
        temp.SetActive(true);
        poolDictionary[type].Enqueue(temp);
    }
}
