using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RocketLauncherProjectile : Gun
{
    [SerializeField] private GameObject projectilePrefab;
    public Transform SpawnPoint;
    public LayerMask aimLayerMask;

    [SerializeField] float projectileSpeed = 10f;

    public override void Use()
    {
        Shoot();
    }

    void Shoot()
    {
        Vector3 mouseWorldPos = Vector3.zero;
        Vector2 screencenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screencenter);
        
        if(Physics.Raycast(ray, out RaycastHit hitInfo, 999f, aimLayerMask))
        {
            mouseWorldPos = hitInfo.point;
            Vector3 aimDir = (mouseWorldPos - SpawnPoint.position).normalized;
            GameObject _projectile = PhotonNetwork.Instantiate(projectilePrefab.name, SpawnPoint.position, Quaternion.LookRotation(aimDir,Vector3.up));
            _projectile.GetComponent<Rigidbody>().velocity = _projectile.transform.forward * projectileSpeed;

            StartCoroutine(WaitForBullet(_projectile));
        }
    }

    private IEnumerator WaitForBullet(GameObject p)
    {
        yield return new WaitForSeconds(3f);
        PhotonNetwork.Destroy(p);
    }
}
