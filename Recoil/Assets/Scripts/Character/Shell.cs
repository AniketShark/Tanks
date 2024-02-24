using UnityEngine;
using ObjectPooling;
public class Shell : MonoBehaviour, IPoolable
{
	public float speed;
	public float damage;
	public ObjectPool PoolParent { get; set; }
	public void Init(ObjectPool objetctPool)
	{
		PoolParent = objetctPool;
	}

	public void Launch()
	{
		gameObject.SetActive(true);
		Invoke("Return",2.0f);
	}

	public void Move(Vector3 direction)
	{
		transform.position += direction * speed * Time.deltaTime;
	}

	public void Update()
	{
		Move(transform.forward);
	}

	public void Return()
	{
		CancelInvoke("Return");
		gameObject.SetActive(false);
		PoolParent.ReturnToPool(gameObject);
	}

	void OnTriggerEnter(Collider collision) {

		Debug.LogFormat("{0} collided with {1} ",gameObject.name,collision.gameObject.name);
		var health = collision.gameObject.GetComponent<Health>();
		if(health)
			health.Damage(damage);
		Return();
	}

}
