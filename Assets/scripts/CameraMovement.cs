using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 pos;
    float distance;

    void Start()
    {
        distance = transform.position.z;
    }

    void Update()
    {
        //move x and z(with set distance) to follow player
        if (GameObject.FindWithTag("Player") != null)
        {
            pos = GameObject.FindWithTag("Player").transform.position;
            transform.position = new Vector3(pos.x, transform.position.y, pos.z + distance);
        }
    }
}
