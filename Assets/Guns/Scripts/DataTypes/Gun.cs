using System.Collections;
using UnityEngine;
using static CameraRecoilController;
using static GunRecoilController;
using static GunController;
using static PlayerController;
using static GunUtils;

public class Gun : MonoBehaviour
{
    [Header("Info")]
    public new string name;
    public float baseReloadTime = .5f;
    public Transform barrelPosition;

    [Header("Refernce Points")]
    public Transform rotationPoint;
    public Transform recoilPosition;

    [HideInInspector]
    public FireState currentFireState;
    [HideInInspector]
    public AimState currentAimState;

    [Header("Camera Recoil Controls (Aim Effecting)")]
    public Vector3 hipRecoil;
    public Vector3 aimRecoil;
    public float baseReturnSpeed;
    public float baseRotationSpeed;

    [Header("Magazine")]
    public float baseFireRate = .3f;
    public int baseClipSize = 30;
    public int leftInClip = 30;

    [Header("Damage")]
    public float baseDamage = 100;
    public float baseFalloff = 0;

    [Header("Muzzle Flash")]
    public ParticleSystem muzzleFlash;

    [Header("Muzzle Flash")]
    public Attachment[] attachments;

    [Header("Gun Image")]
    public Sprite gunImage;

    // Affectors for attachments
    private float damage;
    private float falloff;
    private float fireRate;
    private float reloadTime;
    private float returnSpeed;
    [HideInInspector]
    public int clipSize;

    [HideInInspector]
    public float lastShot = 0;
    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public Collider gunCol;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gunCol = GetComponent<Collider>();

        currentAimState = AimState.Hip;
        currentFireState = FireState.Idle;

        UpdateGunStats();
    }

    public void PickUp()
    {
        gunCol.isTrigger = true;

        rb.isKinematic = true;
        rb.useGravity = false;
        
        gameObject.transform.parent = gunController.gunParent;
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localEulerAngles = Vector3.zero;
    }

    public void Drop()
    {
        gunCol.isTrigger = false;

        rb.isKinematic = false;
        rb.useGravity = true;
        gameObject.transform.parent = null;

        // Code for the toss when dropping
        rb.velocity = playerController.characterController.velocity;
        rb.AddForce(playerController.gameObject.transform.forward * 3 + Vector3.up * 3, ForceMode.VelocityChange);

        //So if picked up you can fire right away
        lastShot = 0;
    }

    public void Shoot(float bloomMultiplyer = 1)
    {
        muzzleFlash.Play();

        // Fire rate is 1 / fireRate becuase fireRate is measure in bullets per second
        lastShot = Time.time + (1 / fireRate);

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))
        {
            Target target = hit.transform.gameObject.GetComponent<Target>();

            if (target != null)
                target.TargetHit(CalculateDamage(hit.point), hit);

            //GameObject scorch = Instantiate(gunUtil.GetRandomScorch(), hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            //scorch.transform.parent = hit.transform;
        }

        leftInClip--;

        Recoil();
    }

    float CalculateDamage(Vector3 targetPos)
    {
        float dist = Mathf.Abs(Vector3.Distance(targetPos, barrelPosition.position));
        float multiplier = Mathf.Pow(falloff, -dist);
        return multiplier < 0.1f ? 0 : damage * multiplier;
    }

    public bool CanShoot()
    {
        return lastShot <= Time.time && leftInClip > 0 && currentFireState != FireState.Reloading && playerController.canShoot;
    }

    public IEnumerator Reload()
    {
        Debug.Log("Reloading");
        currentFireState = FireState.Reloading;
        yield return new WaitForSeconds(reloadTime);
        currentFireState = FireState.Idle;
        leftInClip = clipSize;
        Debug.Log("Reload Complete");
    }

    public void Recoil()
    {
        cameraRecoilController.Recoil(currentAimState);
        gunRecoilController.Recoil(currentAimState);
    }

    public void UpdateGunStats()
    {
        float damageMult = 1;
        float falloffMult = 1;
        float fireRateMult = 1;
        float recoilMult = 1;
        float reloadMult = 1;

        foreach (Attachment attachment in attachments)
        {
            if (attachment == null)
                return;

            damageMult += attachment.damageMultiplier;
            falloffMult -= attachment.falloffMultiplier;
            fireRateMult += attachment.fireRateMultiplier;
            recoilMult += attachment.recoilMultiplier;
            reloadMult += attachment.reloadMultiplier;
        }

        falloff = baseFalloff * falloffMult;
        damage = baseDamage * damageMult;
        fireRate = baseFireRate * fireRateMult;
        returnSpeed = baseReturnSpeed * recoilMult;
        
        reloadTime = reloadMult * baseReloadTime;
    }
}

public enum FireState
{
    Firing,
    Idle,
    Reloading
}

public enum AimState
{ 
    Aiming,
    Hip
}