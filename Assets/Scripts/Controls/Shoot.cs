using UnityEngine;

public class Shoot : MonoBehaviour
{
	[SerializeField] private Transform firePoint;

	[Header("Charge Times")]
	[SerializeField] private float normalCharge = 0f;
	[SerializeField] private float mediumCharge = 1f;
	[SerializeField] private float heavyCharge = 2f;

	private float timePassed;
	private bool isCounting = false;

    void Update()
	{
		if (isCounting)
			timePassed += Time.deltaTime;
	}

	public void StartTimer()
	{
		isCounting = true;
	}

	public void OnShoot()
	{
		Pools bulletPool = Pools.ApawlloBullet;

		if (timePassed >= normalCharge && timePassed < mediumCharge)
		{
			bulletPool = Pools.ApawlloBullet;
			Debug.Log("Shooting normal charge.");
		}
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
		isCounting = false;
	}
}
