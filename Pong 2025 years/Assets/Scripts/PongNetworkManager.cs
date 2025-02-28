using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PongNetworkManager : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        if (PhotonNetwork.NetworkClientState == ClientState.ConnectedToMasterServer)
        {
            Debug.Log("✅ Подключились к мастер-серверу");
            PhotonNetwork.JoinLobby();
        }
        else
        {
            Debug.LogWarning("Не готовы для входа в лобби, текущее состояние: " + PhotonNetwork.NetworkClientState);
        }

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("✅ Зашли в лобби");
        JoinOrCreateRoom();
    }

    void JoinOrCreateRoom()
    {
        var roomOptions = new RoomOptions { MaxPlayers = 2 };
        PhotonNetwork.JoinRandomOrCreateRoom(null, 2, MatchmakingMode.FillRoom, TypedLobby.Default, null, null, roomOptions);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("✅ Зашли в комнату");
    }
}
