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
        Transform point = SpawnManager.Instance.GetRandomSpawnPoint();

        controller = 
            PhotonNetwork.Instantiate(Path.Combine("PlayerController"),
            point.position, point.rotation,0, new object[] {pView.ViewID});
    }

    public void Die()
    {
        PhotonNetwork.Destroy(controller);
        CreateController();
    }
}
