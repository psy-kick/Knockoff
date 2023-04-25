using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using KnockOff.Player;

public class RocketLauncherProjectile : Gun
{
    private Rigidbody projectile;
    public Transform SpawnPoint;
    public Transform camTarget;

    [SerializeField]
    float projectileSpeed = 10f;

    private void Start()
    {
        camTarget = Utils.FindWithTag(PlayerManager.LocalPlayerInstance.transform, "CamTarget");
    }
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
        SpawnPoint.localRotation = camTarget.localRotation;
        _projectile = Instantiate(projectile,SpawnPoint.position, SpawnPoint.localRotation);
        _projectile.velocity = transform.rotation*(Vector3.forward * projectileSpeed);
    }
}
