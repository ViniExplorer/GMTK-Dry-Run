using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum Birds { Bobby, Dolly }

    public Birds activeBird = Birds.Bobby;

    GameObject bobby, dolly;

    // Start is called before the first frame update
    void Start()
    {
        bobby = GameObject.Find("Bobby");
        dolly = GameObject.Find("Dolly");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (activeBird == Birds.Bobby)
            {
                bobby.transform.GetChild(0).gameObject.SetActive(false);
                activeBird = Birds.Dolly;
                dolly.transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                dolly.transform.GetChild(0).gameObject.SetActive(false);
                activeBird = Birds.Bobby;
                bobby.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
}
