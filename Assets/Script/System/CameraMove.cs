using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform cameraTransform;
    private Vector3 orignalCameraPos;

    // Shake Parameters
    public float shakeDuration = 2f;
    public float shakeAmount = 0.7f;

    private bool b_canShake = false;
    private float shakeTimer;

    void Start()
    {
        orignalCameraPos = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (b_canShake) {
            StartCameraShakeEffect();
        }
    }

    /// <summary>
    /// set camera shake and duration
    /// </summary>
    public void SetShakeCamera()
    {
        b_canShake = true;
        shakeTimer = shakeDuration;
    }

    /// <summary>
    /// start shake camera effect
    /// </summary>
    public void StartCameraShakeEffect()
    {
        if (shakeTimer > 0) {
            cameraTransform.localPosition = orignalCameraPos + UnityEngine.Random.insideUnitSphere * shakeAmount;
            shakeTimer -= Time.deltaTime;
        } else {
            shakeTimer = 0f;
            cameraTransform.position = orignalCameraPos;
            b_canShake = false;
        }
    }
}
