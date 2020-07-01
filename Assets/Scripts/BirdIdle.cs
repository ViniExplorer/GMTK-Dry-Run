using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdIdle : MonoBehaviour
{
    public bool idle = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveAround());
    }

 
    public IEnumerator MoveAround()
    {
        while (idle)
        {
            float randX = Random.Range(-8f, 8f);
            float randY = Random.Range(-4f, 4f);

            Vector2 newPos = new Vector2(randX, randY);

            StartCoroutine(MoveTools.SmoothMovement(gameObject, newPos, 0f, 5f));

            GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            yield return new WaitForSeconds(1f);
        }
    }
}
