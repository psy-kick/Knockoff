using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RocketLauncherProjectile : Gun
{
    private GameObject projectile;
    [SerializeField]
    float projectileSpeed = 10f;
    public override void Use()
    {
        Shoot();
    }
    void Shoot()
    {
        projectile.GetComponent<Rigidbody>().velocity = transform.forward * projectileSpeed;
    }
}
