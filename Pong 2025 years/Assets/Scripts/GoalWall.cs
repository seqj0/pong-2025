using Photon.Pun;
using UnityEngine;

public class GoalWall : MonoBehaviour
{
    public bool isLeftGoal; // true Ч это ворота слева, false Ч справа

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(collision.gameObject);
            ScoreManager.Instance.photonView.RPC("AddScore", RpcTarget.All, isLeftGoal);
            RespawnBall();
        }
    }

    void RespawnBall()
    {
        PhotonNetwork.Instantiate("Ball", Vector3.zero, Quaternion.identity);
    }
}
