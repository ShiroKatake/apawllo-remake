using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Ammo : MonoBehaviour
{
    [SerializeField] private int bulletCount;
    [SerializeField] private Image ammoX;
    [SerializeField] private Image fill;

    const int x = 19, y = 20;

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
        Vector2 size = new Vector2(bulletCount * x, y);
        ammoX.rectTransform.sizeDelta = size;
        fill.rectTransform.sizeDelta = size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetAmmoSize()
	{
        Vector2 size = new Vector2(bulletCount * x, y);
        ammoX.rectTransform.sizeDelta = size;
        fill.rectTransform.sizeDelta = size;
    }
}
