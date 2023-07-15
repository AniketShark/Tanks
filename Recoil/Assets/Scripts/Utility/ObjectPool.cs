using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using static UnityEditor.Progress;

namespace ObjectPooling
{
	public interface IObjectPool
	{
		void Initialize(Transform containerTransform, ObjectPoolInfo poolInfoAsset);
		GameObject GetPooledObject(Type type);
		void ReturnToPool(GameObject go);
	}

	public class ObjectPool : IObjectPool
	{
		public  ObjectPoolInfo poolInfoAsset;
		private Dictionary<Type, Queue<GameObject>> pool;
		private Dictionary<Type, PrefabInfo> poolInfoDictionary;
		private Transform containerTransform;

		private GameObject CreatePoolObject(Type type)
		{
			var prefabInfo = poolInfoDictionary[type];
			var instance = GameObject.Instantiate(prefabInfo.prefab, containerTransform);
			instance.GetComponent<IPoolable>().Init(this);
			instance.SetActive(false);
			return instance;
		}
		public void Initialize(Transform containerTransform,ObjectPoolInfo poolInfoAsset)
		{
			this.poolInfoAsset = poolInfoAsset;
			this.containerTransform = containerTransform;

			if (pool == null)
				pool = new Dictionary<Type, Queue<GameObject>>();
			if (poolInfoDictionary == null)
				poolInfoDictionary = new Dictionary<Type, PrefabInfo>();

			for (int j = 0; j < poolInfoAsset.pooledObjects.Count; j++)
			{
				PrefabInfo item = poolInfoAsset.pooledObjects[j];
				Type t = item.prefab.GetComponent<IPoolable>().GetType();
				if (!poolInfoDictionary.ContainsKey(t))
				{
					poolInfoDictionary[t] = item;
					pool.Add(t, new Queue<GameObject>());
				}
				var queue = pool[t];
				for (int i = 0; i < item.instances; i++)
				{
					pool[t].Enqueue(CreatePoolObject(t));
				}
			}
		}
		public GameObject GetPooledObject(Type type)
		{
			if (pool.ContainsKey(type))
			{
				if (pool[type].Count > 0)
				{
					var go = pool[type].Dequeue();
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
			go.transform.SetParent(containerTransform);
			pool[t].Enqueue(go);
		}

	}
}
