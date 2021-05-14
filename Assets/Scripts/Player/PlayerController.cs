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
	private Shoot shoot;
	private Ammo ammo;
	#endregion

	/// <summary>
	/// Subscribe to input events.
	/// </summary>	
	private void Awake()
    {
		controls = new GameControls();

		shoot = GetComponent<Shoot>();
		ammo = GetComponent<Ammo>();

		controls.Gameplay.Shoot.performed += context => shoot.StartCharge();
		controls.Gameplay.Shoot.performed += context => ammo.OnShootPressed();
		controls.Gameplay.Shoot.canceled += context => shoot.OnShoot();
		controls.Gameplay.Shoot.canceled += context => ammo.OnShootReleased();

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
	/// Moves the player according to the vector context from the Move event.
	/// </summary>
	private void Update()
	{
		Vector2 m = new Vector2(move.x, move.y) * movementSpeed * Time.deltaTime;
		transform.Translate(m, Space.World);
	}
}
