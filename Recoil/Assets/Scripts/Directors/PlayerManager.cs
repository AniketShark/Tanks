using ObjectPooling;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class PlayerManager
{
    private Dictionary<InputDevice,GameObject> players = new Dictionary<InputDevice, GameObject>();
    private PlayerInputManager playerInputManager;
    private ObjectPool gameObjectPool;
    
    public void Initialize(PlayerInputManager inputManager,ObjectPool objectPool)
    {
        InputSystem.onDeviceChange += OnDeviceChange;
		playerInputManager = inputManager;
        gameObjectPool = objectPool;
	}

	private void onPlayerJoined(PlayerInput input)
	{
        Debug.Log("onPlayerJoined");
	}

	private void onPlayerLeft(PlayerInput input)
	{
        Debug.Log("onPlayerLeft");
	}

	public void Reset()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;   
    }

	private void OnDeviceChange(InputDevice device, InputDeviceChange change)
	{
        Debug.LogFormat("device Joined {0} input device chnage {1}", device.displayName, change.ToString());
        if(change == InputDeviceChange.Enabled || change == InputDeviceChange.Added)
        {
            PlayerInput player =  playerInputManager.JoinPlayer(players.Count, -1, null, device);
            if(player != null)
            {
                player.GetComponent<Tank>().Init(gameObjectPool);
                players.Add(device,player.gameObject);

            }
        }
        else if(change == InputDeviceChange.Removed)
        {
            GameObject player = players[device];
            players.Remove(device);
            GameObject.Destroy(player);
		}
	}
}
