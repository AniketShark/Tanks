using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
	public Transform muzzle;
	public Shell shell;
	public Vector3 lookVector;
	public float aimSpeed;

	public void Aim(Vector3 mouseWorldPosition, Vector3 tankPosition)
	{
		lookVector = mouseWorldPosition - tankPosition;
		lookVector.y = 0;
		lookVector = lookVector.normalized;
		Quaternion lookRotation = Quaternion.LookRotation(lookVector, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, aimSpeed * Time.deltaTime);
	}

	public void FireShell()
	{
		Shell shellInstance = Instantiate<Shell>(shell,muzzle.position,muzzle.rotation);
	}
}
