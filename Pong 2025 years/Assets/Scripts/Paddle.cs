using Photon.Pun;
using UnityEngine;

public class Paddle : MonoBehaviourPun
{
    public float speed = 5f;

    public float yLimit = 4.5f; // ������� �� ������� ����� ������

    void Update()
    {
        if (!photonView.IsMine) return;

        float move = Input.GetAxisRaw("Vertical");
        transform.Translate(Vector3.up * move * speed * Time.deltaTime);

        // ����������� �� ������
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, -yLimit, yLimit);
        transform.position = pos;
    }
}
