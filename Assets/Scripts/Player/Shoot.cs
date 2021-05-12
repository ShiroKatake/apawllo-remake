using UnityEngine;

public class Shoot : MonoBehaviour
{
	[SerializeField] private Transform firePoint;

	[Header("Charge Times")]
	[SerializeField] private float mediumCharge = 1f;
	[SerializeField] private float heavyCharge = 2f;

	private float timePassed;
	private bool isTiming = false;

    void Update()
	{
		if (isTiming)
			timePassed += Time.deltaTime;
	}

	public void StartTimer()
	{
		isTiming = true;
	}

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
