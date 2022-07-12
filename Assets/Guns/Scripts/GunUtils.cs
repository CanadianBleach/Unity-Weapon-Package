using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUtils : MonoBehaviour
{
    [Header("Hit Marks")]
    public GameObject[] scorchMarks;

    public static GunUtils gunUtil;

    public void Start()
    {
        gunUtil = this;
    }

    public GameObject GetRandomScorch()
    {
        return scorchMarks[UnityEngine.Random.Range(0, scorchMarks.Length - 1)];
    }
}
