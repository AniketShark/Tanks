using ObjectPooling;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager
{
    private readonly Dictionary<InputDevice,GameObject> _players = new Dictionary<InputDevice, GameObject>();
    private PlayerInputManager _playerInputManager;
    private ObjectPool _gameObjectPool;
    
    public void Initialize(PlayerInputManager inputManager,ObjectPool objectPool)
    {
		InputSystem.onDeviceChange += OnDeviceChange;
		_playerInputManager = inputManager;
        _gameObjectPool = objectPool;

		foreach (var device in InputSystem.devices)
		{
			if (device is Gamepad || device is Keyboard)
			{
				OnDeviceChange(device, InputDeviceChange.Added);
				Debug.Log(string.Format("Input Device Type : {0}", device.ToString()));
			}
		}
	}

	private void OnPlayerJoined(PlayerInput input)
	{
        Debug.Log("onPlayerJoined");
	}

	private void OnPlayerLeft(PlayerInput input)
	{
        Debug.Log("onPlayerLeft");
	}

	public void Reset()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;   
    }

	private void OnDeviceChange(InputDevice device, InputDeviceChange change)
	{
        if(change == InputDeviceChange.Enabled || change == InputDeviceChange.Added)
        {
            PlayerInput player =  _playerInputManager.JoinPlayer(_players.Count, -1, null, device);
            if(player != null)
            {
                player.GetComponent<Tank>().Init(_gameObjectPool);
                _players.Add(device,player.gameObject);
            }
        }
        else if(change == InputDeviceChange.Removed)
        {
            GameObject player = _players[device];
            _players.Remove(device);
            GameObject.Destroy(player);
		}
	}
}
