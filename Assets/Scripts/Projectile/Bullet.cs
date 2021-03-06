﻿using UnityEngine;

/// <summary>
/// Handles bullet movement and lifetime.
/// </summary>
public class Bullet : MonoBehaviour, IPooledObject
{
	#region Serialized Fields
	[SerializeField] private int damage;
	[SerializeField] private int cost;
	[SerializeField] private float speed = 10f;
	[SerializeField] private float lifetime = 10f;
	[SerializeField] private Pools pool;
	#endregion

	#region Private Fields
	private float timePassed;
	#endregion

	public int Damage { get => damage; }
	public Pools PoolType { get => pool; }
	public int Cost { get => cost; }

	/// <summary>
	/// Moves the bullet.
	/// Return the bullet to pool if lifetime exceeds amount specified.
	/// </summary>
	private void Update()
	{
		Vector2 m = transform.right * speed * Time.deltaTime;
		transform.Translate(m, Space.World);

		timePassed += Time.deltaTime;
		if (timePassed >= lifetime)
		{
			ObjectPooler.Instance.ReturnToPool(pool, gameObject);
			timePassed = 0f;
		}
	}
}
