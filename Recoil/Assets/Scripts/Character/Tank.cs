using ObjectPooling;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class Tank : MonoBehaviour, IPoolable
{
	public Motor motor;
	public Barrel barrel;
	public PlayerInput input;

	private Vector3 currentMousePos;
	private Vector3 lastMouseMovePos;
	private Vector2 moveDirection;
	private bool move;
	private bool aim;

	public IObjectPool PoolParent { get; set; }

	public void Init(IObjectPool objetctPool)
	{
		PoolParent = objetctPool;
		barrel.Init(PoolParent);
		input = GetComponent<PlayerInput>();
	}

	public void Destroy()
	{
		Return();
	}

	private void Update()
	{
		if(move)
		{
			motor.Move(moveDirection);
		}
	}

	public void OnSpecial(InputAction.CallbackContext context)
	{
	}

	public void OnFire(InputAction.CallbackContext context)
	{
		if(context.performed) barrel.FireShell();
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		move = context.performed;
		moveDirection = context.ReadValue<Vector2>();
	}

	public void OnAim(InputAction.CallbackContext context)
	{

		if(context.control.name == "rightStick")
		{
			print(context.ReadValue<Vector2>());
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

	public void Return()
	{
		gameObject.SetActive(false);
		PoolParent.ReturnToPool(gameObject);
	}
}
