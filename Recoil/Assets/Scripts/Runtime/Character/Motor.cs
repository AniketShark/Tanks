using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
	public float speed;
	public float rotationalSpeed;
	private Vector3 _moveDirection = Vector3.forward;
	private Vector3 _lookDirection = Vector3.forward;

	public void Move(Vector2 direction)
	{
		_moveDirection.x = direction.x;
		_moveDirection.z = direction.y;
		Quaternion rotation = Quaternion.LookRotation(_moveDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationalSpeed * Time.deltaTime);
		transform.position += _lookDirection * speed * Time.deltaTime * _moveDirection.magnitude;
		_lookDirection = transform.forward;
	}

}
