using ObjectPooling;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class Tank : MonoBehaviour, IPoolable
{
	public Motor motor;
	public Barrel barrel;
	private Vector3 currentMousePos;
	private Vector3 lastMouseMovePos;
	private Vector2 moveDirection;
	private bool move;
	private bool aim;
	private ActionMaps actionMap;
	public IObjectPool PoolParent { get; set; }

	public void Init(IObjectPool objetctPool)
	{
		PoolParent = objetctPool;
		barrel.Init(PoolParent);
		actionMap = new ActionMaps();
		actionMap.Enable();
	}

	public void Destroy()
	{
		Return();
	}

	private void Update()
	{
		currentMousePos = actionMap.Tank.Aim.ReadValue<Vector2>();
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
		if(context.control.displayName == "Right Stick")
			barrel.AimUsingDirection(context.ReadValue<Vector2>());
		else
			barrel.AimUsingMouseWorld(CameraUtility.MouseToWorldHitPoint(context.ReadValue<Vector2>()));
	}

	public void Return()
	{
		gameObject.SetActive(false);
		PoolParent.ReturnToPool(gameObject);
	}
}
