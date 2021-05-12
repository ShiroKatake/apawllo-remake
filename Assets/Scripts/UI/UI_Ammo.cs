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

    /// <summary>
    /// Updates UI whenever bullet count is changed.
    /// </summary>
    private int BulletCount { 
        get => bulletCount;
        set
        {
            bulletCount = value;
            SetAmmoSize();
        }
    }

	// Start is called before the first frame update
	void Start()
    {
        SetAmmoSize();
    }

    /// <summary>
    /// Changes tiling for the ammo bar.
    /// </summary>
    private void SetAmmoSize()
	{
        Vector2 size = new Vector2(bulletCount * x, y);
        ammoX.rectTransform.sizeDelta = size;
        fill.rectTransform.sizeDelta = size;
    }
}
