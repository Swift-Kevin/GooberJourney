using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAILVL3 : MonoBehaviour
{
    Transform _Player;
    float distance;
    public float bulletSpeed;
    public float howClosetoPlayer;
    public float fireRate;
    public float nextFire;
    public Transform turretWeapon;
    public Transform shootStartingPosition;
    public Transform shootStartingPosition2;
    public Transform shootStartingPosition3;
    public Transform shootStartingPosition4;
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
        GameObject _shoot2 = Instantiate(projectile, shootStartingPosition2.position, turretWeapon.rotation);
        GameObject _shoot3 = Instantiate(projectile, shootStartingPosition3.position, turretWeapon.rotation);
        GameObject _shoot4 = Instantiate(projectile, shootStartingPosition4.position, turretWeapon.rotation);
        _shoot.GetComponent<Rigidbody>().AddForce(turretWeapon.forward * bulletSpeed);
        _shoot2.GetComponent<Rigidbody>().AddForce(turretWeapon.forward * bulletSpeed);
        _shoot3.GetComponent<Rigidbody>().AddForce(turretWeapon.forward * bulletSpeed);
        _shoot4.GetComponent<Rigidbody>().AddForce(turretWeapon.forward * bulletSpeed);
        Destroy(_shoot, 2);
        Destroy(_shoot2, 2);
        Destroy(_shoot3, 2);
        Destroy(_shoot4, 2);

    }

}
