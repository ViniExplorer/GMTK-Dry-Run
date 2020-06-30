using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    public float hp;
    public float maxHP;

    public List<Sprite> sprites;

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        foreach(Sprite sprite in sprites)
        {
            float split = maxHP / sprites.Count;
            if (hp > maxHP - (sprites.IndexOf(sprite) * split) && hp <= maxHP - ((sprites.IndexOf(sprite) - 1) * split))
            {
                GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }
    }
}
