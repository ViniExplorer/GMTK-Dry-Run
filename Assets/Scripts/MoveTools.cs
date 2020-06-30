using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTools
{
    public static IEnumerator SmoothMovement(GameObject self, Vector3 targetPos, float finalDistance, float moveTime)
    {
        while(Vector2.Distance(self.transform.position, targetPos) > finalDistance)
        {
            Vector2 step = Vector2.MoveTowards(self.transform.position, targetPos, moveTime * Time.deltaTime);

            self.GetComponent<Rigidbody2D>().MovePosition(step);

            yield return null;
        }
    }
}
