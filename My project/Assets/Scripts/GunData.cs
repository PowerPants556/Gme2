using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GunData", order = 2)]

public class GunData : WeaponData
{
    public int maxAmmo;
    public float reloadTime;
}
