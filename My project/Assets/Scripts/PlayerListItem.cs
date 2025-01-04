using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text playerNameText;

    public PlayerListItem playerInfo { private get; set; }

    void Start()
    {
        Init();
    }

    private void Init()
    {
        playerNameText.text = playerInfo.NickName;
    }

    public override void OnPlayerLeftRoom(PlayerListItem otherPlayer)
    {
        if(playerInfo == otherPlayer)
        {
            Destroy(gameObject);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(GameObject);
    }
}
