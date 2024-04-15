using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using Game.Runtime.Network;


[RequireComponent(typeof(PlayerInput))]
public class TankInput : MonoBehaviour
{
	private PlayerInput _playerInput;
	private InputAction _aimAction;
	private InputAction _moveAction;
	private InputAction _fireAction;
	private InputAction _specialAction;
	private Motor _motor;
	private Tank _tank;
	private Barrel _barrel;

	public void OnEnable()
	{
		if (_playerInput == null)
		{
			_playerInput = GetComponent<PlayerInput>();
			_aimAction = _playerInput.actions["Aim"];
			_moveAction = _playerInput.actions["Move"];
			_fireAction = _playerInput.actions["Fire"];
			_specialAction = _playerInput.actions["Special"];
		}

		_aimAction.performed += OnAim;
		_fireAction.performed += OnFire;
		_specialAction.performed += OnSpecial;

		_barrel = gameObject.GetComponent<Barrel>();
		_motor  = gameObject.GetComponent<Motor>();
		_tank   = gameObject.GetComponent<Tank>();
	}

	private void OnDisable()
	{
		_aimAction.performed -= OnAim;
		_fireAction.performed -= OnFire;
		_specialAction.performed -= OnSpecial;
	}

	private void Update()
	{
		_tank.Move = _moveAction.IsPressed();
		_tank.MoveDirection = _moveAction.ReadValue<Vector2>();
	}

	public void OnSpecial(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			_barrel.FireShell();
			Client.Instance.SendInputDataToServer(NetworkMessegeType.Special);
		}
	}

	public void OnFire(InputAction.CallbackContext context)
	{
		if (context.performed)
		{
			Client.Instance.SendInputDataToServer(NetworkMessegeType.Fire);
			_barrel.FireShell();
		}
	}

	public void OnAim(InputAction.CallbackContext context)
	{
		if (context.control.name == "rightStick")
		{
			_barrel.AimUsingDirection(context.ReadValue<Vector2>());
			Client.Instance.SendInputDataToServer(NetworkMessegeType.Aim);
		}
		else
		{
			Vector3 lookVector = CameraUtility.MouseToWorldHitPoint(context.ReadValue<Vector2>());
			lookVector = lookVector - transform.position;
			lookVector.y = lookVector.z;
			lookVector = lookVector.normalized;
			_barrel.AimUsingDirection(lookVector);
		}
	}
}
