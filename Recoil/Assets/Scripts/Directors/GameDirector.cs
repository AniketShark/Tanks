using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ObjectPooling;
using UnityEngine.InputSystem;

public class GameDirector : MonoBehaviour
{
    // Object Pool
    [SerializeField]
    private Transform objectPoolContainer;
    [SerializeField]
    private ObjectPoolInfo objectPoolInfo;

    private IObjectPool objectPool;

    void Awake()
    {  
        //Creating Essential Objects
        objectPool = new ObjectPool();
        // Injecting Dependencies
        objectPool.Initialize(objectPoolContainer, objectPoolInfo);
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        Debug.LogFormat("Player Joined {0} Player Id : {1}", input.GetInstanceID(),input.name);
        input.GetComponent<Tank>().Init(objectPool);
    }

	public void OnPlayerLeft(PlayerInput input)
	{
		Debug.LogFormat("Player Joined {0} Player Id : {1}", input.GetInstanceID(), input.name);
	}

}
