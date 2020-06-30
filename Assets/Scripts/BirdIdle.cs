using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdIdle : MonoBehaviour
{
    public bool idle;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveAround());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    IEnumerator MoveAround()
    {
        while (idle)
        {
            float randX = Random.Range(-1.78f, 1.78f);
            float randY = Random.Range(-1f, 1f);

            Vector2 newPos = new Vector2(randX, randY);

            StartCoroutine(MoveTools.SmoothMovement(gameObject, newPos, 0f, 5f));

            yield return new WaitForSeconds(1f);
        }
    }
}
