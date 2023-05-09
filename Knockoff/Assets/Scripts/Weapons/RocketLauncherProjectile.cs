using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RocketLauncherProjectile : Gun
{
    [SerializeField] private GameObject projectilePrefab;
    public Transform SpawnPoint;
    public LayerMask aimLayerMask;
    [SerializeField] Transform HitVfxText;
    [SerializeField] Transform HitVfx;
    [SerializeField] Transform HitSound;

    private float NextFire;

    [SerializeField] float projectileSpeed = 10f;

    [Header("Player")]
    [SerializeField] private PhotonView playerOwner;
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
            if(Time.time > NextFire)
            {
                NextFire = Time.time + itemInfo.FireRate;
                GameObject _projectile = PhotonNetwork.Instantiate(projectilePrefab.name, SpawnPoint.position, Quaternion.LookRotation(aimDir, Vector3.up));
                _projectile.GetComponent<Rigidbody>().velocity = _projectile.transform.forward * projectileSpeed;
                _projectile.GetComponent<Projectile>().playerOwner = playerOwner.Owner;
                StartCoroutine(WaitForBullet(_projectile));
            }
        }
    }

    private IEnumerator WaitForBullet(GameObject p)
    {
        yield return new WaitForSeconds(0.5f);
        Transform HitSoundInScene = Instantiate(HitSound, p.transform.position, Quaternion.identity);
        GameObject HitVfxInScene = PhotonNetwork.Instantiate(HitVfx.name, p.transform.position, Quaternion.identity);
        GameObject HitVfxTextInScene = PhotonNetwork.Instantiate(HitVfxText.name, p.transform.position, Quaternion.identity);
        StartCoroutine(DestroyFx(HitVfxTextInScene, HitVfxInScene, HitSoundInScene));
        PhotonNetwork.Destroy(p);
    }
    IEnumerator DestroyFx(GameObject Htvfx, GameObject Hvfx, Transform HtSound)
    {
        yield return new WaitForSeconds(0.5f);
        PhotonNetwork.Destroy(Htvfx);
        PhotonNetwork.Destroy(Hvfx);
        Destroy(HtSound.gameObject);
    }
}
