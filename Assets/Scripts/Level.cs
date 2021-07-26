using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : SingletonBehaviour<Level>
{
    private int coinsCollected = 0;
    private PlayerController player;
    private Camera mainCamera;

    public event System.Action<int> CoinCollected;
    public event System.Action Reset;


    public Transform PlayerTranform => player ? player.transform : null;

    public Vector3 PlayerPosition => player ? player.transform.position : default(Vector3);

    public Vector3 CameraPosition => mainCamera ? mainCamera.transform.position : default(Vector3);

    private void Start()
    {
        mainCamera = Camera.main;
    }

    public void CollectCoin()
    {
        coinsCollected++;

        CoinCollected?.Invoke(coinsCollected);
    }


    public void SetPlayer(PlayerController _player)
    {
        this.player = _player;
    }

    public void ResetLevel()
    {
        Debug.Log("Level Reset");
        coinsCollected = 0;
        Reset?.Invoke();
    }
}
