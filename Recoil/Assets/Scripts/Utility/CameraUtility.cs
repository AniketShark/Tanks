using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public static class CameraUtility 
{
	public static Vector3 MouseToWorldPosition(Vector3 mousePos,float distanceFromCamera)
	{
		// Get the mouse position from Event.
		// Note that the y position from Event is inverted.
		mousePos.y = Camera.main.pixelHeight - mousePos.y;
		mousePos.z = distanceFromCamera;
		return Camera.main.ScreenToWorldPoint(mousePos);
	}

	public static Vector3 MouseToWorldHitPoint(Vector3 mousePos)
	{
		RaycastHit hitinfo;
		if(Physics.Raycast(Camera.main.ScreenPointToRay(mousePos),out hitinfo,Camera.main.farClipPlane,LayerMask.GetMask("Raycast")))
		{
			return hitinfo.point;
		}
		return Vector3.zero;
	}
}
