using UnityEngine;

/// <summary>
/// Shoots bullets.
/// </summary>
public class Shoot : MonoBehaviour
{
	#region Serialized Fields
	[SerializeField] private Transform firePoint;

	[Header("Charge Times")]
	[SerializeField] private float mediumCharge = 1f;
	[SerializeField] private float heavyCharge = 2f;

	[SerializeField] private Bullet normalBullet;
	[SerializeField] private Bullet mediumBullet;
	[SerializeField] private Bullet heavyBullet;
	#endregion

	#region Private Fields
	private Ammo ammo;
	private float timePassed;
	private bool isTiming = false;
	#endregion

	private void Awake()
	{
		ammo = GetComponent<Ammo>();
	}

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
		if (!ammo.CanShoot)
			return; 
		isTiming = true;
	}

	/// <summary>
	/// Fires a bullet from the pool depending on how long "Shoot" was held down for 
	/// (currently only have 1 prefab so all 3 types use the same prefab).
	/// </summary>
	public void OnShoot()
	{
		if (!ammo.CanShoot)
			return;
		
		Bullet bullet = normalBullet;
		Debug.Log("Shooting normal charge.");
		//Move bullet type checking to ammo script
		if (timePassed >= mediumCharge && timePassed < heavyCharge)
		{
			bullet = mediumBullet;
			Debug.Log("Shooting medium charge.");
		}
		if (timePassed >= heavyCharge)
		{
			bullet = heavyBullet;
			Debug.Log("Shooting heavy charge.");
		}

		ObjectPooler.Instance.SpawnFromPool(bullet.BulletType, firePoint.position, firePoint.rotation);
		ammo.BulletCount--;
		timePassed = 0f;
		isTiming = false;
	}
}
