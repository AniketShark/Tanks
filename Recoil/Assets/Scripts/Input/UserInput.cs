using ObjectPooling;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
	private ActionMaps actionMaps;
	
	void Start()
	{
		
	}
	void OnEnable()
    {
		if (actionMaps == null)
		{
			actionMaps = new ActionMaps();
		}
		actionMaps.Enable();
	}

	private void OnSpecial(InputAction.CallbackContext context)
	{
	}

	private void OnFire(InputAction.CallbackContext context)
	{
	}
}
