using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdIdle : MonoBehaviour
{
    public bool idle = true;


    public void FlapSound()
    {
        FindObjectOfType<AudioManager>().Play("BirdFlap");
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveAround());
        var health = GetComponent<HP>();
        health.hp = PlayerPrefs.GetInt("Level") * 50f;
        health.maxHP = PlayerPrefs.GetInt("Level") * 50f;
    }

 
    public IEnumerator MoveAround()
    {
        while (idle)
        {
            float randX = Random.Range(-8f, 8f);
            float randY = Random.Range(-4f, 4f);

            Vector2 newPos = new Vector2(randX, randY);

            if (randX < transform.position.x)
                transform.localScale = new Vector3(-4f, 4f, 1f);
            else
                transform.localScale = new Vector3(4f, 4f, 1f);

            StartCoroutine(MoveTools.SmoothMovement(gameObject, newPos, 0f, 5f));

            GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            yield return new WaitForSeconds(1f);
        }
    }
}
