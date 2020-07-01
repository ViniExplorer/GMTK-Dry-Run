using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int level;
    int numOfEnemies;
    int num = 0;

    public Transform[] birds;
    public GameObject[] enemies;

    public enum Birds { Bobby, Dolly }

    public Birds activeBird = Birds.Bobby;

    GameObject bobby, dolly;

    IEnumerator GeneratorOfEnemies()
    {
        while (num < numOfEnemies)
        {
            GenerateEnemy();
            yield return new WaitForSeconds(1f);
            num++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        level = PlayerPrefs.GetInt("Level");
        numOfEnemies = level * 2;
        bobby = GameObject.Find("Bobby");
        dolly = GameObject.Find("Dolly");
        StartCoroutine(GeneratorOfEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (activeBird == Birds.Bobby)
            {
                bobby.GetComponent<Animator>().SetBool("active", false);
                activeBird = Birds.Dolly;
                dolly.GetComponent<Animator>().SetBool("active", true);
            }
            else
            {
                dolly.GetComponent<Animator>().SetBool("active", false);
                activeBird = Birds.Bobby;
                bobby.GetComponent<Animator>().SetBool("active", true);
            }
        }
        if(GameObject.Find("Rock") == null)
        {
            StartCoroutine(NextLevel());
        }
    }

    void GenerateEnemy()
    {
        float randX = Random.Range(-8f, 8f);
        float randY = Random.Range(-4f, 4f);

        Vector2 newPos = new Vector2(randX, randY);

        foreach (Transform bird in birds)
        {
            if (Vector3.Distance(newPos, bird.position) < 1f)
            {
                GenerateEnemy();
                return;
            }
        }

        int numChosen = Random.Range(0, enemies.Length - 1);

        Instantiate(enemies[numChosen], newPos, Quaternion.identity);
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1.5f);
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene(1);
    }
}
