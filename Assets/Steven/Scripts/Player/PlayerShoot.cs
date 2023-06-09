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
    bool reload = false;
    [HideInInspector]
    bool isShooting = false;

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
            currentAmmo--;
            isShooting = true;
            gunAnimation.SetBool("Shooting", true);

            nextTimeToFire = Time.time + 1f / fireRate;
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
            Destroy(bullet, 2);
            yield return new WaitForSeconds(fireRate);
            isShooting = false;
        }
        gunAnimation.SetBool("Shooting", false);
    }

}
