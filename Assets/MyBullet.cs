using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MyBullet : MonoBehaviourPun
{
    private void OnCollisionEnter(Collision other)
    {
        if (photonView.IsMine)
        {
            return;
        }

        if (other.transform.CompareTag("Player"))
        {
            other.gameObject.GetComponent<MyPlayer>().Hit();
            photonView.RPC("kill", RpcTarget.All);
        }
    }

    [PunRPC]
    void Kill()
    {
        if (photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
