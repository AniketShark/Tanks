using UnityEngine;
using ObjectPooling;
using UnityEngine.InputSystem;
using Game.Runtime.Network;

public class GameDirector : MonoBehaviour
{
    // Object Pool
    [SerializeField]
    private Transform _objectPoolContainer;
    [SerializeField]
    private ObjectPoolInfo _objectPoolInfo;
    private ObjectPool _objectPool;
    
    // Player Management
    [SerializeField]
    private GameObject _playerPrefab;
    private PlayerManager _playerManager;
	private PlayerInputManager _playerInputManager;

    void Awake()
    {
        //Creating Essential Objects
        _objectPool = new ObjectPool();
        _playerManager = new PlayerManager();
	    _playerInputManager = GetComponent<PlayerInputManager>();
        _playerInputManager.playerPrefab = _playerPrefab;
        
        // Injecting Dependencies
		_objectPool.Initialize(_objectPoolContainer, _objectPoolInfo);
        _playerManager.Initialize(_playerInputManager, _objectPool);

        Invoke("ConnectToServer", 2);
    }

    private void ConnectToServer()
    {
        Debug.Log("Connect To Game Server ");
        Client.Instance.ConnectToServer();
        CancelInvoke();
    }

    private void OnDestroy()
	{
		_playerManager.Reset();
	}
}
