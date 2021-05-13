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
    
    [ReadOnly]
    [Tooltip("Current ammo count.")]
    [SerializeField] private float currentBulletCount;

    [Tooltip("Game event for when ammo count gets updated.")]
    [SerializeField] private GameEvent onAmmoUpdate;

    [Tooltip("Game event for when ammo capcity gets updated.")]
    [SerializeField] private GameEvent onMaxAmmoUpdate;
    #endregion

    #region Private Fields
    private Timer rechargeTimer;
    private bool canShoot;
    private bool canRefill;
    #endregion

    #region Public Properties
    public int MaxBulletCount { get => maxBulletCount; set => maxBulletCount = value; }
    public float CurrentBulletCount {
        get => currentBulletCount;
        set
        {
            currentBulletCount = value;
            onAmmoUpdate?.Invoke();
        }
    }
    public bool CanShoot {
        get
        {
            if (canShoot)
                return CurrentBulletCount >= 1;
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
        CurrentBulletCount = maxBulletCount;
        canShoot = true;
        canRefill = true;
        onMaxAmmoUpdate?.Invoke();
    }

    /// <summary>
    /// Refills ammo and changes state for ammo usage accordingly.
    /// </summary>
    private void Update()
    {
        if (CurrentBulletCount < 1)
            canShoot = false;

        if (rechargeTimer.TimerFinished)
            canRefill = true;

        if (canRefill)
        {
            if (CurrentBulletCount < maxBulletCount)
                CurrentBulletCount += maxBulletCount / rechargeTime * Time.deltaTime;
            else
                CurrentBulletCount = maxBulletCount;
        }

        if (CurrentBulletCount == maxBulletCount)
            canShoot = true;
    }

    /// <summary>
    /// Increases maximum ammo capacity.
    /// </summary>
    private void AddMaxBullet()
	{
        maxBulletCount++;
        onMaxAmmoUpdate?.Invoke();
    }

    /// <summary>
    /// Starts the recharge wait timer when "Shoot" button is released.
    /// </summary>
    public void StartWaitTimer()
	{
        if (canShoot && canRefill)
            canRefill = false;
        rechargeTimer.StartTimer();
    }
}
