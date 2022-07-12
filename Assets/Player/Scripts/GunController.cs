using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CameraRecoilController;
using static GunRecoilController;

public class GunController : MonoBehaviour
{
    [Header("Gun Controls")]
    public Gun primaryGun;
    public Gun secondaryGun;
    [Space(10)]

    [Header("Placement Info")]
    public Transform gunPos;
    public Transform adsPos;
    public Transform gunParent;

    [HideInInspector]
    public Gun currentGun;

    public static GunController gunController;

    private void Start()
    {
        gunController = this;

        currentGun = primaryGun;
        secondaryGun.gameObject.SetActive(false);

        if (currentGun != null)
        {
            cameraRecoilController.SetRecoil(currentGun);
            gunRecoilController.SetRecoil(currentGun);
        }
    }

    void Update()
    {
        // Calls the HandleUpdate method in the current gun
        if (currentGun != null)
            ((IShooting)currentGun).HandleUpdate();

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            SwapGuns();
    }

    public void SwapGuns()
    {
        if (primaryGun == null || secondaryGun == null)
        {
            Debug.Log("Player has no weapons to swap");
            return;
        }

        currentGun.gameObject.SetActive(false);

        if (currentGun == primaryGun)
        {
            currentGun = secondaryGun;
        }
        else
        {
            currentGun = primaryGun;
        }

        currentGun.gameObject.SetActive(true);
    }
}
