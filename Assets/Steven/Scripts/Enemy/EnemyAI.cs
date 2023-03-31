using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    Transform _Player;
    float distance;
    public float bulletSpeed;
    public float howClosetoPlayer;
    public float fireRate;
    public float nextFire;
    public Transform turretWeapon;
    public Transform shootStartingPosition;
    public GameObject projectile;

    private void Start()
    {
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

}
