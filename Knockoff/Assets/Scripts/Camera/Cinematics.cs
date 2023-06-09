using KnockOff;
using KnockOff.Player;
using System.Collections;
using UnityEngine;

public class Cinematics : MonoBehaviour
{
    private PlayerManager playerManager;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerManager.gameObject.SetActive(false);
    }

    private void Update()
    {
        StartCoroutine(CinematicOff());
    }

    IEnumerator CinematicOff()
    {
        yield return new WaitForSeconds(6f);
        playerManager.gameObject.SetActive(true);
        this.gameObject.SetActive(false);
        GameManager.instance.isMatchPlaying = true;
    }
}
