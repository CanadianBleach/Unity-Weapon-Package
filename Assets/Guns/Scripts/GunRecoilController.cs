using System.Collections;
using System.Collections.Generic;
using static Gun;
using UnityEngine;

public class GunRecoilController : MonoBehaviour
{
    [Header("Reference Points")]
    public Transform recoilPosition;
    public Transform rotationPoint;
    [Space(30)]

    [Header("Speed Settings")]
    public float positionRecoilSpeed = 8f;
    public float rotationalRecoilSpeed = 8f;
    [Space(10)]

    public float positionalReturnSpeed = 18f;
    public float rotationalReturnSpeed = 38f;
    [Space(10)]

    [Header("Amount Settings")]
    public Vector3 recoilRotation;
    public Vector3 recoilKickBack;
    [Space(10)]
    public Vector3 recoilRotationAim;
    public Vector3 recoilKickBackAim;
    [Space(30)]

    Vector3 rotationalRecoil;
    Vector3 positionalRecoil;
    Vector3 rot;

    // Public instance
    public static GunRecoilController gunRecoilController;

    private void Start()
    {
        gunRecoilController = this;
    }

    private void FixedUpdate()
    {
        // Controls slerping lerping and interpolation of the gun kick and recoil
        rotationalRecoil = Vector3.Lerp(rotationalRecoil, Vector3.zero, rotationalRecoilSpeed * Time.deltaTime);
        positionalRecoil = Vector3.Lerp(positionalRecoil, Vector3.zero, positionalReturnSpeed * Time.deltaTime);

        recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, positionalRecoil, positionRecoilSpeed * Time.deltaTime);
        rot = Vector3.Slerp(rot, rotationalRecoil, rotationalRecoilSpeed * Time.deltaTime);
        rotationPoint.localRotation = Quaternion.Euler(rot);
    }

    public void SetRecoil(Gun gun)
    {
        /* Sets all controls to the specifc gun
        positionRecoilSpeed = gun.positionRecoilSpeed;
        rotationalRecoilSpeed = gun.rotationalRecoilSpeed;


        positionalReturnSpeed = gun.positionalReturnSpeed;
        rotationalReturnSpeed = gun.rotationalReturnSpeed;

        recoilRotation = gun.recoilRotation;
        recoilKickBack = gun.recoilKickBack;

        recoilRotationAim = gun.recoilRotationAim;
        recoilKickBackAim = gun.recoilKickBackAim;
        */

        recoilPosition = gun.recoilPosition;
        rotationPoint = gun.rotationPoint;
    }

    public void Recoil(AimState aimState)
    {
        if (aimState == AimState.Aiming)
        {
            rotationalRecoil += new Vector3(-recoilRotationAim.x, Random.Range(-recoilRotationAim.y, recoilRotationAim.y), Random.Range(-recoilRotationAim.z, recoilRotationAim.z));
            positionalRecoil += new Vector3(-recoilKickBackAim.x, Random.Range(-recoilKickBackAim.y, recoilKickBackAim.y), Random.Range(-recoilKickBackAim.z, recoilKickBackAim.z));
        }
        else
        {
            rotationalRecoil += new Vector3(-recoilRotation.x, Random.Range(-recoilRotation.y, recoilRotation.y), Random.Range(-recoilRotation.z, recoilRotation.z));
            positionalRecoil += new Vector3(-recoilKickBack.x, Random.Range(-recoilKickBack.y, recoilKickBack.y), Random.Range(-recoilKickBack.z, recoilKickBack.z));
        }
    }
}
