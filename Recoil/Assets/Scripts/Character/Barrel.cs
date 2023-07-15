using ObjectPooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
	public Transform muzzle;
	public Shell shell;
	public Vector3 lookVector;
	public float aimSpeed;
	private IObjectPool objectPool;

	public void Init(IObjectPool objectPool)
	{
		this.objectPool = objectPool;
	}

	public void Aim(Vector3 mouseWorldPosition, Vector3 tankPosition)
	{
		lookVector = mouseWorldPosition - tankPosition;
		lookVector.y = 0;
		lookVector = lookVector.normalized;
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
