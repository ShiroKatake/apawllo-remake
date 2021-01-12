using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private float speed;

	private void Update()
	{
		Vector2 m = transform.right * speed * Time.deltaTime;
		transform.Translate(m, Space.World);
	}
}
