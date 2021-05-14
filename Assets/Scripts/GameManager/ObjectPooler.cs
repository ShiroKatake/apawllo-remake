using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tags for pools.
/// </summary>
public enum Pools
{
	ApawlloLightBullet,
	ApawlloMediumBullet,
	ApawlloHeavyBullet,
	SquireBullet
}

/// <summary>
/// A generic object pooler.
/// </summary>
public class ObjectPooler : MonoBehaviour
{
	[System.Serializable]
	public class Pool
	{
		[HideInInspector]
		public Pools tag;
		public GameObject objectToPool;
		public int amountToPool;
	}

	#region Serialized Fields
	[SerializeField] private List<Pool> pools;
	#endregion

	#region Private Fields
	private Dictionary<Pools, Queue<GameObject>> poolDictionary;
	#endregion

	#region Public Properties
	public static ObjectPooler Instance { get; private set; }
	#endregion

	/// <summary>
	/// Pseudo singleton.
	/// </summary>
	private void Awake()
	{
		Instance = this;
	}

	/// <summary>
	/// Initialize pools by setting their tags using the assigned objects, 
	/// instantiating said objects, and adding them to the dictionary.
	/// </summary>
	private void Start()
	{
		poolDictionary = new Dictionary<Pools, Queue<GameObject>>();

		foreach (Pool pool in pools)
		{			
			pool.tag = pool.objectToPool.GetComponent<IPooledObject>().PoolType;

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

	/// <summary>
	/// Spawns a game object from a specified pool.
	/// </summary>
	/// <param name="tag">Which pool to spawn from</param>
	/// <param name="position">Position to spawn at</param>
	/// <param name="rotation">Rotation to spawn at</param>
	/// <returns>The object spawned</returns>
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

	/// <summary>
	/// Return an object to a specified pool.
	/// </summary>
	/// <param name="tag">Which pool to return to</param>
	/// <param name="objectToReturn">which object to return</param>	
	public void ReturnToPool(Pools tag, GameObject objectToReturn)
	{
		poolDictionary[tag].Enqueue(objectToReturn);
		objectToReturn.SetActive(false);
	}
}
