using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the Ammo UI.
/// </summary>
public class UI_Ammo : MonoBehaviour
{
    [SerializeField] private int bulletCount;
    [SerializeField] private Image ammoX;
    [SerializeField] private Image fill;

    const int x = 19, y = 20;
    private Ammo playerAmmo;
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

    // Start is called before the first frame update
    private void Awake()
    {
        playerAmmo = FindObjectOfType<PlayerController>().GetComponent<Ammo>();
    }

    /// <summary>
    /// Changes tiling for the ammo bar.
    /// </summary>
    public void SetAmmoMaxSize()
	{
        Vector2 size = new Vector2(playerAmmo.MaxBulletCount * x, y);
        ammoX.rectTransform.sizeDelta = size;
        fill.rectTransform.sizeDelta = size;
    }

    /// <summary>
    /// Changes tiling for the ammo bar.
    /// </summary>
    public void SetAmmoCount()
	{
        fill.fillAmount = playerAmmo.CurrentBulletCount / playerAmmo.MaxBulletCount;
    }
}
