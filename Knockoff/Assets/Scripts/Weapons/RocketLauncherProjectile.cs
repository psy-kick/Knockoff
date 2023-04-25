using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RocketLauncherProjectile : Gun
{
    private Rigidbody projectile;
    public Transform SpawnPoint;
    [SerializeField]
    float projectileSpeed = 10f;
    private void Awake()
    {
        projectile = GetComponentInChildren<Rigidbody>();
    }
    public override void Use()
    {
        Shoot(projectile);
    }
    void Shoot(Rigidbody _projectile)
    {
        _projectile = Instantiate(projectile,SpawnPoint.position, transform.rotation);
        _projectile.velocity = transform.forward * projectileSpeed;
    }
}
