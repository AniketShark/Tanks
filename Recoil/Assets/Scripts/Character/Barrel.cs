using ObjectPooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
	public Transform barrelTransform;
	public Transform muzzle;
	public Shell shell;
	public float aimSpeed;
	private Vector3 lookVector;
	private ObjectPool objectPool;
	public void Init(ObjectPool objectPool)
	{
		this.objectPool = objectPool;
	}

	public void AimUsingDirection(Vector3 lookDirection)
	{
		if (lookDirection.magnitude > 0.125f)
		{
			lookDirection = lookDirection.normalized;
		}
		lookVector.x = lookDirection.x;
		lookVector.z = lookDirection.y;
		lookVector.y = 0;
		Quaternion lookRotation = Quaternion.LookRotation(lookVector, Vector3.up);
		barrelTransform.rotation = lookRotation;
	}

	public void FireShell()
	{
		GameObject shellInstance = objectPool.GetPooledObject(typeof(Shell));
		shellInstance.transform.position = muzzle.position;
		shellInstance.transform.rotation = muzzle.rotation;
		shellInstance.GetComponent<Shell>().Launch();
	}
}
