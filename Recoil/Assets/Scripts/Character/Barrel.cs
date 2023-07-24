using ObjectPooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
	public Transform muzzle;
	public Shell shell;
	public float aimSpeed;
	private Vector3 lookVector;
	private IObjectPool objectPool;
	private float lookDirectionDiff = 0;
	public void Init(IObjectPool objectPool)
	{
		this.objectPool = objectPool;
	}

	public void AimUsingDirection(Vector3 lookDirection)
	{
		print("Look Direction magnitude : " + lookDirection.magnitude);
		if (lookDirection.magnitude < 0.99f)
			return;
		lookVector.x = lookDirection.x;
		lookVector.z = lookDirection.y;
		lookVector.y = 0;
		Quaternion lookRotation = Quaternion.LookRotation(lookVector, Vector3.up);
		transform.rotation = lookRotation;//Quaternion.Lerp(transform.rotation, lookRotation, aimSpeed * Time.deltaTime);
	}

	public void FireShell()
	{
		GameObject shellInstance = objectPool.GetPooledObject(shell.GetType());
		shellInstance.transform.position = muzzle.position;
		shellInstance.transform.rotation = muzzle.rotation;
		shellInstance.GetComponent<Shell>().Launch();
	}
}
