using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Rock : MonoBehaviour
{
    Rigidbody2D rb2D;
    public float speed;
    Vector3 dir;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        float rotation = Random.Range(0f, 360f);
        rb2D.rotation = rotation;
        dir = transform.up;
    }

    private void FixedUpdate()
    {
        transform.Translate(dir * speed * Time.smoothDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb2D.rotation += 90f;
        dir = transform.up;
        if (collision.gameObject.tag == "Bird")
        {
            if (collision.gameObject.GetComponent<BirdIdle>().idle == false)
                GetComponent<HP>().hp -= 10f;
        }
    }
}
