using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAILVL2 : MonoBehaviour
{
    Transform _Player;
    float distance;
    public float maxHealth;
    public float currentHealth;
    public float bulletSpeed;
    public float howClosetoPlayer;
    public float fireRate;
    public float nextFire;
    public Transform turretWeapon;
    public Transform shootStartingPosition;
    public Transform shootStartingPosition2;
    public GameObject projectile;

    private void Start()
    {
        currentHealth = maxHealth;
        _Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {

        distance = Vector3.Distance(_Player.position, transform.position);
        if (distance <= howClosetoPlayer)
        {
            turretWeapon.LookAt(_Player);
            if (Time.time >= nextFire)
            {
                nextFire = Time.time + 1f / fireRate;
                shoot();
            }
        }
    }
    void shoot()
    {
        GameObject _shoot = Instantiate(projectile, shootStartingPosition.position, turretWeapon.rotation);
        GameObject _shoot2 = Instantiate(projectile, shootStartingPosition2.position, turretWeapon.rotation);
        _shoot.GetComponent<Rigidbody>().AddForce(turretWeapon.forward * bulletSpeed);
        _shoot2.GetComponent<Rigidbody>().AddForce(turretWeapon.forward * bulletSpeed);
        Destroy(_shoot, 2);
        Destroy(_shoot2, 2);

    }

    public void TakeDamage(float Amount)
    {
        currentHealth -= Amount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
