using System.Collections;
using System.Collections.Generic;
using static PlayerController;
using static GunController;
using UnityEngine;
using System;

public class CameraRecoilController : MonoBehaviour
{
    [Header("Recoil Settings")]
    public float rotationSpeed = 6;
    public float returnSpeed = 25;
    [Space()]

    [Header("Hipfire")]
    public Vector3 recoilRotation = new Vector3(2f, 2f, 2f);
    [Space()]

    [Header("Aiming")]
    public Vector3 recoilRotationAiming = new Vector3(.5f, 8.5f, 1.5f);
    [Space()]

    private Vector3 currentRotation;
    private Vector3 rot;

    public static CameraRecoilController cameraRecoilController;

    private void Start()
    {
        cameraRecoilController = this;
    }

    private void FixedUpdate()
    {
        currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        rot = Vector3.Slerp(rot, currentRotation, rotationSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(rot);
    }

    public void SetRecoil(Gun gun)
    {
        recoilRotation = gun.hipRecoil;
        recoilRotationAiming = gun.aimRecoil;
    }

    public void Recoil(AimState aimState)
    {
        if (aimState == AimState.Aiming)
        {
            currentRotation += new Vector3(-recoilRotationAiming.x, UnityEngine.Random.Range(-recoilRotationAiming.y, recoilRotationAiming.y), UnityEngine.Random.Range(-recoilRotationAiming.z, recoilRotationAiming.z));
        }
        else
        {
            currentRotation += new Vector3(-recoilRotation.x, UnityEngine.Random.Range(-recoilRotation.y, recoilRotation.y), UnityEngine.Random.Range(-recoilRotation.z, recoilRotation.z));
        }
    }
}
