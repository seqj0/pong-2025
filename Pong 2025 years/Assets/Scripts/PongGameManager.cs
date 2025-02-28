using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PongGameManager : MonoBehaviourPunCallbacks
{
    public GameObject ballPrefab;
    public Transform leftPaddleSpawn;
    public Transform rightPaddleSpawn;
    public GameObject paddlePrefab;

    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            JoinOrCreateRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        JoinOrCreateRoom();
    }

    void JoinOrCreateRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("✅ Подключились к комнате!");

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(paddlePrefab.name, leftPaddleSpawn.position, Quaternion.identity);
            PhotonNetwork.Instantiate(ballPrefab.name, Vector3.zero, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate(paddlePrefab.name, rightPaddleSpawn.position, Quaternion.identity);
        }
    }
}
