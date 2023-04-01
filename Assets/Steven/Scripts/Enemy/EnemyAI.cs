using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Transform _Player;
    float distance;
    public float bulletSpeed;
    public float currentHealth;
    public float maxHealth;
    public float howClosetoPlayer;
    public float fireRate;
    public float nextFire;
    public Transform turretWeapon;
    public Transform shootStartingPosition;
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
        _shoot.GetComponent<Rigidbody>().AddForce(turretWeapon.forward * bulletSpeed);
        Destroy(_shoot, 2);

    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
