using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            UIManager.Instance.ChangeConnectedPlayersTxt(PhotonNetwork.CountOfPlayers);
            yield return new WaitForSeconds(6);
        }
    }
}