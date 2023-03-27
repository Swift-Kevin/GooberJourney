using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    [Header("Name of Weapon")]
    public new string name;

    [Header("Gun Data")]
    [SerializeField] public float damage;
    [SerializeField] public float maxDistance;
    [SerializeField] public float fireRate;
    private float nextTimeToFire = 0f;

    [Header("Mag")]
    [SerializeField] public int maxAmmo;
    [SerializeField] public int currentAmmo;
    [SerializeField] public float reloadSpeed;
    [HideInInspector]
    public bool reload = false;

    public Animator gunAnimation;

    private void Start()
    {
        currentAmmo = maxAmmo;
    }

    public void Update()
    {
        if (reload)
        {
            return;
        }
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        StartCoroutine(Shoot());
    }

    IEnumerator Reload()
    {
        reload = true;
        gunAnimation.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadSpeed);
        gunAnimation.SetBool("Reloading", false);

        currentAmmo = maxAmmo;
        reload = false;
    }

    IEnumerator Shoot()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            gunAnimation.SetBool("Shooting", true);
            currentAmmo--;
            nextTimeToFire = Time.time + 1f / fireRate;
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
            gunAnimation.SetBool("Shooting", false);
        yield return new WaitForSeconds(fireRate);
    }

}
