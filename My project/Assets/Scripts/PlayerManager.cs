using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private PhotonView pView;

    private void Start()
    {
        if (pView.IsMine)
        {
            CreateController();
        }
    }

    private void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("PlayerController"), Vector3.zero, Quaternion.identity);
    }
}
