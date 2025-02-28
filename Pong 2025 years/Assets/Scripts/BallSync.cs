using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class BallSync : MonoBehaviour, IPunObservable
{
    private PhotonView photonView;

    Vector2 latestPos;
    Vector2 latestVel;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        photonView = GetComponent<PhotonView>();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rb.position);
            stream.SendNext(rb.velocity);
        }
        else
        {
            latestPos = (Vector2)stream.ReceiveNext();
            latestVel = (Vector2)stream.ReceiveNext();
        }
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            rb.position = Vector2.Lerp(rb.position, latestPos, Time.fixedDeltaTime * 10f);
            rb.velocity = latestVel;
        }
    }
}
