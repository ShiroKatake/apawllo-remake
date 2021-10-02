using UnityEngine;

/// <summary>
/// Ammo management.
/// </summary>
public class Ammo : MonoBehaviour
{
    #region Serialized Fields
    [Header("Ammo Stats")]
    [SerializeField] private int maxBulletCount;

    [ReadOnly]
    [SerializeField] private float currentBulletCount;

    [Tooltip("The time it takes to recharge to max ammo.")]
    [SerializeField] private float rechargeTime;

    [Tooltip("The time the player has to not shoot for the ammo to starts refill automatically.")]
    [SerializeField] private float rechargeCooldown;
    

    [Header("Game Events")]
    [Tooltip("Game event for when ammo count gets updated.")]
    [SerializeField] private GameEvent onAmmoUpdate;

    [Tooltip("Game event for when ammo capcity gets updated.")]
    [SerializeField] private GameEvent onMaxAmmoUpdate;

    [Tooltip("Game event for when player is able to shoot or not.")]
    [SerializeField] private GameEvent onAmmoStateChange;
    #endregion

    #region Private Fields
    private Timer rechargeTimer;
    private bool canShoot;
    private bool canRefill;
    private bool isShootPressed;
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
        set
		{
            canShoot = value;
            onAmmoStateChange?.Invoke();
        }
    }
	#endregion

	/// <summary>
	/// Creates and initializes the recharge wait timer,
	/// </summary>
	private void Awake()
	{
        rechargeTimer = Timer.CreateComponent(gameObject, "Recharge Wait Timer", rechargeCooldown);
	}

    /// <summary>
    /// Initializes ammo attributes.
    /// </summary>
    private void Start()
    {
        CurrentBulletCount = maxBulletCount;
        CanShoot = true;
        canRefill = true;
        onMaxAmmoUpdate?.Invoke();
    }

    /// <summary>
    /// Refills ammo and changes state for ammo usage accordingly.
    /// </summary>
    private void Update()
    {
        if (CurrentBulletCount < 1)
            CanShoot = false;

        if (rechargeTimer.TimerFinished && !isShootPressed)
            canRefill = true;

        if (canRefill)
        {
            if (CurrentBulletCount < maxBulletCount)
                CurrentBulletCount += maxBulletCount / rechargeTime * Time.deltaTime;
            else
                CurrentBulletCount = maxBulletCount;
        }

        if (CurrentBulletCount == maxBulletCount)
            CanShoot = true;
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
    /// Pauses refilling of ammo.
    /// </summary>
    public void OnShootPressed()
	{
        isShootPressed = true;
        if (canRefill)
            canRefill = false;
    }

    /// <summary>
    /// Starts the recharge wait timer when "Shoot" button is released.
    /// </summary>
    public void OnShootReleased()
	{
        isShootPressed = false;
        if (CanShoot && canRefill)
            canRefill = false;
        rechargeTimer.StartTimer();
    }

    /// <summary>
    /// Use ammo depending on bullet type.
    /// </summary>
    public void ConsumeBullet(Bullet bullet)
	{
        CurrentBulletCount -= bullet.Cost;
    }
}
