using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
	#region Serialized Fields
    [SerializeField] private int maxBulletCount;
    [SerializeField] private float currentBulletCount;
    [SerializeField] private float rechargeTime;
    [SerializeField] private float cooldownTime;
	#endregion

	#region Private Fields
    private bool canShoot;
    private bool canRefill;
    private Timer cooldownTimer;
	#endregion

	public float BulletCount { get => currentBulletCount; set => currentBulletCount = value; }
    public bool CanShoot {
        get
        {
            if (canShoot)
                return currentBulletCount >= 1;
            return false;
        }
    }

	private void Awake()
	{
        cooldownTimer = Timer.CreateComponent(gameObject, cooldownTime);
	}

	// Start is called before the first frame update
	private void Start()
    {
        currentBulletCount = 0;
        canShoot = true;
        canRefill = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (currentBulletCount < 1)
            canShoot = false;

        if (canRefill)
        {
            if (currentBulletCount < maxBulletCount)
                currentBulletCount += maxBulletCount / rechargeTime * Time.deltaTime;
            else
                currentBulletCount = maxBulletCount;
        }

        if (currentBulletCount == maxBulletCount)
            canShoot = true;
    }

    private void AddMaxBullet()
	{
        maxBulletCount++;
	}

    public void StartCooldown()
	{
        cooldownTimer.StartTimer();
    }
}
