using ObjectPooling;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class TankInput : MonoBehaviour
{
	private PlayerInput playerInput;
	private InputAction aimAction;
	private InputAction moveAction;
	private InputAction fireAction;
	private InputAction specialAction;

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
		moveAction.performed += OnMove; 
		fireAction.performed += OnFire;
		specialAction.performed += OnSpecial;
	}

	private void OnDisable()
	{
		aimAction.performed -= OnAim;
		moveAction.performed -= OnMove;
		fireAction.performed -= OnFire;
		specialAction.performed -= OnSpecial;
	}

	private void Update()
	{
		gameObject.GetComponent<Tank>().Move = moveAction.IsPressed();
		gameObject.GetComponent<Tank>().MoveDirection = moveAction.ReadValue<Vector2>();
	}

	public void OnSpecial(InputAction.CallbackContext context)
	{
		if (context.performed) gameObject.GetComponent<Barrel>().FireShell();
	}

	public void OnFire(InputAction.CallbackContext context)
	{
		if (context.performed) gameObject.GetComponent<Barrel>().FireShell();
	}

	public void OnMove(InputAction.CallbackContext context)
	{
		
	}

	public void OnAim(InputAction.CallbackContext context)
	{
		if (context.control.name == "rightStick")
		{
			gameObject.GetComponent<Barrel>().AimUsingDirection(context.ReadValue<Vector2>());
		}
		else
		{
			Vector3 lookVector = CameraUtility.MouseToWorldHitPoint(context.ReadValue<Vector2>());
			lookVector = lookVector - transform.position;
			lookVector.y = lookVector.z;
			lookVector = lookVector.normalized;
			gameObject.GetComponent<Barrel>().AimUsingDirection(lookVector);
		}
	}
}
