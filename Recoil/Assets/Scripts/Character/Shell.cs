using UnityEngine;

public class Shell : MonoBehaviour
{
	public float speed;

	public void OnEnable()
	{
		Destroy(gameObject,2);
	}
	public void Move(Vector3 direction)
	{
	} 
	public void Update()
	{
		transform.position += transform.forward * speed * Time.deltaTime;
	}
}
