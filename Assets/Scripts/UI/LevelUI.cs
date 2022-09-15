using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] GameObject[] coins;
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationClip _animationClip;


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
        StartCoroutine(FlyingCoin());
        if (coins.Length >= count)
        {
            coins[count - 1].SetActive(true);
        }
    }

    private IEnumerator FlyingCoin()
    {
       // _animator.SetBool("isFlying",  true);
       
        _animator.Play(_animationClip.name);
        
        //yield break;
        yield return new WaitForSeconds(_animationClip.length);
        
    }
}
