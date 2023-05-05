using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using KnockOff.Player;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class RocketLauncherProjectile : Gun
{
    [SerializeField] private GameObject projectilePrefab;
    public Transform SpawnPoint;
    public LayerMask aimLayerMask;

    [SerializeField] float expForce = 800f;
    [SerializeField] float radius = 2f;

    [SerializeField]
    float projectileSpeed = 10f;

    private byte knockOffEventCode = 1;

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
        yield return new WaitForSeconds(2f);
        PhotonNetwork.Destroy(p);
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*
        if (collision.gameObject.GetComponentInParent<PlayerManager>() != null && collision.gameObject.GetComponentInParent<PlayerManager>().localPlayer)
        {
            Debug.Log("this is local");
            return;
        }
        else if (collision.transform.tag == "Player")
        {
            KnockBackFx(collision);
        }
        */
        if (collision.gameObject.GetComponentInParent<PlayerManager>() != null && collision.gameObject.GetComponentInParent<PlayerManager>().localPlayer)
        {
            Debug.Log("this is local");
            return;
        }
        else if (collision.transform.tag == "Player")
        {
            int playerId = collision.gameObject.GetComponentInParent<PhotonView>().ViewID;
            Debug.LogError(playerId + " SENDING");
            KnockOffEvent knockOffEvent = new KnockOffEvent(playerId, expForce, radius);
            PhotonNetwork.RaiseEvent(knockOffEventCode, knockOffEvent, new RaiseEventOptions { Receivers = ReceiverGroup.All }, new SendOptions { Reliability = true });
        }
    }
    private void KnockBackFx(Collision collision)
    {
        Rigidbody exPlode = collision.gameObject.GetComponentInParent<Rigidbody>();
        exPlode.AddExplosionForce(expForce, transform.position, radius);
    }


}
