using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor : MonoBehaviour
{
	public float speed;
	public float rotationalSpeed;
	private Vector3 moveDirection = Vector3.forward;
	private Vector3 lookDirection = Vector3.forward;


	public void Move(Vector2 direction)
	{
		moveDirection.x = direction.x;
		moveDirection.z = direction.y;
		Quaternion rotation = Quaternion.LookRotation(moveDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationalSpeed * Time.deltaTime);
		transform.position += lookDirection * speed * Time.deltaTime * moveDirection.magnitude;
		lookDirection = transform.forward;
	}
}
