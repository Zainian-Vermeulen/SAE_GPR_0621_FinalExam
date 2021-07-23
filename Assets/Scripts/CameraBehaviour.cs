using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] float offsetToShiftCamera;
    [SerializeField] float camOffset;
    [SerializeField] float minY, maxY;
    [SerializeField] float lerpSpeed;

    private float targetY = float.MinValue;
    private float currentY;

    private void Start()
    {
        playerController.Grounded += OnGrounded;
        OnGrounded();
    }

    private void OnGrounded()
    {
        float newY = playerController.transform.position.y + camOffset;

        if (Mathf.Abs(newY - targetY) > offsetToShiftCamera)
        {
            targetY = Mathf.Clamp(newY, minY, maxY);
        }
    }

    private void Update()
    {
        currentY = Mathf.Lerp(currentY, targetY, Time.deltaTime * lerpSpeed);
        transform.position = new Vector3(playerController.transform.position.x, currentY, -10);
    }
}
