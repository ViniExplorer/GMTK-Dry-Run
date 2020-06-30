using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Rock : MonoBehaviour
{
    Rigidbody2D rb2D;
    public float speed;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        float rotation = Random.Range(0f, 360f);
        rb2D.rotation = rotation;
    }

    private void FixedUpdate()
    {
        transform.position += new Vector3() * Time.deltaTime;
    }
}
