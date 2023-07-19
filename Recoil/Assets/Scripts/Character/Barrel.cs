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
	public void Init(IObjectPool objectPool)
	{
		this.objectPool = objectPool;
	}

	public void AimUsingMouseWorld(Vector3 mouseWorldPosition)
	{
		lookVector = mouseWorldPosition - transform.position;
		lookVector.y = 0;
		lookVector = lookVector.normalized;
		Quaternion lookRotation = Quaternion.LookRotation(lookVector, Vector3.up);
		transform.rotation = lookRotation;
	}

	public void AimUsingDirection(Vector3 lookDirection)
	{
		lookVector.x = lookDirection.x;
		lookVector.z = lookDirection.y;
		lookVector.y = 0;
		Quaternion lookRotation = Quaternion.LookRotation(lookVector, Vector3.up);
		transform.rotation      = Quaternion.Lerp(transform.rotation, lookRotation, aimSpeed * Time.deltaTime);
	}

	public void Idle(Vector3 tankForward)
    {
		Quaternion lookRotation = Quaternion.LookRotation(tankForward, Vector3.up);
		transform.rotation      = Quaternion.Lerp(transform.rotation, lookRotation, aimSpeed * Time.deltaTime);
	}

	public void FireShell()
	{
		GameObject shellInstance = objectPool.GetPooledObject(shell.GetType());
		shellInstance.transform.position = muzzle.position;
		shellInstance.transform.rotation = muzzle.rotation;
		shellInstance.GetComponent<Shell>().Launch();
	}
}
