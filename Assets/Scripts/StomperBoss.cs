using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StomperBoss : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    Vector3 viewPos;

    [SerializeField] private GameObject _eyeLidLeft, _eyeLidRight, _bossNameGO;
    private bool isAwake = false;

    [SerializeField] private Animator _animator;
    [SerializeField] private float _waitTime;
    
    [Range(0, 1)]
    [SerializeField]private float _animationOffset;

    [SerializeField] private Transform _targetTransform;


    // Start is called before the first frame update
    void Start()
    {
        _eyeLidLeft.SetActive(true);
        _eyeLidRight.SetActive(true);
        _bossNameGO.SetActive(false);

        
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CanSeeBoss();
        
    }

    private void CanSeeBoss()
    {
        viewPos = _camera.WorldToViewportPoint(transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            isAwake = true;
        }
        else
            isAwake = false;

        BossWakeUp();
    }

    private void BossWakeUp()
    {
        if (isAwake)
        {
            _eyeLidLeft.SetActive(false);
            _eyeLidRight.SetActive(false);
            _bossNameGO.SetActive(true);
            CrushTarget();
        }
        else
        {
            _eyeLidLeft.SetActive(true);
            _eyeLidRight.SetActive(true);
            _bossNameGO.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Level.Instance.ResetLevel();
        }
    }

    private void CrushTarget()
    {
        var x = gameObject.transform.position.x - _targetTransform.transform.position.x;
        if (x <= 0.25)
        {
            //_animator.SetFloat("WaitTime", 1 / _waitTime);
            //_animator.Play("Stomper_Crush", -1, _animationOffset);
            _animator.SetBool("isCrushing", false);
           // StartCoroutine(WaitAnim());
            Debug.Log("Waited for: " + _waitTime);

            //_animator.SetFloat("WaitTime", 0f);
            //_animator.SetBool("isCrushing", false);
        }
        else
            _animator.SetBool("isCrushing", true);
    }

    private IEnumerator WaitAnim()
    {
        yield return new WaitForSeconds(_waitTime);
    }
}
