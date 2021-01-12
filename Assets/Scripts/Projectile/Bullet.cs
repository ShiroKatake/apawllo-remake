using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float speed = 10f;
	[SerializeField] private float lifetime = 10f;
	[SerializeField] private Pools pool;
	private float timePassed;

	private void Update()
	{

		Vector2 m = transform.right * speed * Time.deltaTime;
		transform.Translate(m, Space.World);

		timePassed += Time.deltaTime;
		if (timePassed >= lifetime)
		{
			ObjectPooler.Instance.ReturnToPool(pool, gameObject);
		}
	}
}
