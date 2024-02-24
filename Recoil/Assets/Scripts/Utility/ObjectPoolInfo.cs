using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
	public interface IPoolable
	{
		public void Init(ObjectPool objetctPool);
		ObjectPool PoolParent { get; set; }
		void Return();
	}

	[Serializable]
	public class PrefabInfo
	{
		public GameObject prefab;
		public int instances;
	}

	[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ObjectPoolInfo", order = 1)]
	public class ObjectPoolInfo : ScriptableObject
	{
		public List<PrefabInfo> pooledObjects;
	}
}
