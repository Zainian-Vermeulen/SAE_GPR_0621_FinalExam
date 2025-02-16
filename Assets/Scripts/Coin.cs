using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Level _level;
    private void Start()
    {   
        _level.Reset += SetCoinsActive;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform == Level.Instance.PlayerTranform)
        {
            Debug.Log("Collected Coin");
            Level.Instance.CollectCoin();

            gameObject.SetActive(false);
        }   
    }

    private void SetCoinsActive()
    {
        gameObject.SetActive(true);
    }

}
