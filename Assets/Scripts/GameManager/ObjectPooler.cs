using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pools
{
	ApawlloBullet,
	SquireBullet
}

public class ObjectPooler : MonoBehaviour
{
	[System.Serializable]
	public class Pool
	{
		public Pools tag;
		public GameObject objectToPool;
		public int amountToPool;
	}

	[SerializeField] private List<Pool> pools;

	public Dictionary<Pools, Queue<GameObject>> poolDictionary;
	public static ObjectPooler Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		poolDictionary = new Dictionary<Pools, Queue<GameObject>>();

		foreach (Pool pool in pools)
		{
			Queue<GameObject> objectPool = new Queue<GameObject>();

			for (int i = 0; i < pool.amountToPool; i++)
			{
				GameObject obj = Instantiate(pool.objectToPool);
				obj.SetActive(false);
				objectPool.Enqueue(obj);
			}

			poolDictionary.Add(pool.tag, objectPool);
		}
	}

	public GameObject SpawnFromPool (Pools tag, Vector3 position, Quaternion rotation)
	{
		if (!poolDictionary.ContainsKey(tag))
		{
			Debug.LogWarning("Pool with tag" + tag + "doesn't exist.");
			return null;
		}

		if (poolDictionary[tag].Count == 0)
		{
			GameObject obj = Instantiate(pools[(int)tag].objectToPool);
			obj.SetActive(false);
			poolDictionary[tag].Enqueue(obj);
		}

		GameObject objectToSpawn = poolDictionary[tag].Dequeue();

		objectToSpawn.SetActive(true);
		objectToSpawn.transform.position = position;
		objectToSpawn.transform.rotation = rotation;

		return objectToSpawn;
	}

	public void ReturnToPool(Pools tag, GameObject objectToReturn)
	{
		poolDictionary[tag].Enqueue(objectToReturn);
		objectToReturn.SetActive(false);
	}
}
