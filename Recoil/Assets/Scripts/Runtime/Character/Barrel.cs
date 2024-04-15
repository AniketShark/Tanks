using ObjectPooling;
using UnityEngine;

public class Barrel : MonoBehaviour
{
	public Transform barrelTransform;
	public Transform muzzle;
	public Shell shell;
	public float aimSpeed;
	private Vector3 _lookVector;
	private ObjectPool _objectPool;
	public void Init(ObjectPool objectPool)
	{
		this._objectPool = objectPool;
	}

	public void AimUsingDirection(Vector3 lookDirection)
	{
		if (lookDirection.magnitude > 0.125f)
		{
			lookDirection = lookDirection.normalized;
		}
		_lookVector.x = lookDirection.x;
		_lookVector.z = lookDirection.y;
		_lookVector.y = 0;
		Quaternion lookRotation = Quaternion.LookRotation(_lookVector, Vector3.up);
		barrelTransform.rotation = lookRotation;
	}

	public void FireShell()
	{
		GameObject shellInstance = _objectPool.GetPooledObject(typeof(Shell));
		shellInstance.transform.position = muzzle.position;
		shellInstance.transform.rotation = muzzle.rotation;
		shellInstance.GetComponent<Shell>().Launch();
	}
}
