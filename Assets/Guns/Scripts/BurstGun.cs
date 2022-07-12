using System.Collections;
using System.Collections.Generic;
using static GunController;
using UnityEngine;

public class BurstGun : Gun, IShooting
{
    [Header("Burst Settings")]
    public float burstSize;
    public float timeBetweenRounds;


    void Awake()
    {
        currentFireState = FireState.Idle;
    }

    public void HandleUpdate()
    {
        if (Input.GetButton("Fire1") && CanShoot() && currentFireState != FireState.Firing)
        {
            StartCoroutine(HandleBurst());
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

    IEnumerator HandleBurst()
    {
        for (int i = 0; i < burstSize; i++)
        {
            currentFireState = FireState.Firing;
            if (leftInClip > 0)
                Shoot();

            yield return new WaitForSeconds(timeBetweenRounds); // wait till the next round
        }
    }
}
