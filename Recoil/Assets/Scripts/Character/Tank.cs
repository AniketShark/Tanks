using ObjectPooling;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class Tank : MonoBehaviour, IPoolable
{
	public Motor motor;
	public Barrel barrel;
	private Health health; 

	private PlayerInput input;
	private Vector3 currentMousePos;
	private Vector3 lastMouseMovePos;
	private Vector2 moveDirection;
	public bool Move { get; set; }
	public Vector2 MoveDirection { get { return moveDirection; } set { moveDirection = value; } }
	private bool aim;

	#region Pooling
	public IObjectPool PoolParent { get; set; }

	public void Init(IObjectPool objetctPool)
	{
		PoolParent = objetctPool;
		barrel.Init(PoolParent);
		input = GetComponent<PlayerInput>();
		health = GetComponent<Health>();
	}

	public void Return()
	{
		gameObject.SetActive(false);
		PoolParent.ReturnToPool(gameObject);
	}

	#endregion

	#region Unity Events

	private void Update()
	{
		if(Move)
		{
			motor.Move(moveDirection);
		}
	}

	#endregion
}
