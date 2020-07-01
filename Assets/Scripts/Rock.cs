using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Rock : MonoBehaviour
{
    Rigidbody2D rb2D;
    public float speed;
    Vector3 dir;
    public List<Sprite> sprites;
    HP health;
    public GameObject explosion;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        health = GetComponent<HP>();
        health.hp = PlayerPrefs.GetInt("Level") * 100f;
        health.maxHP = PlayerPrefs.GetInt("Level") * 100f;
        float rotation = Random.Range(0f, 360f);
        rb2D.rotation = rotation;
        dir = transform.up;
    }

    private void Update()
    {
        foreach (Sprite sprite in sprites)
        {
            float split = health.maxHP / sprites.Count;
            if (health.hp > health.maxHP - (sprites.IndexOf(sprite) * split) && health.hp <= health.maxHP - ((sprites.IndexOf(sprite) - 1) * split))
            {
                GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }
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
            {
                if(GetComponent<HP>().hp - 10f <= 0f)
                {
                    Instantiate(explosion, transform.position, Quaternion.identity);
                    FindObjectOfType<AudioManager>().Play("RockDeath");
                    GetComponent<HP>().hp -= 10f;
                }
                FindObjectOfType<AudioManager>().Play("RockDmg");
                GetComponent<HP>().hp -= 10f;
                speed += 0.5f;
            }
        }
    }
}
