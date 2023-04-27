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
    public LayerMask aimLayerMask;

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
        Vector3 mouseWorldPos = Vector3.zero;
        Vector2 screencenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screencenter);
        if(Physics.Raycast(ray, out RaycastHit hitInfo, 999f, aimLayerMask))
        {
            mouseWorldPos = hitInfo.point;
            Vector3 aimDir = (mouseWorldPos - SpawnPoint.position).normalized;
            _projectile = Instantiate(projectile,SpawnPoint.position, Quaternion.LookRotation(aimDir,Vector3.up));
            _projectile.velocity = _projectile.transform.forward * projectileSpeed;
            Destroy(_projectile.gameObject, 2);
        }
    }
}
