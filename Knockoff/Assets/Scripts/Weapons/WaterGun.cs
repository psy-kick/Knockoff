using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using Photon.Pun;

public class WaterGun : Gun
{
    [SerializeField] private GameObject WaterprojectilePrefab;
    public Transform SpawnPoint;
    public LayerMask aimLayerMask;

    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] Transform HitVfxText;
    [SerializeField] Transform HitVfx;
    [SerializeField] Transform HitSound;

    public override void Use()
    {
        WaterShoot();
    }

    void WaterShoot()
    {
        Vector3 mouseWorldPos = Vector3.zero;
        Vector2 screencenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screencenter);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 999f, aimLayerMask))
        {
            mouseWorldPos = hitInfo.point;
            Vector3 aimDir = (mouseWorldPos - SpawnPoint.position).normalized;
            GameObject _projectile = PhotonNetwork.Instantiate(WaterprojectilePrefab.name, SpawnPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
            _projectile.GetComponent<Rigidbody>().velocity = _projectile.transform.forward * projectileSpeed;

            StartCoroutine(WaitForBullet(_projectile));
        }
    }

    private IEnumerator WaitForBullet(GameObject p)
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(HitSound, p.transform.position, Quaternion.identity);
        PhotonNetwork.Instantiate(HitVfx.name, p.transform.position, Quaternion.identity);
        PhotonNetwork.Instantiate(HitVfxText.name, p.transform.position, Quaternion.identity);
        PhotonNetwork.Destroy(p);
    }
}
