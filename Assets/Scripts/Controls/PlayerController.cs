using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float movementSpeed = 10f;

	private GameControls controls;
	private Vector2 move;

	// Start is called before the first frame update
	private void Awake()
    {
		controls = new GameControls();

		controls.Gameplay.Shoot.performed += context => OnShoot(context);

		controls.Gameplay.Move.performed += context => move = context.ReadValue<Vector2>();
		controls.Gameplay.Move.canceled += context => move = Vector2.zero;
    }

	public void OnShoot(InputAction.CallbackContext context)
	{
		Debug.Log(context.phase.ToString());
	}

	private void Update()
	{
		Vector2 m = new Vector2(move.x, move.y) * movementSpeed * Time.deltaTime;
		transform.Translate(m, Space.World);
	}

	private void OnEnable()
	{
		controls.Gameplay.Enable();
	}
}
