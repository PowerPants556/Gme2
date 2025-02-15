using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private PhotonView pView;

    private GameObject controller;

    private void Start()
    {
        if (pView.IsMine)
        {
            CreateController();
        }
    }

    private void CreateController()
    {
        controller = 
            PhotonNetwork.Instantiate(Path.Combine("PlayerController"),
            Vector3.zero, Quaternion.identity,0, new object[] {pView.ViewID});
    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        CreateController();
    }
}
