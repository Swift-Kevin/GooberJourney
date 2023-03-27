using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapon")]
public class GunData1 : ScriptableObject
{
    [Header("Name of Weapon")]
    public new string name;

    [Header("Gun Data")]
    [SerializeField] public float damage;
    [SerializeField] public float maxDistance;

    [Header("Mag Data")]
    [SerializeField] private int maxAmmo;
    [SerializeField] public int currentAmmo;
    [SerializeField] public float fireRate;
    [SerializeField] public float reloadSpeed;
    [HideInInspector]
    public bool reload;
}
