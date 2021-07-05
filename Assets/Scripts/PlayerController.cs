using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum VisualState
    {
        Idle,
        Run,
        Jump,
        Land
    }
    
    [Header("Animations")]
    [SerializeField] Animator _animator;

    [SerializeField] VisualState _currentVisualState;
    [SerializeField] string[] _visualStatesNames;

    [SerializeField] float _visualWalkSpeed;

    [Header("Behaviour")]
    [SerializeField] Rigidbody2D _rigidbody;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        _animator.Play(_visualStatesNames[(int)_currentVisualState]);
        _animator.SetFloat("WalkSpeed", _visualWalkSpeed);

    }
}
