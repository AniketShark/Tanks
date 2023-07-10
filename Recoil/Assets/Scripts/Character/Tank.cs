using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tank : MonoBehaviour
{
	public Motor motor;
	public Barrel barrel;
	public ActionMaps actionMaps;

	private Vector3 mousePos;
	private void Start()
	{
		print("Start");
	}

	public void OnEnable()
	{
		if(actionMaps == null)
		{
			actionMaps = new ActionMaps();
		}
		print("OnEnable");
		actionMaps.Tank.Fire.performed += OnFire;
		actionMaps.Tank.Special.performed += OnSpecial;
		actionMaps.Enable();
	}

	private void OnSpecial(InputAction.CallbackContext context)
	{
		print("special");
	}

	private void OnFire(InputAction.CallbackContext context)
	{
		print("fire");
		barrel.FireShell();
	}

	private void Update()
	{
		if (actionMaps.Tank.Move.inProgress)
		{
			motor.Move(actionMaps.Tank.Move.ReadValue<Vector2>());
		}
		if (actionMaps.Tank.Aim.inProgress)
		{
			mousePos = actionMaps.Tank.Aim.ReadValue<Vector2>();
			mousePos = CameraUtility.MouseToWorldHitPoint(mousePos);
			barrel.Aim(mousePos,transform.position);
		} 
	}


	private void OnDisable()
	{
		actionMaps.Tank.Fire.performed -= OnFire;
		actionMaps.Tank.Special.performed -= OnSpecial;
		actionMaps.Disable();
	}
}
