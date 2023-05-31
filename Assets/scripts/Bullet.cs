using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 15;

    //to check who fired
    int x;
    GameObject attacker;

    void Start()
    {
        x = 0;
    }

    void Update()
    {
        //move in direction that the bullet is fired
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision other)
    {
        //check what is hit
        if (other.gameObject.tag == "Player")
        {
            if (x == 0)
            {
                //player fired
                attacker = other.gameObject;
                x++;
            }
            //if enemy hit player
            else if (attacker != null && attacker.tag != "Player")
            {
                UIController.stamina--;

                if (UIController.stamina <= 0)
                {
                    Destroy(other.gameObject);
                }

                Destroy(gameObject);
            }
        }
        else if(other.gameObject.tag == "Enemy")
        {
            if (x == 0)
            {
                //enemy fired
                attacker = other.gameObject;
                x++;
            }
            //if player hit enemy
            else if(attacker != null && attacker.tag != "Enemy")
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
        else
        {
            //destroy if it hit anything else
            Destroy(gameObject);
        }
    }
}