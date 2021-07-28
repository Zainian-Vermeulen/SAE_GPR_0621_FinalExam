using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;
    [SerializeField] float _offsetToShiftCamera;
    [SerializeField] float _camOffset;
    [SerializeField] float _minY, _maxY;
    [SerializeField] float _lerpSpeed;

    private float _targetY = float.MinValue;
    private float _currentY;

    private void Start()
    {
        _playerController.Grounded += OnGrounded;
        OnGrounded();
    }

    private void OnGrounded()
    {
        float newY = _playerController.transform.position.y + _camOffset;

        if (Mathf.Abs(newY - _targetY) > _offsetToShiftCamera)
        {
            _targetY = Mathf.Clamp(newY, _minY, _maxY);
        }
    }

    private void Update()
    {
        _currentY = Mathf.Lerp(_currentY, _targetY, Time.deltaTime * _lerpSpeed);
        transform.position = new Vector3(_playerController.transform.position.x, _currentY, -10);
    }
}
