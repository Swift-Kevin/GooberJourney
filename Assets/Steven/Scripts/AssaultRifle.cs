using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour
{
    [Header("Weapon Stat References")]
    [SerializeField] GunData1 gunData;

    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
    }

    public void Shoot()
    {
        Debug.Log("Shot Gun!");
    }
}
