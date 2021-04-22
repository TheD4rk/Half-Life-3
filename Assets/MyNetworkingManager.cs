using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MyNetworkingManager : MonoBehaviour
{
    public GameObject playerPrefab;
    void Start()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 5, 0), Quaternion.identity, 0);
    }
}
