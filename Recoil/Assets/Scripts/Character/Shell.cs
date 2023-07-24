using UnityEngine;
using ObjectPooling;
public class Shell : MonoBehaviour, IPoolable
{
	public float speed;
	public IObjectPool PoolParent { get; set; }
	public void Init(IObjectPool objetctPool)
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

	

}
