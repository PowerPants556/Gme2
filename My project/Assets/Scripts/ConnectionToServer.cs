using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class ConnectionToServer : MonoBehaviourPunCallbacks
{
    public static ConnectionToServer Instance { get; private set; }


    [SerializeField] private TMP_InputField inputRoomName;
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private Transform transformRoomList;
    [SerializeField] private GameObject roomItemPrefab;

    [SerializeField] private GameObject playerListPrefab;
    [SerializeField] private Transform playerListT;

    [SerializeField] private GameObject startGameButton;

    private void Awake()
    {
        Instance = this;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform room in transformRoomList)
        {
            Destroy(room.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            Instantiate(roomItemPrefab, transformRoomList).GetComponent<RoomItem>().roomInfo = roomList[i];
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        Debug.Log($"Connected to lobby!");
        UIManager.Instance.OpenPanel("MenuPanel");
    }

    public override void OnJoinedRoom()
    {
        UIManager.Instance.OpenPanel("GameRoomPanel");
        roomName.text = PhotonNetwork.CurrentRoom.Name;

        if (!PhotonNetwork.IsMasterClient)
        {
            startGameButton.SetActive(false);
        }

        Player[] players = PhotonNetwork.PlayerList;
        foreach (Transform player in playerListT)
        {
            Destroy(player.gameObject);
        }
        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListPrefab, playerListT).GetComponent<PlayerListItem>().playerInfo = players[i];
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListPrefab, playerListT).GetComponent<PlayerListItem>().playerInfo = newPlayer; ;
    } 

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        UIManager.Instance.OpenPanel("MenuPanel");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            startGameButton.SetActive(true);
        }
    }

    public void CreateNewRoom()
    {
        if (string.IsNullOrEmpty(inputRoomName.text)) return;

        PhotonNetwork.CreateRoom(inputRoomName.text);
    }

    public void ConnectedToRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void StartLevel(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
