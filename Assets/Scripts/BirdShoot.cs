﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdShoot : MonoBehaviour
{
    public float ballPower;
    public Rigidbody2D rb2D;

    public Vector2 minimumpower;
    public Vector2 maximumpower;
    public LineRenderer line;
    Camera camera;
    Vector2 ballforce;
    Vector3 startpoint;
    Vector3 endpoint;

    GameManager manager;
    public GameManager.Birds type;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        camera = Camera.main;
    }

    private void Update()
    {
        if (manager.activeBird == type)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<BirdIdle>().StopAllCoroutines();
                GetComponent<BirdIdle>().idle = false;
                rb2D.velocity = Vector3.zero;
                
                startpoint = camera.ScreenToWorldPoint(Input.mousePosition);
                startpoint.z = 15f;
            }
            if (Input.GetMouseButton(0))
            {
                GetComponent<BirdIdle>().StopAllCoroutines();
                GetComponent<BirdIdle>().idle = false;
                rb2D.velocity = Vector3.zero;
                Vector3 currentpoint = camera.ScreenToWorldPoint(Input.mousePosition);
                currentpoint.z = 15f;
                DrawLine(startpoint, currentpoint);
            }
            if (Input.GetMouseButtonUp(0))
            {
                endpoint = camera.ScreenToWorldPoint(Input.mousePosition);
                endpoint.z = 15;
            
                ballforce = new Vector2(Mathf.Clamp(startpoint.x - endpoint.x, minimumpower.x, maximumpower.x), Mathf.Clamp(startpoint.y - endpoint.y, minimumpower.y, maximumpower.y));
                float rotation = Mathf.Atan2(ballforce.x, ballforce.y) * Mathf.Rad2Deg + 270f;
                rb2D.rotation = rotation;
                GetComponent<Animator>().SetBool("shooting", true);
                rb2D.AddForce(ballforce * ballPower, ForceMode2D.Impulse);
                EndLine();
                StartCoroutine(StopBird());
            }
        }
        
    }

    IEnumerator StopBird()
    {
        yield return new WaitForSeconds(1f);
        rb2D.rotation = 0;
        rb2D.velocity = Vector3.zero;
        GetComponent<Animator>().SetBool("shooting", false);
        GetComponent<BirdIdle>().idle = true;
        GetComponent<BirdIdle>().StartCoroutine(GetComponent<BirdIdle>().MoveAround());
    }

    void DrawLine(Vector2 startpos, Vector2 endpos)
    {
        line.positionCount = 2;
        Vector3[] allPoints = new Vector3[2];
        allPoints[0] = startpos;
        allPoints[1] = endpos;
        line.SetPositions(allPoints);
    }

    void EndLine()
    {
        line.positionCount = 0;
    }
}
