using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private Animator _animator;
    [SerializeField] private AnimationClip _clip;
    [SerializeField] LevelUI _levelUI;

    private void Start()
    {
       // level = GetComponent<Level>();
        _level.Reset += SetCoinsActive;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform == Level.Instance.PlayerTranform)
        {
            Debug.Log("Collected Coin");
            Level.Instance.CollectCoin();

            //_animator.SetBool("isColleced", true);
           _animator.Play($"{_clip}");
            WaitForAnimation();
            _levelUI.OnCoinCollected(1);
           // yield return new WaitForSeconds(_clip.length);
            gameObject.SetActive(false);
        }   
    }

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(_clip.length);
    }

    private void SetCoinsActive()
    {
        gameObject.SetActive(true);
    }

}
