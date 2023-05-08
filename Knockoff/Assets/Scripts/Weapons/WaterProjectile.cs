using KnockOff.Player;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterProjectile : MonoBehaviourPunCallbacks
{
    [SerializeField] float expForce = 50f;
    [SerializeField] float radius = 2f;
    [SerializeField] Transform HitAudio;

    public Photon.Realtime.Player playerOwner { get; set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<PlayerManager>() != null && collision.gameObject.GetComponentInParent<PlayerManager>().localPlayer)
        {
            Debug.Log("this is local");
            return;
        }
        else if (collision.transform.tag == "Player")
        {
            int targetPlayerID = collision.gameObject.GetComponentInParent<PhotonView>().ViewID;
            Vector3 contactPoint = collision.contacts[0].point;
            Instantiate(HitAudio, transform.position, Quaternion.identity);
            photonView.RPC("KnockBackPlayer", RpcTarget.Others, targetPlayerID, playerOwner, expForce, radius, contactPoint);
        }
    }

    [PunRPC]
    private void KnockBackPlayer(int targetPlayerID, Photon.Realtime.Player attackingPlayer, float expForce, float radius, Vector3 contactPoint)
    {
        PhotonView pv = PhotonView.Find(targetPlayerID);

        if (photonView.Owner == attackingPlayer && attackingPlayer != null)
        {
            Rigidbody exPlode = pv.GetComponent<Rigidbody>();
            pv.GetComponent<PlayerRespawn>().Opponent = attackingPlayer;
            Vector3 knockbackDir = (photonView.transform.position - contactPoint).normalized;
            exPlode.AddForceAtPosition(-knockbackDir * expForce, contactPoint, ForceMode.Impulse);
            //pv.GetComponent<PlayerMovement>().anim.SetBool("GotHit", true);
        }
    }
}
