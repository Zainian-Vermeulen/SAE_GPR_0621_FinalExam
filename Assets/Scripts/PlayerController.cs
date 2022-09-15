using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum State
    {
        Idle,
        Run,
        Jump,
        Fall
    }

    [Header("Animations")]
    [SerializeField] Animator _animator;
    [SerializeField] SpriteRenderer _renderer;

    [SerializeField] State _currentState;
    [SerializeField] string[] _visualStatesNames;


    [Header("Behaviour")]
    [SerializeField] Rigidbody2D _rigidbody;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _acceleration;
    [SerializeField] float _jumpForce;
    [SerializeField] float _slowdownDragMultiplyer;
    [SerializeField] float _groundedCheckLength;
    [SerializeField] LayerMask _groundedLayerMask;
    [SerializeField] float deathHeight = -6;

    private bool _isGrounded;

    public event System.Action Grounded;

    [SerializeField] private GameObject _vfx;

    private void Start()
    {
        Level.Instance.SetPlayer(this);
        Level.Instance.Reset += OnReset;
    }

    private void OnReset()
    {
        transform.position = Vector3.zero;
        _rigidbody.velocity = Vector2.zero;
    }

    private void Update()
    {
        UpdateGrounded();
        UpdateMovement();
        UpdateState();
        UpdateVisuals();

        if (transform.position.y < deathHeight)
        {
            Level.Instance.ResetLevel();
        }
    }

    private void UpdateGrounded()
    {
        RaycastHit2D[] hits = new RaycastHit2D[5];
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(_groundedLayerMask);
        int hitCount = Physics2D.Raycast(transform.position, Vector2.down, filter, hits, _groundedCheckLength);

        _isGrounded = hitCount > 0;

        if (_isGrounded)
            Grounded?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _groundedCheckLength);
    }

    private void UpdateMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 vel = _rigidbody.velocity;

        if (IsGrounded())
        {
            if (Mathf.Abs(horizontal) > 0.05f)
            {
                _rigidbody.AddForce(new Vector2(_acceleration * horizontal * Time.deltaTime, 0), ForceMode2D.Impulse);

                if (Mathf.Abs(vel.x) > _maxSpeed)
                {
                    _rigidbody.velocity = new Vector2(Mathf.Sign(vel.x) * _maxSpeed, vel.y);
                }
            }
            else
            {
                _rigidbody.velocity = new Vector2(vel.x * (1 - Time.deltaTime * _slowdownDragMultiplyer), vel.y);
            }

            if ((_currentState == State.Idle || _currentState == State.Run) && vertical > 0)
            {
                _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
                SetState(State.Jump);
                var x = Instantiate(_vfx, this.gameObject.transform);
                Destroy(x, 0.9f);
               
            }
        }
    }

    private void SetState(State s)
    {
        _currentState = s;
    }

    private void UpdateState()
    {
        switch (_currentState)
        {
            case State.Idle:
                TryTransitionTo(State.Run, GetXSpeed() > 0.05f);
                TryTransitionTo(State.Jump, !IsGrounded());
                break;

            case State.Run:
                TryTransitionTo(State.Idle, GetXSpeed() < 0.05f);
                TryTransitionTo(State.Jump, !IsGrounded());
                break;

            case State.Jump:
                TryTransitionTo(State.Fall, GetYVelocity() < 0);
                break;

            case State.Fall:
                TryTransitionTo(State.Idle, GetYVelocity() >= 0);
                break;
        }

    }

    private void TryTransitionTo(State to, bool condition)
    {
        if (condition)
            SetState(to);
    }

    private void UpdateVisuals()
    {
        _animator.Play(_visualStatesNames[(int)_currentState]);
        _animator.SetFloat("WalkSpeed", Mathf.Abs(_rigidbody.velocity.x) / _maxSpeed);

        if (GetXSpeed() > 0.05f)
            _renderer.flipX = _rigidbody.velocity.x < 0;
    }

    private float GetXSpeed()
    {
        return Mathf.Abs(_rigidbody.velocity.x);
    }

    private float GetYVelocity()
    {
        return _rigidbody.velocity.y;
    }


    private bool IsGrounded()
    {
        return _isGrounded;
    }
}
