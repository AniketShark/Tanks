using ObjectPooling;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tank : MonoBehaviour, IPoolable
{
	public Motor motor;
	public Barrel barrel;
	private Vector3 mousePos;
	private Vector2 moveDirection;
	private bool move;
	public IObjectPool PoolParent { get; set; }

	public void Init(IObjectPool objetctPool)
	{
		PoolParent = objetctPool;
		barrel.Init(PoolParent);
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
		print("OnSpecial");
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
		mousePos = CameraUtility.MouseToWorldHitPoint(context.ReadValue<Vector2>());
		barrel.Aim(mousePos, transform.position);
	}

	public void Return()
	{
		gameObject.SetActive(false);
		PoolParent.ReturnToPool(gameObject);
	}
}
