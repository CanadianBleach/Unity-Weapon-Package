using System;
using System.Collections;
using System.Collections.Generic;
using static GunController;
using UnityEngine;

public class SemiAutoGun : Gun, IShooting
{
    public void HandleUpdate()
    {
        if (Input.GetButtonDown("Fire1") && CanShoot())
        {
            currentFireState = FireState.Firing;
            Shoot();
        }
        else if (currentFireState != FireState.Reloading)
        {
            currentFireState = FireState.Idle;
        }

        // Control gun position and ads
        if (Input.GetButton("Aim"))
        {
            gameObject.transform.localPosition = gunController.adsPos.localPosition;
            gameObject.transform.localEulerAngles = gunController.adsPos.localEulerAngles;
            currentAimState = AimState.Aiming;
        }
        else
        {
            gameObject.transform.localPosition = gunController.gunPos.localPosition;
            gameObject.transform.localEulerAngles = gunController.gunPos.localEulerAngles;
            currentAimState = AimState.Hip;
        }

        if (Input.GetKeyDown(KeyCode.R) && leftInClip < clipSize)
            StartCoroutine(Reload());
    }
}
