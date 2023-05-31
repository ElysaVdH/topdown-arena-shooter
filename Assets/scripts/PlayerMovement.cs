using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 10;

    public GameObject bullet;
    Transform gun;

    void Start()
    {
        gun = transform.Find("Gun").Find("BulletOrigin");
    }

    void Update()
    {
        //if player has won, the player cant move
        if (GameObject.FindWithTag("Enemy") != null)
        {
            //move and rotate
            Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));

            if (move != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), .25f);
            }

            transform.Translate(move * playerSpeed * Time.deltaTime, Space.World);

            //fire bullet
            if (Input.GetButton("Fire"))
            {
                Instantiate(bullet, gun.position, transform.rotation);
            }
        }
    }
}
