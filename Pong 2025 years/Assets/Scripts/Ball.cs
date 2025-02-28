using Photon.Pun;
using UnityEngine;

public class Ball : MonoBehaviourPun
{
    public float speedMultiplier = 1.05f;
    public float baseSpeed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (PhotonNetwork.IsMasterClient)
        {
            LaunchBall();
        }
    }

    void LaunchBall()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(-0.5f, 0.5f);  // слегка по диагонали
        rb.velocity = new Vector2(x, y).normalized * baseSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.contacts[0].normal;
        rb.velocity = Vector2.Reflect(rb.velocity.normalized, normal) * Mathf.Max(rb.velocity.magnitude * speedMultiplier, baseSpeed);
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude < 3f)
        {
            rb.velocity = rb.velocity.normalized * baseSpeed;
        }
    }
}
