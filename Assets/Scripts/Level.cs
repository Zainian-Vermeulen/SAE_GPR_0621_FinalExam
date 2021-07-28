using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : SingletonBehaviour<Level>
{
    private int _coinsCollected = 0;
    private PlayerController _player;
    private Camera _mainCamera;

    public event System.Action<int> CoinCollected;
    public event System.Action Reset;


    public Transform PlayerTranform => _player ? _player.transform : null;

    public Vector3 PlayerPosition => _player ? _player.transform.position : default(Vector3);

    public Vector3 CameraPosition => _mainCamera ? _mainCamera.transform.position : default(Vector3);

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    public void CollectCoin()
    {
        _coinsCollected++;

        CoinCollected?.Invoke(_coinsCollected);
    }


    public void SetPlayer(PlayerController _player)
    {
        this._player = _player;
    }

    public void ResetLevel()
    {
        Debug.Log("Level Reset");
        _coinsCollected = 0;
        Reset?.Invoke();
    }
}
