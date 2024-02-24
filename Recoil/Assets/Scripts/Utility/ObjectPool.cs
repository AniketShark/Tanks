using System;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
	public interface IObjectPool
	{
		void Initialize(Transform containerTransform, ObjectPoolInfo poolInfoAsset);
		GameObject GetPooledObject(Type type);
		void ReturnToPool(GameObject go);
	}

	public class ObjectPool
	{
		public  ObjectPoolInfo poolInfoAsset;
		private Dictionary<Type, Queue<GameObject>> _pool;
		private Dictionary<Type, PrefabInfo> _poolInfoDictionary;
		private Transform _containerTransform;

		private GameObject CreatePoolObject(Type type)
		{
			var prefabInfo = _poolInfoDictionary[type];
			var instance = GameObject.Instantiate(prefabInfo.prefab, _containerTransform);
			instance.GetComponent<IPoolable>().Init(this);
			instance.SetActive(false);
			return instance;
		}
		public void Initialize(Transform containerTransform,ObjectPoolInfo poolInfoAsset)
		{
			this.poolInfoAsset = poolInfoAsset;
			this._containerTransform = containerTransform;

			if (_pool == null)
				_pool = new Dictionary<Type, Queue<GameObject>>();
			if (_poolInfoDictionary == null)
				_poolInfoDictionary = new Dictionary<Type, PrefabInfo>();

			for (int j = 0; j < poolInfoAsset.pooledObjects.Count; j++)
			{
				PrefabInfo item = poolInfoAsset.pooledObjects[j];
				Type t = item.prefab.GetComponent<IPoolable>().GetType();
				if (!_poolInfoDictionary.ContainsKey(t))
				{
					_poolInfoDictionary[t] = item;
					_pool.Add(t, new Queue<GameObject>());
				}
				var queue = _pool[t];
				for (int i = 0; i < item.instances; i++)
				{
					var go = CreatePoolObject(t);
					go.name += i;
					_pool[t].Enqueue(go);
				}
			}
		}
		public GameObject GetPooledObject(Type type)
		{
			if (_pool.ContainsKey(type))
			{
				if (_pool[type].Count > 0)
				{
					var go = _pool[type].Dequeue();
					go.transform.SetParent(null);
					return go;
				}
				else
				{
					var go = CreatePoolObject(type);
					go.transform.SetParent(null);
					return go;
				}
			}
			Debug.LogErrorFormat("{0} type is not in Pooled Objects", type);
			return null;
		}

		public void ReturnToPool(GameObject go)
		{
			go.SetActive(false);
			Type t = go.GetComponent<IPoolable>().GetType();
			go.transform.SetParent(_containerTransform);
			_pool[t].Enqueue(go);
		}

	}
}
