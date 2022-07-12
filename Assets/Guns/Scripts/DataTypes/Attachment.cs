using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Attachment", menuName = "Guns/Attachment", order = 1)]
public class Attachment : ScriptableObject
{
    [Header("Info")]
    public new string name;
    public Sprite attachmentImage;
    public AttachmentType type;

    [Header("Multipliers")]
    public float damageMultiplier = 0;
    public float falloffMultiplier = 0;
    public float fireRateMultiplier = 0;
    public float recoilMultiplier = 0;
    public float reloadMultiplier = 0;
}

public enum AttachmentType
{
    Stock,
    Overclock,
    Scope,
    FMJ,
}
