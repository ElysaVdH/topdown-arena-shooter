using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    //player and enemie positions
    Vector3 playerPos;
    Vector3 enemiePos;

    NavMeshAgent enemy;
    public float enemySpeed = 10;

    public GameObject bullet;
    Transform gun;

    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
        gun = transform.Find("Gun").Find("BulletOrigin");

        //check positions
        enemiePos = transform.position;
        playerPos = GameObject.FindWithTag("Player").transform.position;
        enemy.SetDestination(playerPos);
    }

    void Update()
    {
        if (GameObject.FindWithTag("Player") != null)
        {
            //check positions
            enemiePos = transform.position;
            playerPos = GameObject.FindWithTag("Player").transform.position;
            enemy.SetDestination(playerPos);

            //check when to move and shoot
            if (enemy.remainingDistance > 3)
            {
                //start shooting
                if (enemy.remainingDistance < 10)
                {
                    enemy.isStopped = true;
                    transform.LookAt(playerPos);
                    Instantiate(bullet, gun.position, transform.rotation);
                }
                //start moving
                else if (enemy.remainingDistance < 25)
                {
                    enemy.isStopped = false;
                }
                //stand still
                else
                {
                    enemy.isStopped = true;
                }
            }
        }
        else
        {
            //stand still when player lost
            enemy.isStopped = true;
        }
    }
}
