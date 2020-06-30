using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdShoot : MonoBehaviour
{
    public float shootForce;
    public Rigidbody2D rb2D;
    private BirdIdle bird;
    public float speed;
    bool alreadyShooting = false;
    bool done = true;
    bool notShoot = true;
    

    private void Start()
    {
        bird = GetComponent<BirdIdle>();
    }

    public void StartDragShoot()
    {
        bird.idle = false;
        StartCoroutine(DragShoot());
    }

    IEnumerator DragShoot()
    {
        while (notShoot) {
            var lookDir = Input.mousePosition - transform.position;

            float dir = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg -270f;

            rb2D.rotation = dir;

            yield return null;
        }
    }
    
    IEnumerator ShootTimer(float secs)
    {
        float sec = 0f;
        while (sec <= secs)
        {
            sec += 1;
            yield return new WaitForSeconds(1f);
        }
        bird.idle = true;
        done = true;
        alreadyShooting = false;
        notShoot = true;
        bird.StartCoroutine(bird.MoveAround());
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && bird.idle == false)
        {
            if (alreadyShooting == false)
            {
                alreadyShooting = true;
                notShoot = false;
                print("hihiiihihi");
                StopAllCoroutines();
                StartCoroutine(ShootTimer(3f));
                rb2D.AddForce(transform.right * shootForce, ForceMode2D.Impulse);
            }
        }
    }

    
    private void OnMouseOver()
    {
        print("Hey there!");
        if (Input.GetMouseButtonDown(0) && done == true)
        {
            print("damn");
            done = false;
            StartDragShoot();
        }
    }
    
}
