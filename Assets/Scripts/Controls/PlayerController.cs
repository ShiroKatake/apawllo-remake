using UnityEngine;

/// <summary>
/// Responses to player inputs.
/// </summary>
public class PlayerController : MonoBehaviour
{
	#region Serialized Fields
	[SerializeField] private float movementSpeed = 10f;
	#endregion

	#region Private Fields
	private GameControls controls;
	private Vector2 move;
	#endregion

	/// <summary>
	/// Subscribe to input events.
	/// </summary>	
	private void Awake()
    {
		controls = new GameControls();

		controls.Gameplay.Shoot.performed += context => OnShoot();

		controls.Gameplay.Move.performed += context => move = context.ReadValue<Vector2>();
		controls.Gameplay.Move.canceled += context => move = Vector2.zero;
    }

	/// <summary>
	/// Enables the controls when the controller script is enabled.
	/// </summary>	
	private void OnEnable()
	{
		controls.Gameplay.Enable();
	}

	/// <summary>
	/// Spawns a bullet when the Shoot event is triggered.
	/// </summary>
	public void OnShoot()
	{
		ObjectPooler.Instance.SpawnFromPool(Pools.ApawlloBullet, transform.position, transform.rotation);
	}

	/// <summary>
	/// Moves the player according to the vector context from the Move event.
	/// </summary>
	private void Update()
	{
		Vector2 m = new Vector2(move.x, move.y) * movementSpeed * Time.deltaTime;
		transform.Translate(m, Space.World);
	}
}
