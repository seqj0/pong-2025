using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PaddleSync : MonoBehaviour, IPunObservable
{
    private PhotonView photonView;

    Vector3 latestPos;
    public void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            latestPos = (Vector3)stream.ReceiveNext();
        }
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, latestPos, Time.deltaTime * 10f);
        }
    }
}
