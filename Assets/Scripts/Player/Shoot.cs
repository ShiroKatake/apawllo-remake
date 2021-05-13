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
	private Timer chargeTimer;
	#endregion

	private void Awake()
	{
		ammo = GetComponent<Ammo>();
		chargeTimer = Timer.CreateComponent(gameObject, true);
	}

	/// <summary>
	/// Keeps track of time as "Shoot" button is held down.
	/// </summary>
	void Update()
	{

	}

	/// <summary>
	/// Initiates time counting when "Shoot" button is pressed.
	/// </summary>
	public void StartCharge()
	{
		if (!ammo.CanShoot)
			return;
		chargeTimer.StartTimer();
	}

	/// <summary>
	/// Fires a bullet from the pool depending on how long "Shoot" was held down for 
	/// (currently only have 1 prefab so all 3 types use the same prefab).
	/// </summary>
	public void OnShoot()
	{
		if (!ammo.CanShoot)
			return;

		ObjectPooler.Instance.SpawnFromPool(GetAmmo().BulletType, firePoint.position, firePoint.rotation);
		ammo.BulletCount--;
		chargeTimer.ResetTimer();
	}

	private Bullet GetAmmo()
	{
		float timePassed = chargeTimer.TimePassed;
		Bullet bullet = normalBullet;
		Debug.Log("Shooting normal charge.");

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

		return bullet;
	}
}
