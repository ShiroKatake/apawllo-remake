using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	[System.Serializable]
	public class Pool
	{
		public string tag;
		public GameObject prefab;
		public int size;
	}

	[SerializeField] private List<Pool> pools;
	public Dictionary<string, Queue<GameObject>> poolDictionary;

	private void Start()
	{
		poolDictionary = new Dictionary<string, Queue<GameObject>>();
	}
}
