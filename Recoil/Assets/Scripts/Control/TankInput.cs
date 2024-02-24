using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class TankInput : MonoBehaviour
{
	private PlayerInput playerInput;
	private InputAction aimAction;
	private InputAction moveAction;
	private InputAction fireAction;
	private InputAction specialAction;
	private Motor motor;
	private Tank tank;
	private Barrel barrel;

	public void OnEnable()
	{
		if (playerInput == null)
		{
			playerInput = GetComponent<PlayerInput>();
			aimAction = playerInput.actions["Aim"];
			moveAction = playerInput.actions["Move"];
			fireAction = playerInput.actions["Fire"];
			specialAction = playerInput.actions["Special"];
		}

		aimAction.performed += OnAim; 
		fireAction.performed += OnFire;
		specialAction.performed += OnSpecial;

		barrel = gameObject.GetComponent<Barrel>();
		motor  = gameObject.GetComponent<Motor>();
		tank   = gameObject.GetComponent<Tank>();

	}

	private void OnDisable()
	{
		aimAction.performed -= OnAim;
		fireAction.performed -= OnFire;
		specialAction.performed -= OnSpecial;
	}

	private void Update()
	{
		tank.Move = moveAction.IsPressed();
		tank.MoveDirection = moveAction.ReadValue<Vector2>();
	}

	public void OnSpecial(InputAction.CallbackContext context)
	{
		if (context.performed) barrel.FireShell();
	}

	public void OnFire(InputAction.CallbackContext context)
	{
		if (context.performed) barrel.FireShell();
	}

	public void OnAim(InputAction.CallbackContext context)
	{
		if (context.control.name == "rightStick")
		{
			barrel.AimUsingDirection(context.ReadValue<Vector2>());
		}
		else
		{
			Vector3 lookVector = CameraUtility.MouseToWorldHitPoint(context.ReadValue<Vector2>());
			lookVector = lookVector - transform.position;
			lookVector.y = lookVector.z;
			lookVector = lookVector.normalized;
			barrel.AimUsingDirection(lookVector);
		}
	}
}
