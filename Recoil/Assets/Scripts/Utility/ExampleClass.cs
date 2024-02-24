using UnityEngine;

public class ExampleClass : MonoBehaviour
{
	private Camera _cam;

	void Start()
	{
		_cam = Camera.main;
	}

	void OnGUI()
	{
		Vector3 point = new Vector3();
		Event currentEvent = Event.current;
		Vector2 mousePos = new Vector2();

		// Get the mouse position from Event.
		// Note that the y position from Event is inverted.
		mousePos.x = currentEvent.mousePosition.x;
		mousePos.y = _cam.pixelHeight - currentEvent.mousePosition.y;

		point = _cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, _cam.farClipPlane));

		GUILayout.BeginArea(new Rect(20, 20, 250, 120));
		GUILayout.Label("Screen pixels: " + _cam.pixelWidth + ":" + _cam.pixelHeight);
		GUILayout.Label("Mouse position: " + mousePos);
		GUILayout.Label("World position: " + point.ToString("F3"));
		GUILayout.EndArea();
	}
}

