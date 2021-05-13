using UnityEngine;

/// <summary>
/// Ammo management.
/// </summary>
public class Ammo : MonoBehaviour
{
    #region Serialized Fields
    [Tooltip("Max ammo capacity.")]
    [SerializeField] private int maxBulletCount;

    [Tooltip("Time it takes to recharge to max ammo.")]
    [SerializeField] private float rechargeTime;

    [Tooltip("How long the player needs to wait after releasing the 'Shoot' button to start recharging ammo.")]
    [SerializeField] private float rechargeWaitTime;
    #endregion

    #region Private Fields
    private Timer rechargeTimer;
    private float currentBulletCount;
    private bool canShoot;
    private bool canRefill;
    #endregion

    #region Public Properties
    public float BulletCount { get => currentBulletCount; set => currentBulletCount = value; }
    public bool CanShoot {
        get
        {
            if (canShoot)
                return currentBulletCount >= 1;
            return false;
        }
    }
	#endregion

	/// <summary>
	/// Creates and initializes the recharge wait timer,
	/// </summary>
	private void Awake()
	{
        rechargeTimer = Timer.CreateComponent(gameObject, "Recharge Wait Timer", rechargeWaitTime);
	}

    /// <summary>
    /// Initializes ammo attributes.
    /// </summary>
    private void Start()
    {
        currentBulletCount = maxBulletCount;
        canShoot = true;
        canRefill = true;
    }

    /// <summary>
    /// Refills ammo and changes state for ammo usage accordingly.
    /// </summary>
    private void Update()
    {
        if (currentBulletCount < 1)
            canShoot = false;

        if (rechargeTimer.TimerFinished)
            canRefill = true;

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

    /// <summary>
    /// Increases maximum ammo capacity.
    /// </summary>
    private void AddMaxBullet()
	{
        maxBulletCount++;
	}

    /// <summary>
    /// Starts the recharge wait timer when "Shoot" button is released.
    /// </summary>
    public void StartWaterTimer()
	{
        if (canRefill)
            canRefill = false;
        rechargeTimer.StartTimer();
    }
}
