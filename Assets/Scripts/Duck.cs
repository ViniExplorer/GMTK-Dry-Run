using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour
{
    public GameObject spit;
    GameObject targetBird;
    BirdIdle[] birds;

    private void Awake()
    {
        var health = GetComponent<HP>();
        health.hp = PlayerPrefs.GetInt("Level") * 50f;
        health.maxHP = PlayerPrefs.GetInt("Level") * 50f;
    }

    void Routine()
    {
        birds = FindObjectsOfType<BirdIdle>();

        float x = Mathf.Infinity;
        foreach (BirdIdle bird in birds)
        {
            if (Vector2.Distance(bird.transform.position, transform.position) < x)
                targetBird = bird.gameObject;
        }

        StartCoroutine(MoveTools.SmoothMovement(gameObject, targetBird.transform.position, 2f, 3f));

        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        while (targetBird != null)
        {
            if (Vector3.Distance(targetBird.transform.position, transform.position) > 2f)
            {
                StartCoroutine(MoveTools.SmoothMovement(gameObject, targetBird.transform.position, 2f, 3f));
            }
            
            var bullet = Instantiate(spit, transform.GetChild(0).position, Quaternion.identity);

            bullet.GetComponent<DuckSpit>().target = targetBird;

            yield return new WaitForSeconds(1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetBird == null)
        {
            Routine();
        }
        else
        {
            if (targetBird.transform.position.x < transform.position.x)
                transform.localScale = new Vector3(-4f, 4f, 1f);
            else
                transform.localScale = new Vector3(4f, 4f, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bird")
        {
            if (collision.gameObject.GetComponent<BirdIdle>().idle == false)
            {
                GetComponent<HP>().hp -= 15f;
            }
        }
    }
}
