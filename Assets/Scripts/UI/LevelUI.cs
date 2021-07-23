using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] GameObject[] coins;


    private void Start()
    {
        Level.Instance.CoinCollected += OnCoinCollected;
        Level.Instance.Reset += OnReset;

        OnReset();
    }

    private void OnReset()
    {
        for (int i = 0; i < coins.Length; i++)
        {
            coins[i].SetActive(false);
        }
    }

    private void OnCoinCollected(int count)
    {
        if (coins.Length >= count)
            coins[count - 1].SetActive(true);
    }
}
