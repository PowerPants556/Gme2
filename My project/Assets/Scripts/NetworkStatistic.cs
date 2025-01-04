using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkStatistic : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(UpdateConnectedPlayersCount());
    }
    private IEnumerator UpdateConnectedPlayersCount()
    {
        yield return new WaitForSeconds(3);
        while (true)
        {
            UIManager.Instance.ChangeConnectedPlayerText(PhotonNetwork.CountOfPlayers);
            yield return new WaitForSeconds(6);
        }
    }
}