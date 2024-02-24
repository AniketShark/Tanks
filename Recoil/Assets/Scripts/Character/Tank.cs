using ObjectPooling;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class Tank : MonoBehaviour
{
	public Motor motor;
	public Barrel barrel;
	
	private Health _health; 
	private PlayerInput _input;
	private Vector3 _lastMouseMovePos;
	private Vector2 _moveDirection;
	public bool Move { get; set; }
	public Vector2 MoveDirection { get { return _moveDirection; } set { _moveDirection = value; } }
	private bool _aim;

	#region Pooling
	public ObjectPool PoolParent { get; set; }

	public void Init(ObjectPool objetctPool)
	{
		PoolParent = objetctPool;
		barrel.Init(PoolParent);
		_input = GetComponent<PlayerInput>();
		_health = GetComponent<Health>();
	}

	#endregion

	#region Unity Events

	private void Update()
	{
		if(Move)
		{
			motor.Move(_moveDirection);
		}
	}

	#endregion
}
