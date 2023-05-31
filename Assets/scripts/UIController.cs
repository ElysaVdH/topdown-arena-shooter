using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //ui panels
    public GameObject start;
    public GameObject lost;
    public GameObject won;
    public GameObject Difficulty;

    //prefabs
    public GameObject player;
    public GameObject enemy;

    List<GameObject> playEnemyList;

    //difficulty setting
    Dropdown levelSetting;
    int enemyCount;
    public static int stamina;

    void Start()
    {
        //set panels correctly
        start.SetActive(true);
        lost.SetActive(false);
        won.SetActive(false);
        Difficulty.SetActive(true);

        //set default difficulty
        levelSetting = Difficulty.transform.Find("LevelSetting").GetComponent<Dropdown>();
        levelSetting.value = 1;

        //list to reset level
        playEnemyList = new List<GameObject>();
    }

    void Update()
    {
        //check when to start
        if (playEnemyList.Count > 1)
        {
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

            //player lost
            if (GameObject.FindWithTag("Player") == null)
            {
                lost.SetActive(true);
                Difficulty.SetActive(true);
            }

            //player won
            else if (enemyCount == 0)
            {
                won.SetActive(true);
                Difficulty.SetActive(true);
            }
        }
    }


    public void StartGame()
    {
        //reset list and instantiate player and enemies
        playEnemyList = new List<GameObject>();
        playEnemyList.Add(Instantiate(player));
        SetEnemies();

        //set all panel inactive
        start.SetActive(false);
        lost.SetActive(false);
        won.SetActive(false);
        Difficulty.SetActive(false);
    }

    public void ResetGame()
    {
        //destroy remaining player or enemies
        foreach(GameObject playerEnemy in playEnemyList)
        {
            Destroy(playerEnemy);
        }
        StartGame();
    }

    public void SetEnemies()
    {
        //set enemies and stamina
        switch (levelSetting.value)
        {
            case 0:
                enemyCount = 5;
                stamina = 7;
                break;
            case 1:
                enemyCount = 10;
                stamina = 5;
                break;
            case 2:
                enemyCount = 15;
                stamina = 3;
                break;
        }

        //instantiate set number of enemies
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 centerRange = GameObject.FindWithTag("Player").transform.position + Random.insideUnitSphere * 100;
            NavMeshHit hit;
            NavMesh.SamplePosition(centerRange, out hit, 100, NavMesh.AllAreas);
            playEnemyList.Add(Instantiate(enemy, hit.position, Quaternion.identity));
        }
    }
}
