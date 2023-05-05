using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KnockOff.Player;

public class Projectile : MonoBehaviourPunCallbacks
{
    [SerializeField] float expForce = 100f;
    [SerializeField] float radius = 2f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<PlayerManager>() != null && collision.gameObject.GetComponentInParent<PlayerManager>().localPlayer)
        {
            Debug.Log("this is local");
            return;
        }
        else if (collision.transform.tag == "Player")
        {
            int playerId = collision.gameObject.GetComponentInParent<PhotonView>().ViewID;
            Vector3 contactPoint = collision.contacts[0].point;
            photonView.RPC("KnockBackPlayer", RpcTarget.All, playerId, expForce, radius, contactPoint);
        }
    }

    [PunRPC]
    private void KnockBackPlayer(int playerId, float expForce, float radius, Vector3 contactPoint)
    {
        PhotonView pv = PhotonView.Find(playerId);

        if (pv.IsMine)
        {
            Rigidbody exPlode = pv.GetComponent<Rigidbody>();
            Vector3 knockbackDir = (photonView.transform.position - contactPoint).normalized;
            exPlode.AddForceAtPosition(-knockbackDir * expForce, contactPoint, ForceMode.Impulse);
        }
    }
}
