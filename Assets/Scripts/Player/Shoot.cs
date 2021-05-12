using UnityEngine;

/// <summary>
/// Shoots bullets.
/// </summary>
public class Shoot : MonoBehaviour
{
	[SerializeField] private Transform firePoint;

	[Header("Charge Times")]
	[SerializeField] private float mediumCharge = 1f;
	[SerializeField] private float heavyCharge = 2f;

	private float timePassed;
	private bool isTiming = false;

	/// <summary>
	/// Keeps track of time as "Shoot" button is held down.
	/// </summary>
	void Update()
	{
		if (isTiming)
			timePassed += Time.deltaTime;
	}

	/// <summary>
	/// Initiates time counting when "Shoot" button is pressed.
	/// </summary>
	public void StartTimer()
	{
		isTiming = true;
	}

	/// <summary>
	/// Fires a bullet from the pool depending on how long "Shoot" was held down for 
	/// (currently only have 1 prefab so all 3 types use the same prefab).
	/// </summary>
	public void OnShoot()
	{
		Pools bulletPool = Pools.ApawlloBullet;
		Debug.Log("Shooting normal charge.");

		if (timePassed >= mediumCharge && timePassed < heavyCharge)
		{
			bulletPool = Pools.ApawlloBullet;
			Debug.Log("Shooting medium charge.");
		}
		if (timePassed >= heavyCharge)
		{
			bulletPool = Pools.ApawlloBullet;
			Debug.Log("Shooting heavy charge.");
		}

		ObjectPooler.Instance.SpawnFromPool(bulletPool, firePoint.position, firePoint.rotation);
		timePassed = 0f;
		isTiming = false;
	}
}
