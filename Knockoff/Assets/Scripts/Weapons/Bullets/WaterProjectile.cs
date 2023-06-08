using KnockOff.Player;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

public class WaterProjectile : MonoBehaviourPunCallbacks
{
    [SerializeField] float expForce = 100f;
    [SerializeField] private AudioSource _audioSource;

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
            photonView.RPC("KnockBackPlayer", RpcTarget.Others, targetPlayerID, playerOwner, expForce, contactPoint);
        }
    }

    [PunRPC]
    private void KnockBackPlayer(int targetPlayerID, Photon.Realtime.Player attackingPlayer, float expForce, Vector3 contactPoint)
    {
        PhotonView pv = PhotonView.Find(targetPlayerID);

        if (photonView.Owner == attackingPlayer && attackingPlayer != null)
        {
            Rigidbody exPlode = pv.GetComponent<Rigidbody>();
            pv.GetComponent<PlayerRespawn>().Opponent = attackingPlayer;
            pv.GetComponent<PlayerMovement>().anim.SetTrigger("GotHit");
            Vector3 knockbackDir = (contactPoint - transform.position).normalized;
            exPlode.AddForce(knockbackDir * expForce, ForceMode.Impulse);
            Debug.Log("KNOCK BACK");
        }
    }

    private bool IsFriendlyFire(int targetPlayerID)
    {
        PhotonView pv = PhotonView.Find(targetPlayerID);

        if (photonView.Owner.GetPhotonTeam() == pv.Owner.GetPhotonTeam())
            return true;

        return false;
    }
}
