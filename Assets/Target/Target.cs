using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public bool applyForce;
    Rigidbody rb;

    private void Start()
    {
        if (applyForce)
            rb = GetComponent<Rigidbody>();
    }

    public void TargetHit(float damage, RaycastHit hit)
    {
        Debug.Log("Hit for: " + damage);


        if (applyForce)
            rb.AddForceAtPosition(-hit.normal * 100, hit.point);
    }
}
