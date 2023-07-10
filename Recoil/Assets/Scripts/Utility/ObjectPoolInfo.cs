using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
