using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Tank : MonoBehaviour
{
	public Motor motor;
	public Barrel barrel;
	public Shell shell;
	public ActionMaps actionMaps;

	private Vector3 mousePos;
	private void Start()
	{
		actionMaps = new ActionMaps();
		actionMaps.Enable();
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
			shell.transform.position = mousePos;
			barrel.Aim(mousePos,transform.position);
		} 
	}


	private void OnDisable()
	{
		actionMaps.Disable();
	}
}
