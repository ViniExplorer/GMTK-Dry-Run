using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpit : MonoBehaviour
{
    public GameObject target;
    bool done = false;
    public GameObject water;

    private void Update()
    {
        if (target != null && !done)
        {
            done = true;

            transform.up = -target.transform.position;

            GetComponent<Rigidbody2D>().AddForce(-transform.up * 10, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Barrier")
            Destroy(gameObject);
        if(collision.gameObject.tag == "Bird")
        {
            collision.gameObject.GetComponent<HP>().hp -= 5f;
            Instantiate(water, collision.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
