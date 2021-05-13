using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the Ammo UI.
/// </summary>
public class UI_Ammo : MonoBehaviour
{
    #region Serialized Fields
    [SerializeField] private int bulletCount;
    [SerializeField] private Image ammoX;
    [SerializeField] private Image fill;
    [SerializeField] private Color disabledColor;
	#endregion

    #region Private Fields
	const int X = 19, Y = 20;
    private Ammo playerAmmo;
    private Color enabledColor;
	#endregion

	/// <summary>
	/// Updates UI whenever bullet count is changed.
	/// </summary>
	private int BulletCount { 
        get => bulletCount;
        set
        {
            bulletCount = value;
            SetAmmoCount();
        }
    }

    /// <summary>
    /// Gets ammo reference from player object.
    /// </summary>
    // Start is called before the first frame update
    private void Awake()
    {
        playerAmmo = FindObjectOfType<PlayerController>().GetComponent<Ammo>();
    }

    /// <summary>
    /// Initializes fill color.
    /// </summary>
    private void Start()
	{
        enabledColor = fill.color;
    }

	/// <summary>
	/// Changes max ammo count.
	/// </summary>
	public void SetAmmoMaxCount()
	{
        Vector2 size = new Vector2(playerAmmo.MaxBulletCount * X, Y);
        ammoX.rectTransform.sizeDelta = size;
        fill.rectTransform.sizeDelta = size;
    }

    /// <summary>
    /// Changes ammo count.
    /// </summary>
    public void SetAmmoCount()
	{
        fill.fillAmount = playerAmmo.CurrentBulletCount / playerAmmo.MaxBulletCount;
    }

    /// <summary>
    /// Changes ammo state.
    /// </summary>
    public void SetAmmoState()
	{
        fill.color = playerAmmo.CanShoot ? enabledColor : disabledColor;
    }
}
